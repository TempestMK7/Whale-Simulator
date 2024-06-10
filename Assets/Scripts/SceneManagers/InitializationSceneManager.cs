using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Com.Tempest.Whale.GameObjects;
using Com.Tempest.Whale.ResourceContainers;

public class InitializationSceneManager : MonoBehaviour {

    public GameObject loadingPopupPrefab;
    public GameObject tooltipPopupPrefab;
    public ClearDataPopup clearDataPrefab;

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

        try {
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
            if (StateManager.GetCurrentState() == null) {
                throw new Exception("State is null.");
            }
            loadingPopup.SetText("Step 4 of 4.", "Launching world hub...");
            SceneManager.LoadSceneAsync("HubScene");
        } catch (Exception e) {
            initializing = false;
            Debug.LogError(e);
            CredentialsManager.DisplayNetworkError(mainCanvas, e.Message);
        }
    }

    public void LaunchClearDataPopup() {
        var popup = Instantiate(clearDataPrefab, mainCanvas.transform);
        popup.LaunchPopup();
    }
}
