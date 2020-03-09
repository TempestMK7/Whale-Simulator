using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoPanelManager : MonoBehaviour {

    public Text nameText;
    public Text levelText;
    public Text gemText;
    public Text goldText;
    public Text soulsText;
    public ProgressBarPro levelBar;

    private int displayedLevel;

    public void Awake() {
        BindStateToUi();
    }

    public void NotifyUpdate() {
        BindStateToUi();
    }

    private void BindStateToUi() {
        AccountStateContainer accountState = StateManager.GetCurrentState();
        nameText.text = accountState.PlayerName;
        levelText.text = "Level " + accountState.CurrentLevel.ToString("0");
        gemText.text = CustomFormatter.Format(accountState.CurrentGems);
        goldText.text = CustomFormatter.Format(accountState.CurrentGold);
        soulsText.text = CustomFormatter.Format(accountState.CurrentSouls);

        HandleLevelBar();
    }

    private void HandleLevelBar() {
        AccountStateContainer accountState = StateManager.GetCurrentState();
        if (displayedLevel == 0) {
            levelBar.SetValue((float)accountState.CurrentExperience, (float)LevelContainer.ExperienceRequirement(accountState.CurrentLevel));
            displayedLevel = accountState.CurrentLevel;
        } else {
            if (levelBar.Value == 1f) {
                levelBar.EmptyBar();
            }

            var differential = accountState.CurrentLevel - displayedLevel;
            if (differential > 0) {
                displayedLevel++;
                levelBar.SetValue(100f, 100f, HandleLevelBar);
            } else {
                levelBar.SetValue((float)accountState.CurrentExperience, (float)LevelContainer.ExperienceRequirement(accountState.CurrentLevel));
            }
        }
    }
}
