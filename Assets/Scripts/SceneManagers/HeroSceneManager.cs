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
    public Image roleIconRight;
    public UnityEngine.UI.Button fuseButton;
    public Text fuseButtonText;
    public GameObject statPanel;
    public GameObject fusePanel;

    public GameObject heroAnimation;
    public LayerMask heroAnimationLayer;
    public FusionFanfareBehavior fusionFanfare;
    public LevelupFanfareBehavior levelupFanfare;

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
    private bool fanfarePlaying = false;

    private FusionRequirement? currentFusionRequirement;

    public void Awake() {
        var state = StateManager.GetCurrentState();
        unfilteredList = state.AccountHeroes;
        BuildList();
        masterContainer.SetActive(true);
        heroAnimation.SetActive(false);
        detailContainer.SetActive(false);
    }

    public void Update() {
        if (Input.GetMouseButtonUp(0) && !EventSystem.current.IsPointerOverGameObject()) {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, heroAnimationLayer)) {
                var animator = hit.transform.gameObject.GetComponent<Animator>();
                if (animator != null) animator.SetTrigger("Attack");
            }
        }
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
        return fanfarePlaying || FindObjectOfType<FusionPopupBehavior>() != null;
    }

    public void OnBackPressed() {
        if (ButtonsBlocked()) return;
        SceneManager.LoadSceneAsync("HubScene");
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
        levelupFanfare.Play();
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
        roleIconRight.sprite = RoleContainer.GetIconForRole(baseHero.Role);

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
        currentFusionRequirement = LevelContainer.GetFusionRequirementForLevel(currentHero.AwakeningLevel);
        fuseButton.gameObject.SetActive(currentFusionRequirement != null);
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
        if (currentFusionRequirement == null) {
            ToggleStatPanel(true);
            return;
        }
        centerFusion.SetAccountHero(currentHero);

        FactionEnum? requiredFaction = null;
        if (currentFusionRequirement?.RequireSameFaction == true) requiredFaction = baseHero.Faction;
        topLeftFusion.gameObject.SetActive(currentFusionRequirement?.SameHeroRequirement >= 1);
        topLeftFusion.SetCardRequirements(baseHero.Faction, currentFusionRequirement?.SameHeroLevel ?? 5, baseHero.Hero);
        topLeftFusion.SetEmpty();
        topRightFusion.gameObject.SetActive(currentFusionRequirement?.SameHeroRequirement >= 2);
        topRightFusion.SetCardRequirements(baseHero.Faction, currentFusionRequirement?.SameHeroLevel ?? 5, baseHero.Hero);
        topRightFusion.SetEmpty();
        bottomLeftFusion.gameObject.SetActive(currentFusionRequirement?.FactionHeroRequirement >= 1);
        bottomLeftFusion.SetCardRequirements(requiredFaction, currentFusionRequirement?.FactionHeroLevel ?? 5, null);
        bottomLeftFusion.SetEmpty();
        bottomRightFusion.gameObject.SetActive(currentFusionRequirement?.FactionHeroRequirement >= 2);
        bottomRightFusion.SetCardRequirements(requiredFaction, currentFusionRequirement?.FactionHeroLevel ?? 5, null);
        bottomRightFusion.SetEmpty();
        bottomMiddleFusion.gameObject.SetActive(currentFusionRequirement?.FactionHeroRequirement >= 3);
        bottomMiddleFusion.SetCardRequirements(requiredFaction, currentFusionRequirement?.FactionHeroLevel ?? 5, null);
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

    public void RequestPopup(FusionSelectionBehavior fusionButton, FactionEnum? faction, int levelRequirement, HeroEnum? specificHero) {
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
        if (!successful) return;
        ResetListPosition();
        BindDetailView();
        StartCoroutine("PlayFanfare");
    }

    public void OnSuggestFusion() {
        if (currentFusionRequirement == null) return;
        SetupFusePanel();
        var alreadySelected = new List<AccountHero> {
            filteredList[currentPosition]
        };

        if (currentFusionRequirement?.SameHeroRequirement >= 1) {
            SelectSameHero(topLeftFusion, alreadySelected);
        }
        if (currentFusionRequirement?.SameHeroRequirement >= 2) {
            SelectSameHero(topRightFusion, alreadySelected);
        }
        if (currentFusionRequirement?.FactionHeroRequirement >= 1) {
            SelectFactionHero(bottomLeftFusion, alreadySelected);
        }
        if (currentFusionRequirement?.FactionHeroRequirement >= 2) {
            SelectFactionHero(bottomRightFusion, alreadySelected);
        }
        if (currentFusionRequirement?.FactionHeroRequirement >= 3) {
            SelectFactionHero(bottomMiddleFusion, alreadySelected);
        }
        HandleCompleteFusionButton();
    }

    private void SelectSameHero(FusionSelectionBehavior fusion, List<AccountHero> alreadySelected) {
        var baseHero = filteredList[currentPosition].GetBaseHero();
        var allHeroes = StateManager.GetCurrentState().AccountHeroes;
        var firstSelectable = allHeroes.Find(delegate (AccountHero hero) {
            return !alreadySelected.Contains(hero) && hero.GetBaseHero().Hero == baseHero.Hero && hero.AwakeningLevel == currentFusionRequirement?.SameHeroLevel;
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
            bool meetsRequirements = !alreadySelected.Contains(hero) && hero.AwakeningLevel == currentFusionRequirement?.FactionHeroLevel;
            if (currentFusionRequirement?.RequireSameFaction == true) meetsRequirements = meetsRequirements && hero.GetBaseHero().Faction == baseHero.Faction;
            return meetsRequirements;
        });
        if (firstSelectable != null) {
            fusion.SetAccountHero(firstSelectable);
            alreadySelected.Add(firstSelectable);
        }
    }

    IEnumerator PlayFanfare() {
        fanfarePlaying = true;
        fusionFanfare.Play();
        yield return new WaitForSeconds(2f);
        fanfarePlaying = false;
        yield return null;
    }
}
