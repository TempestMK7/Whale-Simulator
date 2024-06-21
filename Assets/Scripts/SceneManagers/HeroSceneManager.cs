using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Com.Tempest.Whale.GameObjects;
using Com.Tempest.Whale.ResourceContainers;
using Com.Tempest.Whale.StateObjects;

public class HeroSceneManager : MonoBehaviour {

    public Canvas mainCanvas;
    public LoadingPopup loadingPrefab;

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

    public EquipmentListItem headEquipment;
    public EquipmentListItem neckEquipment;
    public EquipmentListItem waistEquipment;
    public UnityEngine.UI.Button unequipButton;

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
    public GameObject equipmentPopupPrefab;
    public GameObject fusionPopupPrefab;
    public TooltipPopup tooltipPrefab;

    private CredentialsManager credentialsManager;
    private StateManager stateManager;

    private HeroAdapter heroAdapter;
    private FactionEnum? currentFilter;
    private List<AccountHero> unfilteredList;
    private List<AccountHero> filteredList;

    private int currentPosition;
    private bool loadingFromServer = false;
    private bool fanfarePlaying = false;

    private FusionRequirement? currentFusionRequirement;

    public void Awake() {
        stateManager = FindObjectOfType<StateManager>();
        unfilteredList = stateManager.CurrentAccountState.AccountHeroes;
        masterContainer.SetActive(true);
        heroAnimation.SetActive(false);
        detailContainer.SetActive(false);

        credentialsManager = FindObjectOfType<CredentialsManager>();
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
                var childAnimators = hit.transform.gameObject.GetComponentsInChildren<BaseWhaleAnimation>();
                foreach (BaseWhaleAnimation child in childAnimators) {
                    child.Attack();
                }
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
        return fanfarePlaying ||
            loadingFromServer ||
            FindObjectOfType<FusionPopupBehavior>() != null || 
            FindObjectOfType<TooltipPopup>() != null || 
            FindObjectOfType<EquipmentSelectPopup>() != null || 
            FindObjectOfType<LoadingPopup>() != null;
    }

    public void OnBackPressed() {
        if (ButtonsBlocked()) return;
        SceneManager.LoadSceneAsync("HubScene");
    }

    public void OnFilterPressed(int filterPosition) {
        FactionEnum faction = (FactionEnum)Enum.GetValues(typeof(FactionEnum)).GetValue(filterPosition + 1);
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

    public async void OnLevelUpPressed() {
        if (ButtonsBlocked()) return;
        loadingFromServer = true;
        try {
            bool successful = await credentialsManager.RequestLevelup(filteredList[currentPosition]);
            loadingFromServer = false;
            OnLevelUpComplete(successful);
        } catch (Exception e) {
            Debug.LogError(e);
            loadingFromServer = false;
            CredentialsManager.DisplayNetworkError(mainCanvas, "There was an error while communicating with the server.");
        }
    }

    public void OnLevelUpComplete(bool successful) {
        if (!successful) return;
        levelupFanfare.Play();
        ResetListPosition();
        BindDetailView();
    }

    private void ResetListPosition() {
        var selectedId = filteredList[currentPosition].Id;
        filteredList = FilterList();
        currentPosition = filteredList.FindIndex((AccountHero hero) => {
            return hero.Id.Equals(selectedId);
        });
    }

    public void BindDetailView() {
        var currentHero = filteredList[currentPosition];
        var combatHero = currentHero.GetCombatHeroFromAllEquipment(stateManager.CurrentAccountState.AccountEquipment);
        var baseHero = combatHero.baseHero;
        var currentLevel = combatHero.currentLevel;

        heroLabel.text = baseHero.HeroName;
        positionLabel.text = string.Format("({0} of {1})", currentPosition + 1, filteredList.Count);
        factionIconLeft.sprite = FactionIconContainer.GetIconForFaction(baseHero.Faction);
        roleIconRight.sprite = RoleIconContainer.GetIconForRole(baseHero.Role);

        var existingHarshAnimation = heroAnimation.GetComponentInChildren<BaseWhaleAnimation>();
        if (existingHarshAnimation != null) {
            Destroy(existingHarshAnimation.gameObject);
        }

        if (baseHero.PrefabPath != null) {
            heroAnimation.GetComponent<SpriteRenderer>().enabled = false;
            var harshAnimation = Instantiate(Resources.Load<BaseWhaleAnimation>(baseHero.PrefabPath), heroAnimation.transform);
            harshAnimation.OnCreate(heroAnimation.transform.localScale);
        } else {
            heroAnimation.GetComponent<SpriteRenderer>().enabled = true;
            var animator = Resources.Load<AnimatorOverrideController>(baseHero.AnimatorPath);
            heroAnimation.GetComponent<Animator>().runtimeAnimatorController = animator;
        }

        var equipped = stateManager.CurrentAccountState.GetEquipmentForHero(currentHero);
        headEquipment.SetEquipment(
            equipped.Find((AccountEquipment matchable) => { return matchable.Slot == EquipmentSlot.HEAD; }),
            false, LaunchEquipmentPopup, (int)EquipmentSlot.HEAD);
        neckEquipment.SetEquipment(
            equipped.Find((AccountEquipment matchable) => { return matchable.Slot == EquipmentSlot.NECK; }),
            false, LaunchEquipmentPopup, (int)EquipmentSlot.NECK);
        waistEquipment.SetEquipment(
            equipped.Find((AccountEquipment matchable) => { return matchable.Slot == EquipmentSlot.BELT; }),
            false, LaunchEquipmentPopup, (int)EquipmentSlot.BELT);
        unequipButton.gameObject.SetActive(equipped.Count > 0);

        levelLabel.text = string.Format("Level: {0}", currentLevel);
        levelButton.gameObject.SetActive(currentLevel < LevelContainer.MaxLevelForAwakeningValue(currentHero.AwakeningLevel));
        currentGold.text = CustomFormatter.Format(stateManager.CurrentAccountState.CurrentGold);
        currentSouls.text = ""; // CustomFormatter.Format(stateManager.CurrentAccountState.CurrentSouls);
        goldCost.text = ""; // CustomFormatter.Format(LevelContainer.HeroExperienceRequirement(currentLevel));
        soulsCost.text = ""; // CustomFormatter.Format(LevelContainer.HeroExperienceRequirement(currentLevel));

        healthLabel.text = string.Format("Health: {0}", combatHero.health.ToString("0"));
        attackLabel.text = string.Format("Strength: {0}", combatHero.strength.ToString("0"));
        magicLabel.text = string.Format("Power: {0}", combatHero.power.ToString("0"));
        defenseLabel.text = string.Format("Toughness: {0}", combatHero.toughness.ToString("0"));
        reflectionLabel.text = string.Format("Resistance: {0}", combatHero.resistance.ToString("0"));
        speedLabel.text = string.Format("Speed: {0}", combatHero.speed.ToString("0"));
        deflectionLabel.text = string.Format("Deflection: {0}%", (combatHero.deflectionChance * 100).ToString("0"));
        critLabel.text = string.Format("Critical: {0}%", (combatHero.critChance* 100).ToString("0"));

        var basicAttack = AttackInfoContainer.GetAttackInfo(currentHero.GetBasicAttackEnum());
        var specialAttack = AttackInfoContainer.GetAttackInfo(currentHero.GetChargeAttackEnum());
        var passiveAbility = AbilityInfoContainer.GetAbilityInfo(baseHero.PassiveAbility);

        basicAttackImage.sprite = Resources.Load<Sprite>(basicAttack.AttackIconPath);
        specialAttackImage.sprite = Resources.Load<Sprite>(specialAttack.AttackIconPath);
        passiveImage.sprite = Resources.Load<Sprite>(passiveAbility.AbilityIconPath);

        rarityView.SetLevel(baseHero.Rarity, currentHero.AwakeningLevel, true);
        currentFusionRequirement = LevelContainer.GetFusionRequirementForLevel(currentHero.AwakeningLevel);
        fuseButton.gameObject.SetActive(currentFusionRequirement != null);
        ToggleStatPanel(true);
    }

    public void LaunchEquipmentPopup(int slot) {
        if (ButtonsBlocked()) return;
        var selectedSlot = (EquipmentSlot)slot;
        var popup = Instantiate(equipmentPopupPrefab, detailContainer.transform).GetComponent<EquipmentSelectPopup>();
        popup.SetHeroAndSlot(filteredList[currentPosition], selectedSlot);
    }

    public async void NotifyEquipmentSelected(AccountEquipment selected, AccountHero equippedHero) {
        loadingFromServer = true;
        try {
            await credentialsManager.EquipToHero(selected, equippedHero);
            loadingFromServer = false;
            BindDetailView();
        } catch (Exception e) {
            Debug.LogError(e);
            loadingFromServer = false;
            CredentialsManager.DisplayNetworkError(mainCanvas, "There was an error while communicating with the server.");
        }
    }

    public async void UnequipHero() {
        loadingFromServer = true;
        try {
            await credentialsManager.UnequipHero(filteredList[currentPosition]);
            loadingFromServer = false;
            BindDetailView();
        } catch (Exception e) {
            Debug.LogError(e);
            loadingFromServer = false;
            CredentialsManager.DisplayNetworkError(mainCanvas, "There was an error while communicating with the server.");
        }
    }

    public void LaunchAttackTooltip() {
        if (ButtonsBlocked()) return;
        var attack = filteredList[currentPosition].GetBasicAttackEnum();
        var attackInfo = AttackInfoContainer.GetAttackInfo(attack);
        var popup = Instantiate(tooltipPrefab, detailContainer.transform);
        popup.SetTooltip(attackInfo.AttackName, attackInfo.GetTooltip());
    }

    public void LaunchSpecialTooltip() {
        if (ButtonsBlocked()) return;
        var attack = filteredList[currentPosition].GetChargeAttackEnum();
        var attackInfo = AttackInfoContainer.GetAttackInfo(attack);
        var popup = Instantiate(tooltipPrefab, detailContainer.transform);
        popup.SetTooltip(attackInfo.AttackName, attackInfo.GetTooltip());
    }

    public void LaunchPassiveTooltip() {
        if (ButtonsBlocked()) return;
        var passive = AbilityInfoContainer.GetAbilityInfo(filteredList[currentPosition].GetBaseHero().PassiveAbility);
        var popup = Instantiate(tooltipPrefab, detailContainer.transform);
        popup.SetTooltip(passive.AbilityName, passive.AbilityDescription);
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
        completeFusionButton.gameObject.SetActive(LevelContainer.FusionIsLegal(selected, destroyedHeroes));
    }

    public async void RequestFusion() {
        if (ButtonsBlocked()) return;
        var selected = filteredList[currentPosition];
        var destroyedHeroes = new List<AccountHero>();
        if (topLeftFusion.GetSelectedHero() != null) destroyedHeroes.Add(topLeftFusion.GetSelectedHero());
        if (topRightFusion.GetSelectedHero() != null) destroyedHeroes.Add(topRightFusion.GetSelectedHero());
        if (bottomLeftFusion.GetSelectedHero() != null) destroyedHeroes.Add(bottomLeftFusion.GetSelectedHero());
        if (bottomMiddleFusion.GetSelectedHero() != null) destroyedHeroes.Add(bottomMiddleFusion.GetSelectedHero());
        if (bottomRightFusion.GetSelectedHero() != null) destroyedHeroes.Add(bottomRightFusion.GetSelectedHero());
        if (!LevelContainer.FusionIsLegal(selected, destroyedHeroes)) return;

        loadingFromServer = true;
        bool successful = await credentialsManager.RequestFusion(selected, destroyedHeroes);
        loadingFromServer = false;
        OnFusionComplete(successful);
    }

    public void OnFusionComplete(bool successful) {
        if (!successful) return;
        ResetListPosition();
        BindDetailView();
        StartCoroutine(PlayFanfare());
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
        var allHeroes = stateManager.CurrentAccountState.AccountHeroes;
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
        var allHeroes = stateManager.CurrentAccountState.AccountHeroes;
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
