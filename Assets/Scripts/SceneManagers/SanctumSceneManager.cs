﻿using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Com.Tempest.Whale.GameObjects;

public class SanctumSceneManager : MonoBehaviour {

    public Canvas mainCanvas;

    public PlayerInfoPanelManager infoPanel;

    public Text timeElapsedLabel;

    public Text goldRateLabel;
    public Text goldUnclaimedLabel;
    public Text soulsRateLabel;
    public Text soulsUnclaimedLabel;
    public Text experienceRateLabel;
    public Text experienceUnclaimedLabel;

    public TooltipPopup tooltipPrefab;
    public LoadingPopup loadingPrefab;

    public void Start() {
        var state = StateManager.GetCurrentState();
        if (!state.HasEnteredSanctum) {
            var tooltip = Instantiate(tooltipPrefab, mainCanvas.transform);
            tooltip.SetTooltip("This is the Sanctum.", "This is where you claim materials which you can use to upgrade your heroes and purchase things you need.\nProgressing through the campaign will increase the rate at which you accumulate materials.");
            state.HasEnteredSanctum = true;
            _ = FindObjectOfType<CredentialsManager>().UpdateTutorials();
        }
    }

    void Update() {
        var state = StateManager.GetCurrentState();
        var generation = MissionContainer.GetGenerationInfo(state);
        double unformattedTime = (EpochTime.CurrentTimeMillis() - state.LastClaimTimeStamp);

        goldRateLabel.text = string.Format("Gold: {0} / min.", generation.GoldPerMinute);
        goldUnclaimedLabel.text = CustomFormatter.Format(unformattedTime * GenerationInfo.GenerationPerMillisecond(generation.GoldPerMinute));

        soulsRateLabel.text = string.Format("Souls: {0} / min.", generation.SoulsPerMinute);
        soulsUnclaimedLabel.text = CustomFormatter.Format(unformattedTime * GenerationInfo.GenerationPerMillisecond(generation.SoulsPerMinute));

        experienceRateLabel.text = string.Format("Exp: {0} / min.", generation.ExperiencePerMinute);
        experienceUnclaimedLabel.text = CustomFormatter.Format(unformattedTime * GenerationInfo.GenerationPerMillisecond(generation.ExperiencePerMinute));

        // This is a bit of a mess, I need to see if there's a time formatter out there that works.
        long elapsedTime = (EpochTime.CurrentTimeMillis() - state.LastClaimTimeStamp) / 1000;
        long seconds = elapsedTime % 60;
        elapsedTime /= 60;
        long minutes = elapsedTime % 60;
        elapsedTime /= 60;
        long hours = elapsedTime % 24;
        elapsedTime /= 24;

        string formatted = "Unclaimed: ";
        if (elapsedTime > 0) {
            formatted += string.Format("{0}d ", elapsedTime);
        }
        if (hours > 0) {
            formatted += string.Format("{0}h ", hours);
        }
        if (minutes > 0) {
            formatted += string.Format("{0}m ", minutes);
        }
        formatted += string.Format("{0}s", seconds);
        timeElapsedLabel.text = formatted;
    }

    private bool ButtonsBlocked() {
        return FindObjectOfType<TooltipPopup>() != null || FindObjectOfType<LoadingPopup>() != null;
    }

    public async void OnClaimRewards() {
        if (ButtonsBlocked()) return;
        var popup = Instantiate(loadingPrefab, mainCanvas.transform);
        popup.LaunchPopup("Loading...", "Contacting server...");
        try {
            var credentialsManager = FindObjectOfType<CredentialsManager>();
            await credentialsManager.ClaimResources();
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
