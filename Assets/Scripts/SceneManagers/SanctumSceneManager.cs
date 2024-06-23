using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Com.Tempest.Whale.GameObjects;

public class SanctumSceneManager : MonoBehaviour {

    public Canvas mainCanvas;

    public PlayerInfoPanelManager infoPanel;

    public Text timeElapsedLabel;

    public Text goldRateLabel;

    public TooltipPopup tooltipPrefab;
    public LoadingPopup loadingPrefab;

    private StateManager stateManager;

    public void Start() {
        stateManager = FindObjectOfType<StateManager>();
        if (!stateManager.CurrentAccountState.HasEnteredSanctum) {
            var tooltip = Instantiate(tooltipPrefab, mainCanvas.transform);
            tooltip.SetTooltip("This is the Sanctum.", "This is where you claim materials which you can use to upgrade your heroes and purchase things you need.\nProgressing through the campaign will increase the rate at which you accumulate materials.");
            stateManager.CurrentAccountState.HasEnteredSanctum = true;
            _ = FindObjectOfType<CredentialsManager>().UpdateTutorials();
        }
    }

    void Update() {

    }

    private bool ButtonsBlocked() {
        return FindObjectOfType<TooltipPopup>() != null || FindObjectOfType<LoadingPopup>() != null;
    }

    public void OnClaimRewards() {
        if (ButtonsBlocked()) return;
        var popup = Instantiate(loadingPrefab, mainCanvas.transform);
        popup.LaunchPopup("Loading...", "Contacting server...");
        try {
            var credentialsManager = FindObjectOfType<CredentialsManager>();
            //await credentialsManager.ClaimResources();
            popup.DismissPopup();
            infoPanel.NotifyUpdate();
        } catch (Exception e) {
            Debug.LogError(e);
            CredentialsManager.DisplayNetworkError(mainCanvas, "There was an error claiming your resources.");
        }
    }

    public void OnBackPressed() {
        SceneManager.LoadSceneAsync("HubScene");
    }
}
