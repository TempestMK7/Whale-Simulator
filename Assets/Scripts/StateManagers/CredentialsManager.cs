using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;

using Newtonsoft.Json;
using UnityEngine;

using Com.Tempest.Whale.GameObjects;
using Com.Tempest.Whale.RequestObjects;
using Com.Tempest.Whale.StateObjects;


public class CredentialsManager : MonoBehaviour {

    private const string IDENTITY_API_BASE_URL = "https://h9wf2pigyj.execute-api.us-west-2.amazonaws.com/";
    private const string STATE_API_BASE_URL = "https://qaynofsmwk.execute-api.us-west-2.amazonaws.com/";

    private string refreshTokenFile;
    private string cachedToken;
    private HttpClient identityClient;
    private HttpClient mainClient;
    private StateManager stateManager;

    #region Initialization.

    public void Awake() {
        DontDestroyOnLoad(gameObject);
        refreshTokenFile = Application.persistentDataPath + "/access_token.txt";
        identityClient = new HttpClient();
        identityClient.BaseAddress = new Uri(IDENTITY_API_BASE_URL);
        mainClient = new HttpClient();
        mainClient.BaseAddress = new Uri(STATE_API_BASE_URL);
        stateManager = FindObjectOfType<StateManager>();
    }

    public async Task InitializeEverything() {
        if (File.Exists(refreshTokenFile)) {
            using (StreamReader reader = new StreamReader(refreshTokenFile)) {
                cachedToken = reader.ReadLine();
                reader.Close();
            }
        } else {
            var httpContent = new StringContent("{}");
            var httpResponse = await identityClient.PostAsync("createuser", httpContent);
            if (httpResponse.Content != null && httpResponse.IsSuccessStatusCode) {
                var newUserResponse = JsonConvert.DeserializeObject<CreateUserResponse>(await httpResponse.Content.ReadAsStringAsync());
                cachedToken = newUserResponse.AccessToken;
                SaveUserSession();
            } else {
                Debug.Log("Unable to get a new user token: " + httpResponse.StatusCode + ", " + httpResponse.RequestMessage);
            }
        }

        if (cachedToken != null) {
            mainClient.DefaultRequestHeaders.Clear();
            mainClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", cachedToken);
        }
    }

    #endregion

    #region State altering requests.

    private async Task<T> MakeLambdaCall<T, U>(U request, string endPoint) {
        var httpContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var httpResponse = await mainClient.PostAsync(endPoint, httpContent);
        if (httpResponse.Content != null && httpResponse.IsSuccessStatusCode) {
            return JsonConvert.DeserializeObject<T>(await httpResponse.Content.ReadAsStringAsync());
        } else {
            Debug.Log("Failed web call: " + httpResponse.StatusCode + " on url: " + endPoint);
            if (httpResponse.Content != null) {
                Debug.Log("Response: " + await httpResponse.Content.ReadAsStringAsync());
            }
            return default;
        }
    }

    private async Task<T> MakeLambdaCall<T>(string endPoint) {
        var httpContent = new StringContent("{}", Encoding.UTF8, "application/json");
        var httpResponse = await mainClient.PostAsync(endPoint, httpContent);
        if (httpResponse.Content != null && httpResponse.IsSuccessStatusCode) {
            return JsonConvert.DeserializeObject<T>(await httpResponse.Content.ReadAsStringAsync());
        } else {
            Debug.Log("Failed web call: " + httpResponse.StatusCode + " on url: " + endPoint);
            if (httpResponse.Content != null) {
                Debug.Log("Response: " + await httpResponse.Content.ReadAsStringAsync());
            }
            return default;
        }
    }

    public async Task DownloadUserInfo() {
        GetUserInfoResponse response = await MakeLambdaCall<GetUserInfoResponse>("getuserinfo");
        stateManager.HandleUserInfoResponse(response);
    }

    public async Task DownloadState() {
        var newState = await MakeLambdaCall<AccountState>("downloadstate");
        stateManager.HandleAccountStateResponse(newState);
    }

    public async Task ClaimResources() {
        ClaimResourcesResponse response = await MakeLambdaCall<ClaimResourcesResponse>("claimresources");
        stateManager.HandleClaimResourcesResponse(response);
    }

    public async Task UpdateTutorials() {
        var accountState = stateManager.CurrentAccountState;
        var request = new UpdateTutorialsRequest() {
            HasEnteredHub = accountState.HasEnteredHub,
            HasEnteredSanctum = accountState.HasEnteredSanctum,
            HasEnteredPortal = accountState.HasEnteredPortal,
            HasEnteredCampaign = accountState.HasEnteredCampaign,
        };
        var response = await MakeLambdaCall<UpdateTutorialsResponse, UpdateTutorialsRequest>(request, "updatetutorials");
        stateManager.HandleUpdateTutorialsResponse(response);
    }

    public async Task<List<AccountHero>> RequestSummons(int summonCount) {
        var summonRequest = new SummonRequest() {
            SummonCount = summonCount
        };
        var response = await MakeLambdaCall<SummonResponse, SummonRequest>(summonRequest, "summonhero");
        stateManager.HandleSummonResponse(response);
        return response.SummonedHeroes;
    }

    public async Task<List<AccountHero>> RequestSummons(FactionEnum faction, int rarity, int summonCount) {
        var summonRequest = new FactionSummonRequest() {
            ChosenFaction = faction,
            SummonCount = summonCount,
            SummonRarity = rarity
        };
        var response = await MakeLambdaCall<FactionSummonResponse, FactionSummonRequest>(summonRequest, "summonfactionhero");
        stateManager.HandleSummonResponse(response);
        return response.SummonedHeroes;
    }

    public async Task<bool> RequestLevelup(AccountHero selectedHero) {
        if (selectedHero.CurrentLevel >= LevelContainer.MaxLevelForAwakeningValue(selectedHero.AwakeningLevel)) {
            return false;
        }

        long cost = LevelContainer.HeroExperienceRequirement(selectedHero.CurrentLevel);
        var currentState = stateManager.CurrentAccountState;
        if (currentState.CurrentGold < cost || currentState.CurrentSouls < cost) {
            return false;
        }

        var request = new LevelupHeroRequest() {
            AccountHeroId = selectedHero.Id
        };
        var response = await MakeLambdaCall<LevelupHeroResponse, LevelupHeroRequest>(request, "leveluphero");
        stateManager.HandleLevelupResponse(response, selectedHero);
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
        var response = await MakeLambdaCall<FuseHeroResponse, FuseHeroRequest>(request, "fusehero");
        if (!response.FusionSuccessful) return false;
        stateManager.HandleFuseResponse(response, fusedHero, destroyedHeroes);
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
        var response = await MakeLambdaCall<FuseEquipmentResponse, FuseEquipmentRequest>(request, "fuseequipment");
        if (!response.FusionSuccessful) return false;
        stateManager.HandleFuseResponse(response, fusedEquipment, destroyedEquipment);
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
        var response = await MakeLambdaCall<EquipResponse, EquipRequest>(request, "equiphero");
        stateManager.HandleEquipResponse(response, equipment, hero, slot);
    }

    public async Task UnequipHero(AccountHero hero) {
        var request = new UnequipHeroRequest() {
            HeroId = hero.Id
        };
        await MakeLambdaCall<UnequipHeroResponse, UnequipHeroRequest>(request, "unequiphero");
        stateManager.HandleUnequipResponse(hero);
    }

    public async Task<CombatResponse> PerformEpicBattle(BattleEnum battleType, AccountHero[] selectedHeroes) {
        var selectedGuids = new Guid[selectedHeroes.Length];
        for (int x = 0; x < selectedHeroes.Length; x++) {
            if (selectedHeroes[x] != null) selectedGuids[x] = selectedHeroes[x].Id;
            else selectedGuids[x] = Guid.Empty;
        }
        var request = new CombatRequest() {
            EncounterType = battleType,
            SelectedHeroes = selectedGuids
        };
        var response = await MakeLambdaCall<CombatResponse, CombatRequest>(request, "combat");
        if (response != null && response.Report != null) {
            response.Report.RestoreUnserializedData();
            if (response.Rewards != null) response.Rewards.RestoreUnserializedData();
        }
        stateManager.HandleCombatResponse(battleType, response);
        return response;
    }

    public async Task<LootCaveEncounter> RequestCaveEncounter() {
        var response = await MakeLambdaCall<LootCaveEncounterResponse>("lootcaveencounter");
        stateManager.HandleCaveEncounterResponse(response);
        return response.Encounter;
    }

    #endregion

    #region Account altering requests.

    public async Task<bool> CreateAccount(string email, string password) {
        var request = new CreateLoginRequest() {
            Email = email,
            Password = password
        };
        var response = await MakeLambdaCall<CreateLoginResponse, CreateLoginRequest>(request, "createlogin");
        stateManager.HandleCreateLoginResponse(response);
        return response.Success;
    }

    public async Task<bool> ConfirmEmail(int verificationCode) {
        var request = new VerifyEmailRequest() {
            VerificationCode = verificationCode
        };
        var response = await MakeLambdaCall<VerifyEmailResponse, VerifyEmailRequest>(request, "verifyemail");
        stateManager.HandleVerifyEmailResponse(response);
        return response.Success;
    }

    public async Task<bool> LoginUser(string email, string password) {
        var request = new LoginRequest() {
            Email = email,
            Password = password
        };

        var httpContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var httpResponse = await identityClient.PostAsync("login", httpContent);
        if (httpResponse.Content != null && httpResponse.IsSuccessStatusCode) {
            var response = JsonConvert.DeserializeObject<LoginResponse>(await httpResponse.Content.ReadAsStringAsync());
            if (response.Token != null && response.EmailVerified) {
                cachedToken = response.Token;
                SaveUserSession();
                await InitializeEverything();
                return true;
            } else {
                return false;
            }
        } else {
            Debug.Log("Unable to get a new user token: " + httpResponse.StatusCode + ", " + httpResponse.RequestMessage);
            return false;
        }
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