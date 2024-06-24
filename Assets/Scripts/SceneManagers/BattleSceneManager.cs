using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Com.Tempest.Whale.Combat;
using Com.Tempest.Whale.GameObjects;
using Com.Tempest.Whale.StateObjects;

public class BattleSceneManager : MonoBehaviour {

    public LayerMask heroAnimationLayer;
    public Sprite forestBackground;
    public Sprite caveBackground;
    public LoadingPopup loadingPrefab;

    public Canvas mainCanvas;
    public SpriteRenderer backgroundRenderer;
    public GameObject selectionPanel;
    public RecyclerView selectionRecyclerView;
    public GameObject selectionPrefab;

    public GameObject statusPanel;
    public Text turnText;
    public RecyclerView rewardRecycler;
    public GameObject rewardPrefab;
    public UnityEngine.UI.Button skipFightButton;
    public UnityEngine.UI.Button saveReportButton;
    public UnityEngine.UI.Button continueButton;

    public GameObject skipPanel;
    public CombatReportPopup reportPopupPrefab;
    public CombatGraphPopup graphPopupPrefab;

    public AnimatedHero[] allyHolders;
    public AnimatedHero[] enemyHolders;

    public UnityEngine.UI.Button fightButton;

    private BattleEnum battleType;
    private CredentialsManager credentialsManager;
    private StateManager stateManager;
    private bool loadingFromServer = false;

    // These are used in selection mode.
    private BattleSelectionAdapter selectionAdapter;

    private List<AccountHero> unfilteredList;
    private List<AccountHero> filteredList;
    private FactionEnum? currentFilter;

    private AccountHero[] selectedAllies;
    private AccountHero[] selectedEnemies;

    // These are used in combat mode.
    private bool combatStarted = false;
    private Dictionary<Guid, AnimatedHero> placeHolders;
    private bool skipBattle = false;
    private CombatReport combatReport;
    private EarnedRewardsContainer combatRewards;

    public void Awake() {
        credentialsManager = FindObjectOfType<CredentialsManager>();
        stateManager = FindObjectOfType<StateManager>();
        battleType = BattleManager.GetBattleType();
        switch (battleType) {
            case BattleEnum.CAMPAIGN:
                backgroundRenderer.sprite = forestBackground;
                break;
            case BattleEnum.LOOT_CAVE:
                backgroundRenderer.sprite = caveBackground;
                break;
        }
        skipPanel.SetActive(false);
        SetSelectionMode();
    }

    public void Update() {
        if (loadingFromServer || combatStarted) return;
        if (Input.GetMouseButtonUp(0) && !EventSystem.current.IsPointerOverGameObject()) {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, heroAnimationLayer)) {
                var placeHolder = hit.transform.gameObject.GetComponent<AnimatedHero>();
                placeHolder.OnClick();
            }
        }
    }

    #region Selection.

    private async void SetSelectionMode() {
        selectionPanel.SetActive(true);
        statusPanel.SetActive(false);

        unfilteredList = stateManager.CurrentAccountState.AccountHeroes;
        filteredList = FilterList();

        selectionAdapter = new BattleSelectionAdapter(selectionPrefab, this);
        selectionAdapter.SetList(filteredList);
        selectionRecyclerView.SetAdapter(selectionAdapter);
        selectionRecyclerView.NotifyDataSetChanged();

        battleType = BattleManager.GetBattleType();
        switch (battleType) {
            case BattleEnum.LOOT_CAVE:
            case BattleEnum.CAMPAIGN:
            default:
                selectedAllies = stateManager.GetLastUsedTeam();
                selectionAdapter.SetSelectedList(selectedAllies);
                selectionRecyclerView.NotifyDataSetChanged();
                break;
        }
        await SelectEnemiesFromBattleType();
        FillInSelectedAlliesFromState();
        HandleFightButton();
    }

    private async Task SelectEnemiesFromBattleType() {
        switch (battleType) {
            case BattleEnum.LOOT_CAVE:
                loadingFromServer = true;
                var loadingPopup = Instantiate(loadingPrefab, mainCanvas.transform);
                loadingPopup.LaunchPopup("Loading encounter...", "Finding your latest encounter...");
                var encounter = await credentialsManager.RequestCaveEncounter();
                selectedEnemies = new AccountHero[] {
                    new AccountHero(encounter.Position1Hero),
                    new AccountHero(encounter.Position2Hero),
                    new AccountHero(encounter.Position3Hero),
                    new AccountHero(encounter.Position4Hero),
                    new AccountHero(encounter.Position5Hero)
                };
                foreach (AccountHero hero in selectedEnemies) {
                    hero.AwakeningLevel = (encounter.Floor / 4) + 1;
                    hero.CurrentLevel = encounter.Floor * 5;
                }
                loadingPopup.DismissPopup();
                loadingFromServer = false;
                break;
            case BattleEnum.CAMPAIGN:
                var mission = MissionContainer.GetMission(stateManager.CurrentAccountState.CurrentChapter, stateManager.CurrentAccountState.CurrentMission);
                selectedEnemies = new AccountHero[mission.MissionHeroes.Length];
                for (int x = 0; x < selectedEnemies.Length; x++) {
                    var accountHero = new AccountHero(mission.MissionHeroes[x]) {
                        AwakeningLevel = mission.HeroAwakening,
                        CurrentLevel = mission.HeroLevel
                    };
                    selectedEnemies[x] = accountHero;
                }
                break;
            default:
                selectedEnemies = new AccountHero[0];
                // TODO: Launch error popup.
                break;
        }
        for (int x = 0; x < selectedEnemies.Length; x++) {
            enemyHolders[x].SetHero(selectedEnemies[x].HeroType);
            enemyHolders[x].gameObject.SetActive(true);
        }
        for (int x = selectedEnemies.Length; x < enemyHolders.Length; x++) {
            enemyHolders[x].gameObject.SetActive(false);
        }
    }

    private void FillInSelectedAlliesFromState() {
        for (int x = 0; x < selectedAllies.Length; x++) {
            var ally = selectedAllies[x];
            if (ally == null) {
                allyHolders[x].RegisterOnClick(null, null);
                allyHolders[x].gameObject.SetActive(false);
            } else {
                allyHolders[x].RegisterOnClick(RemoveSelectedAlly, selectedAllies[x]);
                allyHolders[x].SetHero(selectedAllies[x].HeroType);
                allyHolders[x].gameObject.SetActive(true);
            }
        }
        for (int x = selectedAllies.Length; x < allyHolders.Length; x++) {
            allyHolders[x].gameObject.SetActive(false);
        }
    }

    public void RemoveSelectedAlly(AccountHero hero) {
        for (int x = 0; x < selectedAllies.Length; x++) {
            if (selectedAllies[x] == hero) selectedAllies[x] = null;
        }
        FillInSelectedAlliesFromState();
        selectionAdapter.SetSelectedList(selectedAllies);
        selectionRecyclerView.NotifyDataSetChanged();
    }

    private void BuildList() {
        filteredList = FilterList();
        selectionAdapter.SetList(filteredList);
        selectionRecyclerView.NotifyDataSetChanged();
    }

    private List<AccountHero> FilterList() {
        if (currentFilter == null) return new List<AccountHero>(unfilteredList);
        List<AccountHero> filteredList = new List<AccountHero>();
        foreach (AccountHero hero in unfilteredList) {
            if (hero.GetBaseHero().Faction == currentFilter) filteredList.Add(hero);
        }
        return filteredList;
    }

    public void OnFilterPressed(int filterPosition) {
        if (ButtonsBlocked()) return;
        FactionEnum faction = (FactionEnum)Enum.GetValues(typeof(FactionEnum)).GetValue(filterPosition);
        if (currentFilter == faction) {
            currentFilter = null;
        } else {
            currentFilter = faction;
        }
        BuildList();
    }

    public void OnBackPressed() {
        if (ButtonsBlocked()) return;
        switch (battleType) {
            case BattleEnum.LOOT_CAVE:
                SceneManager.LoadSceneAsync("LootCaveScene");
                break;
            case BattleEnum.CAMPAIGN:
                SceneManager.LoadSceneAsync("CampaignScene");
                break;
            default:
                SceneManager.LoadSceneAsync("HubScene");
                break;
        }
    }

    public bool OnHeroSelected(AccountHero hero, bool isSelected) {
        if (ButtonsBlocked()) return false;
        if (isSelected) {
            for (int x = 0; x < selectedAllies.Length; x++) {
                if (selectedAllies[x] == null) {
                    selectedAllies[x] = hero;
                    HandleFightButton();
                    FillInSelectedAlliesFromState();
                    selectionAdapter.SetSelectedList(selectedAllies);
                    return true;
                }
            }
            return false;
        } else {
            for (int x = 0; x < selectedAllies.Length; x++) {
                if (selectedAllies[x] == hero) {
                    selectedAllies[x] = null;
                    HandleFightButton();
                    FillInSelectedAlliesFromState();
                    selectionAdapter.SetSelectedList(selectedAllies);
                    return true;
                }
            }
            return false;
        }
    }

    private void HandleFightButton() {
        foreach (AccountHero ally in selectedAllies) {
            if (ally != null) {
                fightButton.gameObject.SetActive(true);
                return;
            }
        }
        fightButton.gameObject.SetActive(false);
    }

    public async void OnFight() {
        if (ButtonsBlocked()) return;
        loadingFromServer = true;
        var loadingPopup = Instantiate(loadingPrefab, mainCanvas.transform);
        loadingPopup.LaunchPopup("Preparing...", "Writing ballads of your inevitable victory...");
        stateManager.SetLastUsedTeam(selectedAllies);

        try {
            var response = await credentialsManager.PerformEpicBattle(battleType, selectedAllies);
            combatReport = response.Report;
            combatRewards = response.Rewards;

            loadingPopup.DismissPopup();
            loadingFromServer = false;

            SetCombatMode(combatReport);
        } catch (Exception e) {
            Debug.LogError(e);
            loadingFromServer = false;
            CredentialsManager.DisplayNetworkError(mainCanvas, "There was an error loading the battle.");
        }
    }

    #endregion

    #region Combat.

    private void SetCombatMode(CombatReport report) {
        selectionPanel.SetActive(false);
        statusPanel.SetActive(true);
        combatStarted = true;

        turnText.text = "Turn 1";
        rewardRecycler.gameObject.SetActive(false);
        skipFightButton.gameObject.SetActive(true);
        saveReportButton.gameObject.SetActive(false);
        continueButton.gameObject.SetActive(false);

        StartCoroutine(PlayCombatReport(report));
    }

    private System.Collections.IEnumerator PlayCombatReport(CombatReport report) {
        placeHolders = new Dictionary<Guid, AnimatedHero>();

        BindAllyTeam(report.allies);
        BindEnemyTeam(report.enemies);
        yield return new WaitForSeconds(1f);

        foreach (CombatRound round in report.rounds) {
            if (!skipBattle) {
                yield return StartCoroutine(PlayCombatRound(round));
                yield return new WaitForSeconds(1f);
            }
        }

        OnEndOfCombat(report);
    }

    private void BindAllyTeam(CombatHero[] allies) {
        for (int x = 0; x < allies.Length; x++) {
            if (allies[x] != null) {
                allyHolders[x].gameObject.SetActive(true);
                allyHolders[x].SetHero(allies[x]);
                placeHolders[allies[x].combatHeroGuid] = allyHolders[x];
            } else {
                allyHolders[x].gameObject.SetActive(false);
            }
        }
        for (int x = allies.Length; x < allyHolders.Length; x++) {
            allyHolders[x].gameObject.SetActive(false);
        }
    }

    private void BindEnemyTeam(CombatHero[] enemies) {
        for (int x = 0; x < enemies.Length; x++) {
            if (enemies[x] != null) {
                enemyHolders[x].gameObject.SetActive(true);
                enemyHolders[x].SetHero(enemies[x]);
                placeHolders[enemies[x].combatHeroGuid] = enemyHolders[x];
            } else {
                enemyHolders[x].gameObject.SetActive(false);
            }
        }
        for (int x = enemies.Length; x < enemyHolders.Length; x++) {
            enemyHolders[x].gameObject.SetActive(false);
        }
    }

    private IEnumerator PlayCombatRound(CombatRound round) {
        turnText.text = string.Format("Round {0}", round.turnNumber);

        BindAllyTeam(round.allies);
        BindEnemyTeam(round.enemies);

        foreach (CombatTurn turn in round.turns) {
            if (!skipBattle) yield return StartCoroutine(PlayCombatTurn(turn));
        }
        
        foreach (CombatStep step in round.endOfTurn) {
            if (!skipBattle) {
                placeHolders[step.targetGuid].AnimateCombatStep(step);
                yield return new WaitForSeconds(0.3f);
            }
        }
    }

    private IEnumerator PlayCombatTurn(CombatTurn turn) {
        yield return placeHolders[turn.attacker.combatHeroGuid].AnimateCombatTurn(turn, placeHolders);
    }

    private void OnEndOfCombat(CombatReport report) {
        BindAllyTeam(report.alliesEnd);
        BindEnemyTeam(report.enemiesEnd);

        if (report.alliesWon) {
            turnText.text = "Victory!";
        } else {
            turnText.text = "Defeat...";
        }

        skipPanel.SetActive(false);
        skipFightButton.gameObject.SetActive(false);
        rewardRecycler.gameObject.SetActive(true);
        saveReportButton.gameObject.SetActive(true);
        continueButton.gameObject.SetActive(true);

        if (report.alliesWon) {
            var rewardAdapter = new RewardAdapter(rewardPrefab, combatRewards);
            rewardRecycler.SetAdapter(rewardAdapter);
            rewardRecycler.NotifyDataSetChanged();
        }
    }

    private bool ButtonsBlocked() {
        return loadingFromServer || FindObjectOfType<CombatReportPopup>() != null;
    }

    public void OnContinuePressed() {
        OnBackPressed();
    }

    public void OnReportPressed() {
        if (ButtonsBlocked()) return;
        if (combatReport != null) {
            var reportPopup = Instantiate(graphPopupPrefab, mainCanvas.transform);
            reportPopup.LaunchPopup(combatReport);
        }
    }

    public void OnSkipBattlePressed() {
        skipPanel.SetActive(true);
    }

    public void OnSkipConfirmPressed() {
        skipPanel.SetActive(false);
        skipFightButton.gameObject.SetActive(false);
        skipBattle = true;
    }

    public void OnSkipCancelPressed() {
        skipPanel.SetActive(false);
    }

    #endregion
}

public class BattleSelectionAdapter : RecyclerViewAdapter {

    private GameObject listItemPrefab;
    private BattleSceneManager sceneManager;
    private List<AccountHero> heroList = new List<AccountHero>();
    private List<AccountHero> selectedHeroes = new List<AccountHero>();

    public BattleSelectionAdapter(GameObject listItemPrefab, BattleSceneManager sceneManager) {
        this.listItemPrefab = listItemPrefab;
        this.sceneManager = sceneManager;
    }

    public void SetList(List<AccountHero> heroList) {
        this.heroList = heroList;
    }

    public void SetSelectedList(AccountHero[] heroArray) {
        selectedHeroes = new List<AccountHero>();
        foreach (AccountHero hero in heroArray) {
            if (hero != null) {
                selectedHeroes.Add(hero);
            }
        }
    }

    public override GameObject OnCreateViewHolder(RectTransform contentHolder) {
        return UnityEngine.Object.Instantiate(listItemPrefab, contentHolder);
    }

    public override void OnBindViewHolder(GameObject viewHolder, int position) {
        var hero = heroList[position];
        var behavior = viewHolder.GetComponent<HeroListItemBehavior>();
        behavior.SetHero(hero, position);
        behavior.SetBattleSceneManager(sceneManager);
        behavior.SetSelectionStatus(selectedHeroes.Contains(hero), true);
    }

    public override int GetItemCount() {
        return heroList == null ? 0 : heroList.Count;
    }
}

public class RewardAdapter : RecyclerViewAdapter {

    private GameObject listItemPrefab;

    private int earnedGold;
    private int earnedGems;
    private List<AccountInventory> earnedInventory;
    private List<AccountEquipment> earnedEquipment;

    public RewardAdapter(GameObject listItemPrefab, EarnedRewardsContainer rewards) {
        this.listItemPrefab = listItemPrefab;

        earnedGold = rewards.Gold;
        earnedGems = rewards.Gems;
        earnedInventory = rewards.EarnedInventory;
        earnedEquipment = rewards.EarnedEquipment;
    }

    public override GameObject OnCreateViewHolder(RectTransform contentHolder) {
        return UnityEngine.Object.Instantiate(listItemPrefab, contentHolder);
    }

    public override void OnBindViewHolder(GameObject viewHolder, int position) {
        var rewardHolder = viewHolder.GetComponent<RewardListItem>();

        int currencyCount = 0;
        if (earnedGold > 0) { currencyCount++; }
        if (earnedGems > 0) { currencyCount++; }
        if (position < currencyCount) {
            if (position == 0 && earnedGold > 0) {
                rewardHolder.SetReward(earnedGold, 0);
            } else {
                rewardHolder.SetReward(0, earnedGems);
            }
        } else if (position < currencyCount + earnedInventory.Count) {
            rewardHolder.SetReward(earnedInventory[position - currencyCount]);
        } else {
            rewardHolder.SetReward(earnedEquipment[position - (currencyCount + earnedInventory.Count)]);
        }
    }

    public override int GetItemCount() {
        int currencyCount = 0;
        if (earnedGold > 0) { currencyCount++; }
        if (earnedGems > 0) { currencyCount++; }
        return currencyCount + earnedInventory.Count + earnedEquipment.Count;
    }
}