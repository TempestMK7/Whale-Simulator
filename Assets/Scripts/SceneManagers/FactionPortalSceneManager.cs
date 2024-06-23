using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Com.Tempest.Whale.GameObjects;
using Com.Tempest.Whale.StateObjects;

class FactionPortalSceneManager : MonoBehaviour {

    public Canvas mainCanvas;

    public Text nameText;
    public Text bronzeText;
    public Text silverText;
    public Text goldText;
    public Text selectionText;

    public GameObject singleSummonPopupPrefab;
    public GameObject tenSummonPopupPrefab;
    public LoadingPopup loadingPopupPrefab;

    public ParticleSystem portalParticle;
    public AudioSource portalOpenEffect;
    public AudioSource portalLoopEffect;

    private StateManager stateManager;
    private FactionEnum factionSelection = FactionEnum.WATER;

    public void Awake() {
        stateManager = FindObjectOfType<StateManager>();

        ResetTextValues();

        portalOpenEffect.volume = SettingsManager.GetInstance().effectVolume * 0.5f;
        portalOpenEffect.PlayDelayed(0.3f);
        portalLoopEffect.volume = SettingsManager.GetInstance().effectVolume * 0.5f;
        portalLoopEffect.Play();
        portalParticle.Play();
    }

    private void ResetTextValues() {
        nameText.text = stateManager.CurrentAccountState.PlayerName;

        string selection = "None";
        switch (factionSelection) {
            case FactionEnum.WATER:
                selection = "Water";
                break;
            case FactionEnum.GRASS:
                selection = "Nature";
                break;
            case FactionEnum.FIRE:
                selection = "Fire";
                break;
            case FactionEnum.ICE:
                selection = "Ice";
                break;
            case FactionEnum.EARTH:
                selection = "Earth";
                break;
            case FactionEnum.ELECTRIC:
                selection = "Electric";
                break;
        }
        selectionText.text = string.Format("Selected: {0}", selection);
    }

    public void OnBackPressed() {
        if (PopupExists()) return;
        SceneManager.LoadSceneAsync("HubScene");
    }

    private bool PopupExists() {
        return FindObjectOfType<SummonPopupBehavior>() != null || FindObjectOfType<TenSummonPopupBehavior>() != null || FindObjectOfType<LoadingPopup>() != null;
    }

    public void OnFactionPressed(int faction) {
        if (PopupExists()) return;
        factionSelection = ((FactionEnum[])Enum.GetValues(typeof(FactionEnum)))[faction];
        ResetTextValues();
    }

    public void OnBronzeSummonPressed(int count) {

    }

    public void OnSilverSummonPressed(int count) {

    }

    public void OnGoldSummonPressed(int count) {

    }

    public void OnSummonPressed(int rarity, int count) {
        /*if (PopupExists()) return;

        var loadingPopup = Instantiate(loadingPopupPrefab, mainCanvas.transform);
        loadingPopup.LaunchPopup("Summoning...", "Finding heroes who are willing to join your team...", false);

        try {
            var credentialsManager = FindObjectOfType<CredentialsManager>();
            List<AccountHero> summonedHeroes = await credentialsManager.RequestSummons(factionSelection, rarity, count);

            loadingPopup.DismissPopup(false);
            ResetTextValues();
            OnSummonReceived(summonedHeroes);
        } catch (Exception e) {
            Debug.LogError(e);
            CredentialsManager.DisplayNetworkError(mainCanvas, "There was an error summoning your heroes.");
        }*/
    }

    public void OnSummonReceived(List<AccountHero> summonedHeroes) {
        if (summonedHeroes.Count == 1) {
            var hero = summonedHeroes[0];
            var popup = Instantiate(singleSummonPopupPrefab, mainCanvas.transform);
            var behavior = popup.GetComponent<SummonPopupBehavior>();
            behavior.LaunchPopup(hero);
        } else if (summonedHeroes.Count == 10) {
            var popup = Instantiate(tenSummonPopupPrefab, mainCanvas.transform);
            var behavior = popup.GetComponent<TenSummonPopupBehavior>();
            behavior.LaunchPopup(summonedHeroes);
        }
    }
}

