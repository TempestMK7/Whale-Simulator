using UnityEngine;
using UnityEngine.UI;
using Com.Tempest.Whale.GameObjects;

public class PlayerInfoPanelManager : MonoBehaviour {

    public Text nameText;
    public Text gemText;
    public Text goldText;

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
        gemText.text = CustomFormatter.Format(stateManager.CurrentAccountState.CurrentGems);
        goldText.text = CustomFormatter.Format(stateManager.CurrentAccountState.CurrentGold);
    }
}
