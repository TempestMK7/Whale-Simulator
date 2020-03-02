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
        levelText.text = "Level " + accountState.currentLevel;
        gemText.text = accountState.currentGems.ToString();
        goldText.text = accountState.currentGold.ToString();
        soulsText.text = accountState.currentSouls.ToString();

        float currentValue = (float)(accountState.currentExperience / 100.0);
        levelBar.SetValue(currentValue, 100f);
    }
}
