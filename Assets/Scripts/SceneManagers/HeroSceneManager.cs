using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HeroSceneManager : MonoBehaviour {

    private const string emptyStar = "Icons/star_empty";
    private const string silverStar = "Icons/star_silver";
    private const string goldStar = "Icons/star_gold";

    public GameObject masterContainer;
    public RectTransform heroListContent;

    public GameObject detailContainer;
    public Text heroLabel;
    public Image factionIconLeft;
    public Image factionIconRight;

    public Image[] rarityStars;
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

    public GameObject heroListItemPrefab;

    private Sprite emptySprite;
    private Sprite silverSprite;
    private Sprite goldSprite;

    private FactionEnum? currentFilter;
    private List<AccountHero> unfilteredList;
    private List<AccountHero> filteredList;

    private int currentPosition;

    public void Awake() {
        emptySprite = Resources.Load<Sprite>(emptyStar);
        silverSprite = Resources.Load<Sprite>(silverStar);
        goldSprite = Resources.Load<Sprite>(goldStar);

        var state = StateManager.GetCurrentState();
        unfilteredList = state.AccountHeroes;
        BuildList();
        masterContainer.SetActive(true);
        detailContainer.SetActive(false);
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

    }

    public void OnLevelUpPressed() {
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

        healthLabel.text = string.Format("Health: {0}", combatHero.Health);
        attackLabel.text = string.Format("Attack: {0}", combatHero.Attack);
        magicLabel.text = string.Format("Magic: {0}", combatHero.Magic);
        defenseLabel.text = string.Format("Defense: {0}", combatHero.Defense);
        reflectionLabel.text = string.Format("Reflection: {0}", combatHero.Reflection);
        speedLabel.text = string.Format("Speed: {0}", combatHero.Speed);

        BindRarityPanel(currentHero);
    }

    private void BindRarityPanel(AccountHero hero) {
        int rarity = hero.GetBaseHero().Rarity;
        int awakeningLevel = hero.AwakeningLevel;
        for (int x = 0; x < rarityStars.Length; x++) {
            if (x < rarity) rarityStars[x].sprite = silverSprite;
            else if (x < awakeningLevel) rarityStars[x].sprite = goldSprite;
            else rarityStars[x].sprite = emptySprite;
        }
    }
}
