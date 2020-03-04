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
        nameText.text = accountState.PlayerName;
        levelText.text = "Level " + accountState.CurrentLevel.ToString("0");
        gemText.text = CustomFormatter.Format(accountState.CurrentGems);
        goldText.text = CustomFormatter.Format(accountState.CurrentGold);
        soulsText.text = CustomFormatter.Format(accountState.CurrentSouls);
        levelBar.SetValue((float)accountState.CurrentExperience, (float)LevelContainer.experienceRequirement(accountState.CurrentLevel));
    }
}
