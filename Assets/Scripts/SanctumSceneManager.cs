using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SanctumSceneManager : MonoBehaviour {

    public PlayerInfoPanelManager infoPanel;

    void Start() {
        
    }

    void Update() {
        
    }

    public void OnClaimRewards() {
        var state = StateManager.GetCurrentState();
        state.ClaimMaterials();
        infoPanel.NotifyUpdate();
    }

    public void OnBackPressed() {
        SceneManager.LoadScene("HubScene");
    }
}
