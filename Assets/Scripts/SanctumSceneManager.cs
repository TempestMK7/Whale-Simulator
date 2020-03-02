using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SanctumSceneManager : MonoBehaviour {

    public PlayerInfoPanelManager infoPanel;

    public Text timeElapsedLabel;

    public Text goldRateLabel;
    public Text goldUnclaimedLabel;
    public Text soulsRateLabel;
    public Text soulsUnclaimedLabel;
    public Text experienceRateLabel;
    public Text experienceUnclaimedLabel;

    private AccountStateContainer state;

    void Awake() {
        this.state = StateManager.GetCurrentState();
    }

    void Update() {
        double unformattedTime = (double)(EpochTime.CurrentTimeMillis() - state.lastClaimTimeStamp) / 1000.0;

        goldRateLabel.text = string.Format("Gold: {0} / min.", state.goldRate * 60.0);
        goldUnclaimedLabel.text = (unformattedTime * state.goldRate).ToString("0");

        soulsRateLabel.text = string.Format("Souls: {0} / min.", state.soulsRate * 60.0);
        soulsUnclaimedLabel.text = (unformattedTime * state.soulsRate).ToString("0");

        experienceRateLabel.text = string.Format("Exp: {0} / min.", state.experienceRate * 60.0);
        experienceUnclaimedLabel.text = (unformattedTime * state.experienceRate).ToString("0");

        // This is a bit of a mess, I need to see if there's a time formatter out there that works.
        long elapsedTime = (EpochTime.CurrentTimeMillis() - state.lastClaimTimeStamp) / 1000;
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

    public void OnClaimRewards() {
        state.ClaimMaterials();
        infoPanel.NotifyUpdate();
    }

    public void OnBackPressed() {
        SceneManager.LoadScene("HubScene");
    }
}
