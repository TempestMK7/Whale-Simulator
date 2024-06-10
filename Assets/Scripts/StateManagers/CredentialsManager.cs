using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;

using Newtonsoft.Json;
using UnityEngine;

using Com.Tempest.Whale.GameObjects;
using Com.Tempest.Whale.RequestObjects;
using Com.Tempest.Whale.StateObjects;


public class CredentialsManager : MonoBehaviour {

    private string refreshTokenFile;
    private string cachedToken;
    private HttpClient authClient;
    private HttpClient noAuthClient;

    #region Initialization.

    public void Awake() {
        DontDestroyOnLoad(gameObject);
        refreshTokenFile = Application.persistentDataPath + "/access_token.txt";
        authClient = new HttpClient();
        noAuthClient = new HttpClient();
    }

    public async Task InitializeEverything() {
        if (File.Exists(refreshTokenFile)) {
            using (StreamReader reader = new StreamReader(refreshTokenFile)) {
                cachedToken = reader.ReadLine();
                reader.Close();
            }
        } else {
            var httpContent = new StringContent("{}");
            var httpResponse = await noAuthClient.PostAsync("https://h9wf2pigyj.execute-api.us-west-2.amazonaws.com/createuser", httpContent);
            if (httpResponse.Content != null && httpResponse.IsSuccessStatusCode) {
                var newUserResponse = JsonConvert.DeserializeObject<CreateUserResponse>(await httpResponse.Content.ReadAsStringAsync());
                cachedToken = newUserResponse.AccessToken;
                SaveUserSession();
            } else {
                Debug.Log("Unable to get a new user token: " + httpResponse.StatusCode + ", " + httpResponse.RequestMessage);
            }
        }
    }

    #endregion

    #region State altering requests.

    private async Task<T> MakeLambdaCall<T, U>(U request, string url) {
        var uri = new Uri(url, UriKind.Absolute);
        var httpContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        authClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", cachedToken);
        var httpResponse = await authClient.PostAsync(uri, httpContent);
        if (httpResponse.Content != null && httpResponse.IsSuccessStatusCode) {
            return JsonConvert.DeserializeObject<T>(await httpResponse.Content.ReadAsStringAsync());
        } else {
            Debug.Log("Failed web call: " + httpResponse.StatusCode + " on url: " + url);
            if (httpResponse.Content != null) {
                Debug.Log("Response: " + await httpResponse.Content.ReadAsStringAsync());
            }
            return default;
        }
    }

    private async Task<T> MakeLambdaCall<T>(string url) {
        var uri = new Uri(url, UriKind.Absolute);
        var httpContent = new StringContent("{}", Encoding.UTF8, "application/json");
        authClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", cachedToken);
        var httpResponse = await authClient.PostAsync(uri, httpContent);
        if (httpResponse.Content != null && httpResponse.IsSuccessStatusCode) {
            return JsonConvert.DeserializeObject<T>(await httpResponse.Content.ReadAsStringAsync());
        } else {
            Debug.Log("Failed web call: " + httpResponse.StatusCode + " on url: " + url);
            if (httpResponse.Content != null) {
                Debug.Log("Response: " + await httpResponse.Content.ReadAsStringAsync());
            }
            return default;
        }
    }

    public async Task DownloadState() {
        var newState = await MakeLambdaCall<AccountState>("https://qaynofsmwk.execute-api.us-west-2.amazonaws.com/downloadstate");
        StateManager.OverrideState(newState);
    }

    public async Task ClaimResources() {
        ClaimResourcesResponse response = await MakeLambdaCall<ClaimResourcesResponse>("https://qaynofsmwk.execute-api.us-west-2.amazonaws.com/claimresources");
        StateManager.HandleClaimResourcesResponse(response);
    }

    public async Task<List<AccountHero>> RequestSummons(int summonCount) {
        var summonRequest = new SummonRequest() {
            SummonCount = summonCount
        };
        var response = await MakeLambdaCall<SummonResponse, SummonRequest>(summonRequest, "https://qaynofsmwk.execute-api.us-west-2.amazonaws.com/summonhero");
        StateManager.HandleSummonResponse(response);
        return response.SummonedHeroes;
    }

    public async Task<List<AccountHero>> RequestSummons(FactionEnum faction, int rarity, int summonCount) {
        var summonRequest = new FactionSummonRequest() {
            ChosenFaction = faction,
            SummonCount = summonCount,
            SummonRarity = rarity
        };
        var response = await MakeLambdaCall<FactionSummonResponse, FactionSummonRequest>(summonRequest, "SummonFactionHeroFunction");
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
        /*        var provider = cachedProvider;

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
                }*/
        return true;
    }

    public async Task<bool> LoginUser(string username, string password) {
/*        var provider = cachedProvider;
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
        cachedUsername = username;*/
        return true;
    }

    public void SaveUserSession() {
        if (cachedToken == null) return;
        StreamWriter writer = new StreamWriter(refreshTokenFile);
        writer.WriteLine(cachedToken);
        writer.Close();
    }

    #endregion

    #region Utility methods.

    public void DeleteRefreshToken() {
        if (File.Exists(refreshTokenFile)) {
            File.Delete(refreshTokenFile);
        }
    }

    public static void DisplayNetworkError(Canvas sceneCanvas, string errorMessage) {
        var loadingPopup = FindObjectOfType<LoadingPopup>();
        if (loadingPopup != null) loadingPopup.DismissPopup();
        var tooltipPopup = Instantiate(Resources.Load<GameObject>("TooltipPopup"), sceneCanvas.transform).GetComponent<TooltipPopup>();
        tooltipPopup.SetTooltip("Error", errorMessage);
    }

    #endregion
}