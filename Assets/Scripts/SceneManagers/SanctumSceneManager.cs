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
        double unformattedTime = (double)(EpochTime.CurrentTimeMillis() - state.LastClaimTimeStamp) / 1000.0;

        goldRateLabel.text = string.Format("Gold: {0} / min.", state.GoldRate * 60.0);
        goldUnclaimedLabel.text = CustomFormatter.Format(unformattedTime * state.GoldRate);

        soulsRateLabel.text = string.Format("Souls: {0} / min.", state.SoulsRate * 60.0);
        soulsUnclaimedLabel.text = CustomFormatter.Format(unformattedTime * state.SoulsRate);

        experienceRateLabel.text = string.Format("Exp: {0} / min.", state.ExperienceRate * 60.0);
        experienceUnclaimedLabel.text = CustomFormatter.Format(unformattedTime * state.ExperienceRate);

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

    public void OnClaimRewards() {
        StateManager.ClaimRewards(OnRewardsClaimed);
    }

    public void OnRewardsClaimed(object sender) {
        infoPanel.NotifyUpdate();
    }

    public void OnBackPressed() {
        SceneManager.LoadScene("HubScene");
    }
}
