﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HeroSceneManager : MonoBehaviour {

    public GameObject masterContainer;
    public RecyclerView heroRecycler;

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
    public Text deflectionLabel;
    public Text speedLabel;
    public Text attackLabel;
    public Text magicLabel;
    public Text critLabel;

    public Image basicAttackImage;
    public Image specialAttackImage;
    public Image passiveImage;

    public FusionSelectionBehavior centerFusion;
    public FusionSelectionBehavior topLeftFusion;
    public FusionSelectionBehavior topRightFusion;
    public FusionSelectionBehavior bottomLeftFusion;
    public FusionSelectionBehavior bottomMiddleFusion;
    public FusionSelectionBehavior bottomRightFusion;
    public UnityEngine.UI.Button completeFusionButton;

    public GameObject heroListItemPrefab;
    public GameObject fusionPopupPrefab;

    private HeroAdapter heroAdapter;
    private FactionEnum? currentFilter;
    private List<AccountHero> unfilteredList;
    private List<AccountHero> filteredList;

    private int currentPosition;
    private bool fanfarePlaying = false;

    private FusionRequirement? currentFusionRequirement;

    public void Awake() {
        var state = StateManager.GetCurrentState();
        unfilteredList = state.AccountHeroes;
        masterContainer.SetActive(true);
        heroAnimation.SetActive(false);
        detailContainer.SetActive(false);
        heroAdapter = new HeroAdapter(heroListItemPrefab, this);
        heroRecycler.SetAdapter(heroAdapter);
        BuildList();
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
        filteredList = FilterList();
        heroAdapter.SetNewList(filteredList);
        heroRecycler.NotifyDataSetChanged();
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
        var baseHero = combatHero.baseHero;
        var currentLevel = combatHero.currentLevel;

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

        healthLabel.text = string.Format("Health: {0}", combatHero.health.ToString("0"));
        attackLabel.text = string.Format("Attack: {0}", combatHero.attack.ToString("0"));
        magicLabel.text = string.Format("Magic: {0}", combatHero.magic.ToString("0"));
        defenseLabel.text = string.Format("Defense: {0}", combatHero.defense.ToString("0.0"));
        reflectionLabel.text = string.Format("Reflection: {0}", combatHero.reflection.ToString("0.0"));
        speedLabel.text = string.Format("Speed: {0}", combatHero.speed.ToString("0"));
        deflectionLabel.text = string.Format("Deflection: {0}%", (combatHero.deflectionChance * 100).ToString("0"));
        critLabel.text = string.Format("Critical: {0}%", (combatHero.critChance* 100).ToString("0"));

        var basicAttack = AttackInfoContainer.GetAttackInfo(baseHero.BasicAttack);
        var specialAttack = AttackInfoContainer.GetAttackInfo(baseHero.SpecialAttack);
        var passiveAbility = AbilityInfoContainer.GetAbilityInfo(baseHero.PassiveAbility);

        basicAttackImage.sprite = basicAttack.AttackIcon;
        specialAttackImage.sprite = specialAttack.AttackIcon;
        passiveImage.sprite = passiveAbility.AbilityIcon;

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

public class HeroAdapter : RecyclerViewAdapter {

    private List<AccountHero> heroes;
    private readonly GameObject listItemPrefab;
    private readonly HeroSceneManager sceneManager;

    public HeroAdapter(GameObject listItemPrefab, HeroSceneManager sceneManager) {
        this.listItemPrefab = listItemPrefab;
        this.sceneManager = sceneManager;
    }

    public void SetNewList(List<AccountHero> heroes) {
        this.heroes = heroes;
    }

    public override GameObject OnCreateViewHolder(RectTransform contentArea) {
        return UnityEngine.Object.Instantiate(listItemPrefab, contentArea);
    }

    public override void OnBindViewHolder(GameObject viewHolder, int position) {
        viewHolder.GetComponent<HeroListItemBehavior>().SetHero(heroes[position], position);
        viewHolder.GetComponent<HeroListItemBehavior>().SetHeroSceneManager(sceneManager);
    }

    public override int GetItemCount() {
        return heroes != null ? heroes.Count : 0;
    }
}
