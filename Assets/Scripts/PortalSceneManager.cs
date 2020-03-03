using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PortalSceneManager : MonoBehaviour {

    public Text nameText;
    public Text levelText;
    public Text summonText;

    private AccountStateContainer state;

    public void Awake() {
        state = StateManager.GetCurrentState();
        nameText.text = state.playerName;
        levelText.text = string.Format("Level {0}", state.currentLevel);
        summonText.text = state.currentScrolls.ToString("0");
    }

    public void OnBackPressed() {
        SceneManager.LoadScene("HubScene");
    }

    public void OnSummon() {

    }

    public void OnSummonTen() {

    }
}
