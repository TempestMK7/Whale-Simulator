using UnityEngine;
using UnityEngine.SceneManagement;
using Com.Tempest.Whale.GameObjects;
using Com.Tempest.Whale.ResourceContainers;

public class InitializationSceneManager : MonoBehaviour {

    public GameObject loadingPopupPrefab;
    public Canvas mainCanvas;

    private bool initializing = false;

    public void Awake() {
        Application.targetFrameRate = 60;
    }

    public async void StartInitializing() {
        if (initializing) return;
        initializing = true;
        var loadingPopup = Instantiate(loadingPopupPrefab, mainCanvas.transform).GetComponent<LoadingPopup>();
        loadingPopup.LaunchPopup("Step 1 of 4.", "Loading assets...", false);
        BaseHeroContainer.Initialize();
        BaseEquipmentContainer.Initialize();
        AttackInfoContainer.Initialize();
        AbilityInfoContainer.Initialize();

        FactionIconContainer.Initialize();
        RoleIconContainer.Initialize();
        AttackParticleContainer.Initialize();
        RewardIconContainer.Initialize();

        SettingsManager.GetInstance();

        loadingPopup.SetText("Step 2 of 4.", "Contacting identity server...");
        var credentialsManager = FindObjectOfType<CredentialsManager>();
        await credentialsManager.InitializeEverything();
        loadingPopup.SetText("Step 3 of 4.", "Downloading account information...");
        await credentialsManager.DownloadState();
        loadingPopup.SetText("Step 4 of 4.", "Launching world hub...");
        SceneManager.LoadSceneAsync("HubScene");
    }
}
