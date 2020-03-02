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
        gemText.text = accountState.currentGems.ToString("0");
        goldText.text = accountState.currentGold.ToString("0");
        soulsText.text = accountState.currentSouls.ToString("0");
        levelBar.SetValue((float)accountState.currentExperience, (float)LevelContainer.experienceRequirement(accountState.currentLevel));
    }
}
