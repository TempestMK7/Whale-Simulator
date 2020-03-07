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
    public Image factionIconLeft;
    public Image factionIconRight;
    public Text fuseButtonText;
    public GameObject statPanel;
    public GameObject fusePanel;

    public RarityBehavior rarityView;
    public Text levelLabel;
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

    public GameObject heroListItemPrefab;

    private FactionEnum? currentFilter;
    private List<AccountHero> unfilteredList;
    private List<AccountHero> filteredList;

    private int currentPosition;

    public void Awake() {
        var state = StateManager.GetCurrentState();
        unfilteredList = state.AccountHeroes;
        BuildList();
        masterContainer.SetActive(true);
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
        detailContainer.SetActive(true);
        BindDetailView();
    }

    public void OnBackPressed() {
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
        detailContainer.SetActive(false);
        masterContainer.SetActive(true);
        BuildList();
    }

    public void OnPageLeftPressed() {
        if (currentPosition > 0) currentPosition--;
        BindDetailView();
    }

    public void OnPageRightPressed() {
        if (currentPosition < filteredList.Count - 1) currentPosition++;
        BindDetailView();
    }

    public void OnFusePressed() {
        ToggleStatPanel(!statPanel.activeSelf);
    }

    public void OnLevelUpPressed() {
        levelUpSound.time = 0.2f;
        levelUpSound.Play();
        StateManager.LevelUpHero(filteredList[currentPosition]);
        BindDetailView();
    }

    public void BindDetailView() {
        var state = StateManager.GetCurrentState();
        var currentHero = filteredList[currentPosition];
        var combatHero = currentHero.GetCombatHero();
        var baseHero = combatHero.Base;
        var currentLevel = combatHero.CurrentLevel;

        heroLabel.text = baseHero.HeroName;
        factionIconLeft.sprite = FactionContainer.GetIconForFaction(baseHero.Faction);
        factionIconRight.sprite = FactionContainer.GetIconForFaction(baseHero.Faction);

        levelLabel.text = string.Format("Level: {0}", currentLevel);
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

        int currentHeroRequirement = 0;
        int factionHeroRequirement = 0;

        int currentHeroLevelRequirement = 0;
        int factionHeroLevelRequirement = 0;

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
        topLeftFusion.SetEmpty(baseHero.Faction, currentHeroLevelRequirement, baseHero.Hero);
        topRightFusion.gameObject.SetActive(currentHeroRequirement >= 2);
        topRightFusion.SetEmpty(baseHero.Faction, currentHeroLevelRequirement, baseHero.Hero);
        bottomLeftFusion.gameObject.SetActive(factionHeroRequirement >= 1);
        bottomLeftFusion.SetEmpty(baseHero.Faction, factionHeroLevelRequirement, null);
        bottomRightFusion.gameObject.SetActive(factionHeroRequirement >= 2);
        bottomRightFusion.SetEmpty(baseHero.Faction, factionHeroLevelRequirement, null);
        bottomMiddleFusion.gameObject.SetActive(factionHeroRequirement >= 3);
        bottomMiddleFusion.SetEmpty(baseHero.Faction, factionHeroLevelRequirement, null);
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
        List<AccountHero> alreadySelected = GetSelectedFusionHeroes();
        alreadySelected.Add(centerFusion.GetSelectedHero());
        var selected = fusionButton.GetSelectedHero();
        if (selected != null && alreadySelected.Contains(selected)) alreadySelected.Remove(selected);
        Debug.Log("Requesting popup: " + alreadySelected.Count);
    }
}
