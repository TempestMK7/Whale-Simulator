using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Com.Tempest.Whale.GameObjects;
using Com.Tempest.Whale.StateObjects;

public class PortalSceneManager : MonoBehaviour {

    public Canvas sceneUiCanvas;

    public Text nameText;
    public Text levelText;
    public Text summonText;

    public GameObject singleSummonPopupPrefab;
    public GameObject tenSummonPopupPrefab;
    public TooltipPopup tooltipPopupPrefab;
    public LoadingPopup loadingPopupPrefab;

    public ParticleSystem portalParticle;
    public AudioSource portalOpenEffect;
    public AudioSource portalLoopEffect;

    private StateManager stateManager;

    public void Awake() {
        stateManager = FindObjectOfType<StateManager>();
        nameText.text = stateManager.CurrentAccountState.PlayerName;
        summonText.text = CustomFormatter.Format(stateManager.CurrentAccountState.GetInventory(ItemEnum.RED_CRYSTAL).Quantity);
        portalOpenEffect.volume = SettingsManager.GetInstance().effectVolume * 0.5f;
        portalOpenEffect.PlayDelayed(0.3f);
        portalLoopEffect.volume = SettingsManager.GetInstance().effectVolume * 0.5f;
        portalLoopEffect.Play();
        portalParticle.Play();
    }

    public void Start() {
        if (!stateManager.CurrentAccountState.HasEnteredPortal) {
            var tooltip = Instantiate(tooltipPopupPrefab, sceneUiCanvas.transform);
            tooltip.SetTooltip("This is the Portal.", "This is where you summon new heroes.\nWe'll give you 10 basic summoning stones to get started.");
            stateManager.CurrentAccountState.HasEnteredPortal = true;
            _ = FindObjectOfType<CredentialsManager>().UpdateTutorials();
        }
    }

    public void OnBackPressed() {
        if (PopupExists()) return;
        SceneManager.LoadSceneAsync("HubScene");
    }

    public async void OnSummon(int count) {
        if (PopupExists()) return;
        var loadingPopup = Instantiate(loadingPopupPrefab, sceneUiCanvas.transform);
        loadingPopup.LaunchPopup("Summoning...", "Finding heroes who are willing to join your team...", false);
        try {
            var credentialsManager = FindObjectOfType<CredentialsManager>();
            List<AccountHero> summonedHeroes = await credentialsManager.RequestSummons(count);
            loadingPopup.DismissPopup(false);
            OnSummonReceived(summonedHeroes);
        } catch (Exception e) {
            Debug.LogError(e);
            CredentialsManager.DisplayNetworkError(sceneUiCanvas, "There was an error summoning your heroes.");
        }
    }

    public void OnSummonReceived(List<AccountHero> summonedHeroes) {
        summonText.text = CustomFormatter.Format(stateManager.CurrentAccountState.GetInventory(ItemEnum.RED_CRYSTAL).Quantity);
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
        return FindObjectOfType<SummonPopupBehavior>() != null || FindObjectOfType<TenSummonPopupBehavior>() != null || FindObjectOfType<TooltipPopup>() != null || FindObjectOfType<LoadingPopup>() != null;
    }
}
