using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PortalSceneManager : MonoBehaviour {

    public Canvas sceneUiCanvas;

    public Text nameText;
    public Text levelText;
    public Text summonText;

    public GameObject singleSummonPopupPrefab;
    public GameObject tenSummonPopupPrefab;

    private AccountStateContainer state;

    public void Awake() {
        state = StateManager.GetCurrentState();
        nameText.text = state.PlayerName;
        levelText.text = string.Format("Level {0}", state.CurrentLevel);
        summonText.text = CustomFormatter.Format(state.CurrentSummons);
    }

    public void OnBackPressed() {
        if (PopupExists()) return;
        SceneManager.LoadScene("HubScene");
    }

    public void OnSummon() {
        if (PopupExists()) return;
        StateManager.RequestSummon(1, OnSummonReceived);
    }

    public void OnSummonTen() {
        if (PopupExists()) return;
        StateManager.RequestSummon(10, OnSummonReceived);
    }

    public void OnSummonReceived(List<AccountHero> summonedHeroes) {
        if (summonedHeroes.Count == 1) {
            var hero = summonedHeroes[0];
            var popup = Instantiate(singleSummonPopupPrefab, sceneUiCanvas.transform);
            var behavior = popup.GetComponent<SummonPopupBehavior>();
            behavior.LaunchPopup(hero);
        } else if (summonedHeroes.Count == 10) {
            var popup = Instantiate(tenSummonPopupPrefab, sceneUiCanvas.transform);
            var behavior = popup.GetComponent<TenSummonPopupBehavior>();
            behavior.LaunchPopup(summonedHeroes);
        }
    }

    private bool PopupExists() {
        return FindObjectsOfType<SummonPopupBehavior>().Length > 0 || FindObjectsOfType<TenSummonPopupBehavior>().Length > 0;
    }
}
