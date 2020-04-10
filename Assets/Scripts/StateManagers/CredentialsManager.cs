using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Amazon;
using Amazon.CognitoIdentity;
using Amazon.CognitoIdentity.Model;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Amazon.Extensions.CognitoAuthentication;
using Amazon.Lambda;
using Amazon.Lambda.Model;

using Newtonsoft.Json;
using UnityEngine;

using Com.Tempest.Whale.GameObjects;
using Com.Tempest.Whale.RequestObjects;
using Com.Tempest.Whale.StateObjects;

public class CredentialsManager : MonoBehaviour {

    private const string identityPoolId = "us-west-2:8f446e6c-1559-49bc-be35-3f9462c5205c";
    private const string appClientId = "78kml34kbghal04vem0mf3eff8";
    private const string userPoolId = "us-west-2_r9S6PYbdH";

    private string refreshTokenFile;

    private bool authenticatedUser = false;
    private string cachedUsername;
    private CognitoAWSCredentials cachedCredentials;
    private AmazonCognitoIdentityProviderClient cachedProvider;
    private string cachedAccountGuid;

    #region Initialization.

    public void Awake() {
        DontDestroyOnLoad(gameObject);
        refreshTokenFile = Application.persistentDataPath + "/refresh_token.txt";
    }

    public bool UserIsAuthenticated() {
        return authenticatedUser;
    }

    public string GetUsername() {
        return cachedUsername;
    }

    public async Task InitializeEverything() {
        if (File.Exists(refreshTokenFile)) {
            StreamReader reader = new StreamReader(refreshTokenFile);
            cachedUsername = reader.ReadLine();
            var idToken = reader.ReadLine();
            var refreshToken = reader.ReadLine();
            reader.Close();

            var provider = new AmazonCognitoIdentityProviderClient(new Amazon.Runtime.AnonymousAWSCredentials(), RegionEndpoint.USWest2);

            CognitoUserPool userPool = new CognitoUserPool(userPoolId, appClientId, provider);
            CognitoUser user = new CognitoUser(cachedUsername, appClientId, userPool, provider);
            user.SessionTokens = new CognitoUserSession(idToken, string.Empty, refreshToken, DateTime.UtcNow, DateTime.UtcNow.AddDays(365));
            var request = new InitiateRefreshTokenAuthRequest() { AuthFlowType = AuthFlowType.REFRESH_TOKEN };
            var authResponse = await user.StartWithRefreshTokenAuthAsync(request).ConfigureAwait(false);
            cachedCredentials = user.GetCognitoAWSCredentials(identityPoolId, RegionEndpoint.USWest2);

            var getUserRequest = new GetUserRequest();
            getUserRequest.AccessToken = authResponse.AuthenticationResult.AccessToken;
            var getUserResponse = await provider.GetUserAsync(getUserRequest);
            cachedAccountGuid = getUserResponse.UserAttributes.Find((AttributeType type) => { return type.Name.Equals("custom:custom:accountGuid"); }).Value;

            Debug.Log("Found validated user with Identity: " + cachedCredentials.GetIdentityId());
            authenticatedUser = true;
        } else {
            cachedCredentials = new WhaleCredentials(identityPoolId, RegionEndpoint.USWest2);
            cachedCredentials.GetCredentials();
            cachedCredentials.GetIdentityId();
        }
        cachedProvider = new AmazonCognitoIdentityProviderClient(cachedCredentials, RegionEndpoint.USWest2);
    }

    #endregion

    #region State altering requests.

    private async Task<T> MakeLambdaCall<T, U>(U request, string functionName) {
        var currentState = StateManager.GetCurrentState();
        var whaleRequest = new WhaleRequest<U>(authenticatedUser, currentState.Id, request);
        var lambdaClient = new AmazonLambdaClient(cachedCredentials, RegionEndpoint.USWest2);
        var lambdaRequest = new InvokeRequest() {
            FunctionName = functionName,
            Payload = MangleRequest(JsonConvert.SerializeObject(whaleRequest)),
            InvocationType = InvocationType.RequestResponse
        };
        var result = await lambdaClient.InvokeAsync(lambdaRequest);
        var responseReader = new StreamReader(result.Payload);
        var responseBody = responseReader.ReadToEnd();
        try {
            var whaleResponse = DeserializeObject<WhaleResponse<T>>(responseBody);
            responseReader.Dispose();
            if (!whaleResponse.Successful) Debug.Log("Call failed with error of: " + whaleResponse.Error);
            return whaleResponse.Response;
        } catch (Exception e) {
            Debug.LogError(e);
            Debug.Log("Failed response body: " + responseBody);
            responseReader.Dispose();
            return default;
        }
    }

    public async Task DownloadState() {
        string accountGuid = "";
        if (authenticatedUser) accountGuid = cachedAccountGuid;
        else if (StateManager.Initialized()) {
            accountGuid = StateManager.GetCurrentState().Id.ToString();
        }

        var downloadRequest = new DownloadStateRequest() {
            AccountGuid = accountGuid,
            Verified = authenticatedUser
        };
        var lambdaClient = new AmazonLambdaClient(cachedCredentials, RegionEndpoint.USWest2);
        var request = new InvokeRequest() {
            FunctionName = "DownloadStateFunction",
            Payload = MangleRequest(JsonConvert.SerializeObject(downloadRequest)),
            InvocationType = InvocationType.RequestResponse,
        };
        var result = await lambdaClient.InvokeAsync(request);
        var responseReader = new StreamReader(result.Payload);
        var responseBody = await responseReader.ReadToEndAsync();
        responseReader.Dispose();
        var newState = DeserializeObject<AccountState>(responseBody);
        cachedAccountGuid = newState.Id.ToString();
        StateManager.OverrideState(newState);
    }

    public async Task UploadStateToServer() {
        var state = StateManager.GetCurrentState();
        var uploadRequest = new UploadStateRequest() {
            UploadedState = state
        };
        var response = await MakeLambdaCall<UploadStateResponse, UploadStateRequest>(uploadRequest, "UploadStateFunction");
        Debug.Log("Upload State Response: " + response.LinesAffected + " lines affected.");
    }

    public async Task ClaimResources() {
        var claimResourcesRequest = new ClaimResourcesRequest();
        ClaimResourcesResponse response = await MakeLambdaCall<ClaimResourcesResponse, ClaimResourcesRequest>(claimResourcesRequest, "ClaimResourcesFunction");
        StateManager.HandleClaimResourcesResponse(response);
    }

    public async Task<List<AccountHero>> RequestSummons(int summonCount) {
        var summonRequest = new SummonRequest() {
            SummonCount = summonCount
        };
        var response = await MakeLambdaCall<SummonResponse, SummonRequest>(summonRequest, "SummonHeroFunction");
        StateManager.HandleSummonResponse(response);
        return response.SummonedHeroes;
    }

    public async Task<bool> RequestLevelup(AccountHero selectedHero) {
        if (selectedHero.CurrentLevel >= LevelContainer.MaxLevelForAwakeningValue(selectedHero.AwakeningLevel)) {
            return false;
        }

        long cost = LevelContainer.HeroExperienceRequirement(selectedHero.CurrentLevel);
        var currentState = StateManager.GetCurrentState();
        if (currentState.CurrentGold < cost || currentState.CurrentSouls < cost) {
            return false;
        }

        var request = new LevelupHeroRequest() {
            AccountHeroId = selectedHero.Id
        };
        var response = await MakeLambdaCall<LevelupHeroResponse, LevelupHeroRequest>(request, "LevelupHeroFunction");
        StateManager.HandleLevelupResponse(response, selectedHero);
        return response.LevelupSuccessful;
    }

    public async Task<bool> RequestFusion(AccountHero fusedHero, List<AccountHero> destroyedHeroes) {
        if (!LevelContainer.FusionIsLegal(fusedHero, destroyedHeroes)) {
            return false;
        }

        var destroyedHeroIds = new List<Guid>();
        foreach (AccountHero hero in destroyedHeroes) {
            destroyedHeroIds.Add(hero.Id);
        }

        var request = new FuseHeroRequest() {
            FusedHeroId = fusedHero.Id,
            DestroyedHeroIds = destroyedHeroIds
        };
        var response = await MakeLambdaCall<FuseHeroResponse, FuseHeroRequest>(request, "FuseHeroFunction");
        if (!response.FusionSuccessful) return false;
        StateManager.HandleFuseResponse(response, fusedHero, destroyedHeroes);
        return true;
    }

    public async Task<bool> RequestFusion(AccountEquipment fusedEquipment, List<AccountEquipment> destroyedEquipment) {
        if (!LevelContainer.FusionIsLegal(fusedEquipment, destroyedEquipment)) {
            return false;
        }

        var destroyedIds = new List<Guid>();
        foreach (AccountEquipment destroyed in destroyedEquipment) {
            destroyedIds.Add(destroyed.Id);
        }
        var request = new FuseEquipmentRequest() {
            FusedEquipmentId = fusedEquipment.Id,
            DestroyedEquipmentIds = destroyedIds
        };
        var response = await MakeLambdaCall<FuseEquipmentResponse, FuseEquipmentRequest>(request, "FuseEquipmentFunction");
        if (!response.FusionSuccessful) return false;
        StateManager.HandleFuseResponse(response, fusedEquipment, destroyedEquipment);
        return true;
    }

    public async Task EquipToHero(AccountEquipment equipment, AccountHero hero, EquipmentSlot? slot) {
        Guid? heroId = null;
        if (hero != null) heroId = hero.Id;

        var request = new EquipRequest() {
            EquipmentId = equipment.Id,
            HeroId = heroId,
            Slot = slot
        };
        var response = await MakeLambdaCall<EquipResponse, EquipRequest>(request, "EquipHeroFunction");
        StateManager.HandleEquipResponse(response, equipment, hero, slot);
    }

    public async Task UnequipHero(AccountHero hero) {
        var request = new UnequipHeroRequest() {
            HeroId = hero.Id
        };
        await MakeLambdaCall<UnequipHeroResponse, UnequipHeroRequest>(request, "UnequipHeroFunction");
        StateManager.HandleUnequipResponse(hero);
    }

    public async Task<CombatResponse> PerformEpicBattle(BattleEnum battleType, AccountHero[] selectedHeroes) {
        var selectedGuids = new Guid?[selectedHeroes.Length];
        for (int x = 0; x < selectedHeroes.Length; x++) {
            if (selectedHeroes[x] != null) selectedGuids[x] = selectedHeroes[x].Id;
        }
        var request = new CombatRequest() {
            EncounterType = battleType,
            SelectedHeroes = selectedGuids
        };
        var response = await MakeLambdaCall<CombatResponse, CombatRequest>(request, "CombatFunction");
        if (response != null && response.Report != null) {
            response.Report.RestoreUnserializedData();
            if (response.Rewards != null) response.Rewards.RestoreUnserializedData();
        }
        StateManager.HandleCombatResponse(battleType, response);
        return response;
    }

    public async Task<LootCaveEncounter> RequestCaveEncounter() {
        var request = new LootCaveEncounterRequest();
        var response = await MakeLambdaCall<LootCaveEncounterResponse, LootCaveEncounterRequest>(request, "LootCaveEncounterFunction");
        StateManager.HandleCaveEncounterResponse(response);
        return response.Encounter;
    }

    #endregion

    #region Account altering requests.

    public async Task<bool> CreateAccount(string username, string email, string password) {
        var provider = cachedProvider;

        var attributes = new List<AttributeType>() {
            new AttributeType() { Name = "email", Value = email },
            new AttributeType() { Name = "custom:custom:accountGuid", Value = StateManager.GetCurrentState().Id.ToString() }
        };

        var signupRequest = new SignUpRequest() {
            ClientId = appClientId,
            Username = username,
            Password = password,
            UserAttributes = attributes
        };

        try {
            var result = await provider.SignUpAsync(signupRequest);
            return true;
        } catch (Exception e) {
            Debug.LogError(e);
            return false;
        }
    }

    public async Task<bool> LoginUser(string username, string password) {
        var provider = cachedProvider;
        CognitoUserPool userPool = new CognitoUserPool(userPoolId, appClientId, provider);
        CognitoUser user = new CognitoUser(username, appClientId, userPool, provider);
        InitiateSrpAuthRequest authRequest = new InitiateSrpAuthRequest() {
            Password = password
        };
        try {
            var authResponse = await user.StartWithSrpAuthAsync(authRequest).ConfigureAwait(false);
            cachedCredentials = user.GetCognitoAWSCredentials(identityPoolId, RegionEndpoint.USWest2);

            SaveUserSession(user);

            cachedProvider = new AmazonCognitoIdentityProviderClient(cachedCredentials, RegionEndpoint.USWest2);

            var getUserRequest = new GetUserRequest();
            getUserRequest.AccessToken = authResponse.AuthenticationResult.AccessToken;
            var getUserResponse = await provider.GetUserAsync(getUserRequest);
            cachedAccountGuid = getUserResponse.UserAttributes.Find((AttributeType type) => { return type.Name.Equals("custom:custom:accountGuid"); }).Value;

            authenticatedUser = true;
        } catch (Exception e) {
            Debug.LogError(e);
            return false;
        }
        cachedUsername = username;
        return true;
    }

    public void SaveUserSession(CognitoUser user) {
        StreamWriter writer = new StreamWriter(refreshTokenFile);
        writer.WriteLine(user.Username);
        writer.WriteLine(user.SessionTokens.IdToken);
        writer.WriteLine(user.SessionTokens.RefreshToken);
        writer.Close();
    }

    #endregion

    #region Utility methods.

    private string MangleRequest(string request) {
        var mangled = request.Replace("\"", "\\\"");
        return "\"" + mangled + "\"";
    }

    private T DeserializeObject<T>(string serverResponse) {
        var trimmedString = serverResponse.Substring(1, serverResponse.Length - 2);
        return JsonConvert.DeserializeObject<T>(Regex.Unescape(trimmedString));
    }

    #endregion
}