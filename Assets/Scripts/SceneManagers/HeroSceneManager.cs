using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HeroSceneManager : MonoBehaviour {

    public GameObject masterContainer;
    public RectTransform heroListContent;

    public GameObject detailContainer;
    public Text heroLabel;
    public Text positionLabel;
    public Image factionIconLeft;
    public Image factionIconRight;
    public Text fuseButtonText;
    public GameObject statPanel;
    public GameObject fusePanel;

    public GameObject heroAnimation;

    public RarityBehavior rarityView;
    public Text levelLabel;
    public UnityEngine.UI.Button levelButton;
    public Text currentGold;
    public Text currentSouls;
    public Text goldCost;
    public Text soulsCost;

    public Text healthLabel;
    public Text defenseLabel;
    public Text reflectionLabel;
    public Text speedLabel;
    public Text attackLabel;
    public Text magicLabel;

    public AudioSource levelUpSound;

    public FusionSelectionBehavior centerFusion;
    public FusionSelectionBehavior topLeftFusion;
    public FusionSelectionBehavior topRightFusion;
    public FusionSelectionBehavior bottomLeftFusion;
    public FusionSelectionBehavior bottomMiddleFusion;
    public FusionSelectionBehavior bottomRightFusion;
    public UnityEngine.UI.Button completeFusionButton;

    public GameObject heroListItemPrefab;
    public GameObject fusionPopupPrefab;

    private FactionEnum? currentFilter;
    private List<AccountHero> unfilteredList;
    private List<AccountHero> filteredList;

    private int currentPosition;

    private int currentHeroRequirement;
    private int factionHeroRequirement;
    private int currentHeroLevelRequirement;
    private int factionHeroLevelRequirement;

    public void Awake() {
        var state = StateManager.GetCurrentState();
        unfilteredList = state.AccountHeroes;
        BuildList();
        masterContainer.SetActive(true);
        heroAnimation.SetActive(false);
        detailContainer.SetActive(false);
        levelUpSound.volume = SettingsManager.GetInstance().effectVolume * 0.5f;
    }

    // Master List Stuff

    private void BuildList() {
        for (int x = heroListContent.childCount - 1; x >= 0; x--) {
            Destroy(heroListContent.GetChild(x).gameObject);
        }

        filteredList = FilterList();
        var listItemTransform = heroListItemPrefab.transform as RectTransform;
        var listItemWidth = listItemTransform.rect.width;
        var listItemHeight = listItemTransform.rect.height;
        var listAreaWidth = heroListContent.rect.width;

        int numItemsPerRow = (int)(listAreaWidth / (listItemWidth + 8));
        int numRows = filteredList.Count / numItemsPerRow;
        if (filteredList.Count % numItemsPerRow != 0) numRows++;
        float anchorMultiple = 1f / (numItemsPerRow + 1f);

        int heightPerRow = (int)listItemHeight + 8;
        int totalHeight = numRows * heightPerRow;
        heroListContent.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, totalHeight);

        for (int x = 0; x < filteredList.Count; x++) {
            var hero = filteredList[x];
            var listItem = Instantiate(heroListItemPrefab);
            listItem.GetComponent<HeroListItemBehavior>().SetHero(hero, x);
            listItem.GetComponent<HeroListItemBehavior>().SetHeroSceneManager(this);
            listItem.transform.SetParent(heroListContent);
            var transform = listItem.transform as RectTransform;
            float rowPosition = x % numItemsPerRow;
            transform.anchorMin = new Vector2((rowPosition + 1) * anchorMultiple, 1f);
            transform.anchorMax = transform.anchorMin;
            int rowNum = x / numItemsPerRow;
            float verticalPosition = (rowNum + 0.5f) * heightPerRow * -1f;
            transform.anchoredPosition = new Vector2(0f, verticalPosition);
        }
    }

    private List<AccountHero> FilterList() {
        if (currentFilter == null) return new List<AccountHero>(unfilteredList);
        List<AccountHero> filteredList = new List<AccountHero>();
        foreach (AccountHero hero in unfilteredList) {
            if (hero.GetBaseHero().Faction == currentFilter) filteredList.Add(hero);
        }
        return filteredList;
    }

    public void NotifyListSelection(int listPosition) {
        currentPosition = listPosition;
        masterContainer.SetActive(false);
        heroAnimation.SetActive(true);
        detailContainer.SetActive(true);
        BindDetailView();
    }

    private bool ButtonsBlocked() {
        return FindObjectOfType<FusionPopupBehavior>() != null;
    }

    public void OnBackPressed() {
        if (ButtonsBlocked()) return;
        SceneManager.LoadScene("HubScene");
    }

    public void OnFilterPressed(int filterPosition) {
        FactionEnum faction = (FactionEnum)Enum.GetValues(typeof(FactionEnum)).GetValue(filterPosition);
        if (currentFilter == faction) {
            currentFilter = null;
        } else {
            currentFilter = faction;
        }
        BuildList();
    }

    // Detail Screen Stuff

    public void OnDetailBackPressed() {
        if (ButtonsBlocked()) return;
        detailContainer.SetActive(false);
        heroAnimation.SetActive(false);
        masterContainer.SetActive(true);
        BuildList();
    }

    public void OnPageLeftPressed() {
        if (ButtonsBlocked()) return;
        if (currentPosition > 0) currentPosition--;
        BindDetailView();
    }

    public void OnPageRightPressed() {
        if (ButtonsBlocked()) return;
        if (currentPosition < filteredList.Count - 1) currentPosition++;
        BindDetailView();
    }

    public void OnFusePressed() {
        if (ButtonsBlocked()) return;
        ToggleStatPanel(!statPanel.activeSelf);
    }

    public void OnLevelUpPressed() {
        if (ButtonsBlocked()) return;
        StateManager.LevelUpHero(filteredList[currentPosition], OnLevelUpComplete);
    }

    public void OnLevelUpComplete(bool successful) {
        if (!successful) return;
        levelUpSound.time = 0.2f;
        levelUpSound.Play();
        ResetListPosition();
        BindDetailView();
    }

    private void ResetListPosition() {
        var selected = filteredList[currentPosition];
        filteredList = FilterList();
        currentPosition = filteredList.IndexOf(selected);
    }

    public void BindDetailView() {
        var state = StateManager.GetCurrentState();
        var currentHero = filteredList[currentPosition];
        var combatHero = currentHero.GetCombatHero();
        var baseHero = combatHero.Base;
        var currentLevel = combatHero.CurrentLevel;

        heroLabel.text = baseHero.HeroName;
        positionLabel.text = string.Format("({0} of {1})", currentPosition + 1, filteredList.Count);
        factionIconLeft.sprite = FactionContainer.GetIconForFaction(baseHero.Faction);
        factionIconRight.sprite = FactionContainer.GetIconForFaction(baseHero.Faction);

        var animator = baseHero.HeroAnimator;
        heroAnimation.GetComponent<Animator>().runtimeAnimatorController = animator;

        levelLabel.text = string.Format("Level: {0}", currentLevel);
        levelButton.gameObject.SetActive(currentLevel < LevelContainer.MaxLevelForAwakeningValue(currentHero.AwakeningLevel));
        currentGold.text = CustomFormatter.Format(state.CurrentGold);
        currentSouls.text = CustomFormatter.Format(state.CurrentSouls);
        goldCost.text = CustomFormatter.Format(LevelContainer.HeroExperienceRequirement(currentLevel));
        soulsCost.text = CustomFormatter.Format(LevelContainer.HeroExperienceRequirement(currentLevel));

        healthLabel.text = string.Format("Health: {0}", combatHero.Health.ToString("0"));
        attackLabel.text = string.Format("Attack: {0}", combatHero.Attack.ToString("0"));
        magicLabel.text = string.Format("Magic: {0}", combatHero.Magic.ToString("0"));
        defenseLabel.text = string.Format("Defense: {0}", combatHero.Defense.ToString("0.0"));
        reflectionLabel.text = string.Format("Reflection: {0}", combatHero.Reflection.ToString("0.0"));
        speedLabel.text = string.Format("Speed: {0}", combatHero.Speed.ToString("0"));

        rarityView.SetLevel(baseHero.Rarity, currentHero.AwakeningLevel, true);
        ToggleStatPanel(true);
    }

    private void ToggleStatPanel(bool showStats) {
        fuseButtonText.text = showStats ? "Fuse!" : "Stats";
        statPanel.SetActive(showStats);
        fusePanel.SetActive(!showStats);

        if (!showStats) SetupFusePanel();
    }

    private void SetupFusePanel() {
        var currentHero = filteredList[currentPosition];
        var baseHero = currentHero.GetBaseHero();

        switch (currentHero.AwakeningLevel) {
            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
                currentHeroRequirement = 2;
                factionHeroRequirement = 2;
                currentHeroLevelRequirement = currentHero.AwakeningLevel;
                factionHeroLevelRequirement = currentHero.AwakeningLevel;
                break;
            case 6:
                currentHeroRequirement = 0;
                factionHeroRequirement = 1;
                currentHeroLevelRequirement = 5;
                factionHeroLevelRequirement = 6;
                break;
            case 7:
                currentHeroRequirement = 0;
                factionHeroRequirement = 2;
                currentHeroLevelRequirement = 5;
                factionHeroLevelRequirement = 6;
                break;
            case 8:
                currentHeroRequirement = 0;
                factionHeroRequirement = 3;
                currentHeroLevelRequirement = 5;
                factionHeroLevelRequirement = 6;
                break;
            case 9:
                currentHeroRequirement = 2;
                factionHeroRequirement = 2;
                currentHeroLevelRequirement = 5;
                factionHeroLevelRequirement = 6;
                break;
        }

        centerFusion.SetAccountHero(currentHero);

        topLeftFusion.gameObject.SetActive(currentHeroRequirement >= 1);
        topLeftFusion.SetCardRequirements(baseHero.Faction, currentHeroLevelRequirement, baseHero.Hero);
        topLeftFusion.SetEmpty();
        topRightFusion.gameObject.SetActive(currentHeroRequirement >= 2);
        topRightFusion.SetCardRequirements(baseHero.Faction, currentHeroLevelRequirement, baseHero.Hero);
        topRightFusion.SetEmpty();
        bottomLeftFusion.gameObject.SetActive(factionHeroRequirement >= 1);
        bottomLeftFusion.SetCardRequirements(baseHero.Faction, factionHeroLevelRequirement, null);
        bottomLeftFusion.SetEmpty();
        bottomRightFusion.gameObject.SetActive(factionHeroRequirement >= 2);
        bottomRightFusion.SetCardRequirements(baseHero.Faction, factionHeroLevelRequirement, null);
        bottomRightFusion.SetEmpty();
        bottomMiddleFusion.gameObject.SetActive(factionHeroRequirement >= 3);
        bottomMiddleFusion.SetCardRequirements(baseHero.Faction, factionHeroLevelRequirement, null);
        bottomMiddleFusion.SetEmpty();

        HandleCompleteFusionButton();
    }

    private List<AccountHero> GetSelectedFusionHeroes() {
        List<AccountHero> selected = new List<AccountHero>();
        if (topLeftFusion.GetSelectedHero() != null) selected.Add(topLeftFusion.GetSelectedHero());
        if (topRightFusion.GetSelectedHero() != null) selected.Add(topRightFusion.GetSelectedHero());
        if (bottomLeftFusion.GetSelectedHero() != null) selected.Add(bottomLeftFusion.GetSelectedHero());
        if (bottomMiddleFusion.GetSelectedHero() != null) selected.Add(bottomMiddleFusion.GetSelectedHero());
        if (bottomRightFusion.GetSelectedHero() != null) selected.Add(bottomRightFusion.GetSelectedHero());
        return selected;
    }

    public void RequestPopup(FusionSelectionBehavior fusionButton, FactionEnum faction, int levelRequirement, HeroEnum? specificHero) {
        if (ButtonsBlocked()) return;
        List<AccountHero> alreadySelected = GetSelectedFusionHeroes();
        alreadySelected.Add(centerFusion.GetSelectedHero());
        var selected = fusionButton.GetSelectedHero();
        if (selected != null && alreadySelected.Contains(selected)) alreadySelected.Remove(selected);

        var popup = Instantiate(fusionPopupPrefab, detailContainer.transform).GetComponent<FusionPopupBehavior>();
        popup.LaunchPopup(faction, levelRequirement, specificHero, alreadySelected, fusionButton);
    }

    public void OnFusionHeroSelected() {
        HandleCompleteFusionButton();
    }

    private void HandleCompleteFusionButton() {
        var selected = filteredList[currentPosition];
        var destroyedHeroes = new List<AccountHero>();
        if (topLeftFusion.GetSelectedHero() != null) destroyedHeroes.Add(topLeftFusion.GetSelectedHero());
        if (topRightFusion.GetSelectedHero() != null) destroyedHeroes.Add(topRightFusion.GetSelectedHero());
        if (bottomLeftFusion.GetSelectedHero() != null) destroyedHeroes.Add(bottomLeftFusion.GetSelectedHero());
        if (bottomMiddleFusion.GetSelectedHero() != null) destroyedHeroes.Add(bottomMiddleFusion.GetSelectedHero());
        if (bottomRightFusion.GetSelectedHero() != null) destroyedHeroes.Add(bottomRightFusion.GetSelectedHero());
        completeFusionButton.gameObject.SetActive(StateManager.FusionIsLegal(selected, destroyedHeroes));
    }

    public void RequestFusion() {
        if (ButtonsBlocked()) return;
        var selected = filteredList[currentPosition];
        var destroyedHeroes = new List<AccountHero>();
        if (topLeftFusion.GetSelectedHero() != null) destroyedHeroes.Add(topLeftFusion.GetSelectedHero());
        if (topRightFusion.GetSelectedHero() != null) destroyedHeroes.Add(topRightFusion.GetSelectedHero());
        if (bottomLeftFusion.GetSelectedHero() != null) destroyedHeroes.Add(bottomLeftFusion.GetSelectedHero());
        if (bottomMiddleFusion.GetSelectedHero() != null) destroyedHeroes.Add(bottomMiddleFusion.GetSelectedHero());
        if (bottomRightFusion.GetSelectedHero() != null) destroyedHeroes.Add(bottomRightFusion.GetSelectedHero());
        if (!StateManager.FusionIsLegal(selected, destroyedHeroes)) return;
        StateManager.FuseHero(selected, destroyedHeroes, OnFusionComplete);
    }

    public void OnFusionComplete(bool successful) {
        Debug.Log("Attempted fusion: " + successful);
        if (!successful) return;
        ResetListPosition();
        BindDetailView();
        StartCoroutine("FusionFanfare");
    }

    public void OnSuggestFusion() {
        SetupFusePanel();
        var alreadySelected = new List<AccountHero>();
        alreadySelected.Add(filteredList[currentPosition]);

        if (currentHeroRequirement >= 1) {
            SelectSameHero(topLeftFusion, alreadySelected);
        }
        if (currentHeroRequirement >= 2) {
            SelectSameHero(topRightFusion, alreadySelected);
        }
        if (factionHeroRequirement >= 1) {
            SelectFactionHero(bottomLeftFusion, alreadySelected);
        }
        if (factionHeroRequirement >= 2) {
            SelectFactionHero(bottomRightFusion, alreadySelected);
        }
        if (factionHeroRequirement >= 3) {
            SelectFactionHero(bottomMiddleFusion, alreadySelected);
        }
        HandleCompleteFusionButton();
    }

    private void SelectSameHero(FusionSelectionBehavior fusion, List<AccountHero> alreadySelected) {
        var baseHero = filteredList[currentPosition].GetBaseHero();
        var allHeroes = StateManager.GetCurrentState().AccountHeroes;
        var firstSelectable = allHeroes.Find(delegate (AccountHero hero) {
            return !alreadySelected.Contains(hero) && hero.GetBaseHero().Hero == baseHero.Hero && hero.AwakeningLevel == currentHeroLevelRequirement;
        });
        if (firstSelectable != null) {
            fusion.SetAccountHero(firstSelectable);
            alreadySelected.Add(firstSelectable);
        }
    }

    private void SelectFactionHero(FusionSelectionBehavior fusion, List<AccountHero> alreadySelected) {
        var baseHero = filteredList[currentPosition].GetBaseHero();
        var allHeroes = StateManager.GetCurrentState().AccountHeroes;
        var firstSelectable = allHeroes.Find(delegate (AccountHero hero) {
            return !alreadySelected.Contains(hero) && hero.GetBaseHero().Faction == baseHero.Faction && hero.AwakeningLevel == factionHeroLevelRequirement;
        });
        if (firstSelectable != null) {
            fusion.SetAccountHero(firstSelectable);
            alreadySelected.Add(firstSelectable);
        }
    }

    IEnumerator FusionFanfare() {
        yield return null;
    }
}
