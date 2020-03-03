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

    public void Awake() {
        BindStateToUi();
    }

    public void Update() {
        
    }

    public void NotifyUpdate() {
        BindStateToUi();
    }

    private void BindStateToUi() {
        AccountStateContainer accountState = StateManager.GetCurrentState();
        nameText.text = accountState.playerName;
        levelText.text = "Level " + accountState.currentLevel.ToString("0");
        gemText.text = CustomFormatter.Format(accountState.currentGems);
        goldText.text = CustomFormatter.Format(accountState.currentGold);
        soulsText.text = CustomFormatter.Format(accountState.currentSouls);
        levelBar.SetValue((float)accountState.currentExperience, (float)LevelContainer.experienceRequirement(accountState.currentLevel));
    }
}
