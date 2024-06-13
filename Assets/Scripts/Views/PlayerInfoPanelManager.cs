using UnityEngine;
using UnityEngine.UI;
using Com.Tempest.Whale.GameObjects;

public class PlayerInfoPanelManager : MonoBehaviour {

    public Text nameText;
    public Text levelText;
    public Text gemText;
    public Text goldText;
    public Text soulsText;
    public ProgressBarPro levelBar;

    private int displayedLevel;

    private StateManager stateManager;

    public void Awake() {
        stateManager = FindObjectOfType<StateManager>();
        BindStateToUi();
    }

    public void NotifyUpdate() {
        BindStateToUi();
    }

    private void BindStateToUi() {
        nameText.text = stateManager.CurrentAccountState.PlayerName;
        levelText.text = "Level " + stateManager.CurrentAccountState.CurrentLevel.ToString("0");
        gemText.text = CustomFormatter.Format(stateManager.CurrentAccountState.CurrentGems);
        goldText.text = CustomFormatter.Format(stateManager.CurrentAccountState.CurrentGold);
        soulsText.text = CustomFormatter.Format(stateManager.CurrentAccountState.CurrentSouls);

        HandleLevelBar();
    }

    private void HandleLevelBar() {
        if (displayedLevel == 0) {
            levelBar.SetValue((float)stateManager.CurrentAccountState.CurrentExperience, (float)LevelContainer.ExperienceRequirement(stateManager.CurrentAccountState.CurrentLevel));
            displayedLevel = stateManager.CurrentAccountState.CurrentLevel;
        } else {
            if (levelBar.Value == 1f) {
                levelBar.EmptyBar();
            }

            var differential = stateManager.CurrentAccountState.CurrentLevel - displayedLevel;
            if (differential > 0) {
                displayedLevel++;
                levelBar.SetValue(100f, 100f, HandleLevelBar);
            } else {
                levelBar.SetValue((float)stateManager.CurrentAccountState.CurrentExperience, (float)LevelContainer.ExperienceRequirement(stateManager.CurrentAccountState.CurrentLevel));
            }
        }
    }
}
