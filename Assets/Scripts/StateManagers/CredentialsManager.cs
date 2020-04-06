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
        var lambdaClient = new AmazonLambdaClient(cachedCredentials, RegionEndpoint.USWest2);
        var state = StateManager.GetCurrentState();
        var uploadRequest = new UploadStateRequest() {
            Verified = authenticatedUser,
            UploadedState = state
        };
        var request = new InvokeRequest() {
            FunctionName = "UploadStateFunction",
            Payload = MangleRequest(JsonConvert.SerializeObject(uploadRequest)),
            InvocationType = InvocationType.RequestResponse
        };
        var result = await lambdaClient.InvokeAsync(request);
        var responseReader = new StreamReader(result.Payload);
        var responseBody = responseReader.ReadToEnd();
        responseReader.Dispose();
        Debug.Log("Upload State Response: " + responseBody);
    }

    public async Task ClaimResources() {
        var currentState = StateManager.GetCurrentState();
        var lambdaClient = new AmazonLambdaClient(cachedCredentials, RegionEndpoint.USWest2);
        var claimResourcesRequest = new ClaimResourcesRequest() {
            Verified = authenticatedUser,
            AccountGuid = currentState.Id
        };
        var request = new InvokeRequest() {
            FunctionName = "ClaimResourcesFunction",
            Payload = MangleRequest(JsonConvert.SerializeObject(claimResourcesRequest)),
            InvocationType = InvocationType.RequestResponse
        };
        var result = await lambdaClient.InvokeAsync(request);
        var responseReader = new StreamReader(result.Payload);
        var responseBody = responseReader.ReadToEnd();
        responseReader.Dispose();
        var claimResourcesResponse = DeserializeObject<ClaimResourcesResponse>(responseBody);
        StateManager.HandleClaimResourcesResponse(claimResourcesResponse);
    }

    public async Task<List<AccountHero>> RequestSummons(int summonCount) {
        var currentState = StateManager.GetCurrentState();
        var lambdaClient = new AmazonLambdaClient(cachedCredentials, RegionEndpoint.USWest2);
        var summonRequest = new SummonRequest() {
            Verified = authenticatedUser,
            AccountGuid = currentState.Id,
            SummonCount = summonCount
        };
        var request = new InvokeRequest() {
            FunctionName = "SummonHeroFunction",
            Payload = MangleRequest(JsonConvert.SerializeObject(summonRequest)),
            InvocationType = InvocationType.RequestResponse
        };
        var result = await lambdaClient.InvokeAsync(request);
        var responseReader = new StreamReader(result.Payload);
        var responseBody = responseReader.ReadToEnd();
        responseReader.Dispose();
        var summonResponse = DeserializeObject<SummonResponse>(responseBody);
        StateManager.HandleSummonResponse(summonResponse);
        return summonResponse.SummonedHeroes;
    }

    public async Task<bool> RequestFusion(AccountHero fusedHero, List<AccountHero> destroyedHeroes) {
        if (!LevelContainer.FusionIsLegal(fusedHero, destroyedHeroes)) {
            return false;
        }

        var destroyedHeroIds = new List<Guid>();
        foreach (AccountHero hero in destroyedHeroes) {
            destroyedHeroIds.Add(hero.Id);
        }

        var currentState = StateManager.GetCurrentState();
        var lambdaClient = new AmazonLambdaClient(cachedCredentials, RegionEndpoint.USWest2);
        var fuseRequest = new FuseHeroRequest() {
            Verified = authenticatedUser,
            AccountGuid = currentState.Id,
            FusedHeroId = fusedHero.Id,
            DestroyedHeroIds = destroyedHeroIds
        };
        var request = new InvokeRequest() {
            FunctionName = "FuseHeroFunction",
            Payload = MangleRequest(JsonConvert.SerializeObject(fuseRequest)),
            InvocationType = InvocationType.RequestResponse
        };
        var result = await lambdaClient.InvokeAsync(request);
        var responseReader = new StreamReader(result.Payload);
        var responseBody = responseReader.ReadToEnd();
        responseReader.Dispose();

        var fuseResponse = DeserializeObject<FuseHeroResponse>(responseBody);
        if (!fuseResponse.FusionSuccessful) return false;
        StateManager.HandleFuseResponse(fuseResponse, fusedHero, destroyedHeroes);
        return true;
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