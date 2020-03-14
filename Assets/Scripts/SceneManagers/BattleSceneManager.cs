using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleSceneManager : MonoBehaviour {

    public LayerMask heroAnimationLayer;

    public GameObject selectionPanel;
    public RecyclerView selectionRecyclerView;
    public GameObject selectionPrefab;

    public GameObject statusPanel;
    public RecyclerView statusRecyclerView;
    public GameObject statusPrefab;

    public HeroPlaceholderBehavior[] allyHolders;
    public HeroPlaceholderBehavior[] enemyHolders;

    public UnityEngine.UI.Button fightButton;

    private BattleSelectionAdapter selectionAdapter;
    private BattleStatusAdapter statusAdapter;

    private BattleEnum battleType;

    // These are used in selection mode.
    private List<AccountHero> unfilteredList;
    private List<AccountHero> filteredList;
    private FactionEnum? currentFilter;

    private AccountHero[] selectedAllies;
    private AccountHero[] selectedEnemies;

    // These are used in combat mode.
    private AccountHero[] combatAllies;
    private AccountHero[] combatEnemies;

    public void Awake() {
        battleType = BattleManager.GetBattleType();
        switch (battleType) {
            case BattleEnum.TOWER:
            case BattleEnum.CAMPAIGN:
            default:
                SetSelectionMode();
                break;
        }
    }

    public void Update() {
        if (Input.GetMouseButtonUp(0) && !EventSystem.current.IsPointerOverGameObject()) {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, heroAnimationLayer)) {
                var placeHolder = hit.transform.gameObject.GetComponent<HeroPlaceholderBehavior>();
                placeHolder.OnClick();
            }
        }
    }

    #region Selection.

    private void SetSelectionMode() {
        selectionPanel.SetActive(true);
        statusPanel.SetActive(false);

        var state = StateManager.GetCurrentState();
        unfilteredList = state.AccountHeroes;
        filteredList = FilterList();

        selectionAdapter = new BattleSelectionAdapter(selectionPrefab, this);
        selectionAdapter.SetList(filteredList);
        selectionRecyclerView.SetAdapter(selectionAdapter);
        selectionRecyclerView.NotifyDataSetChanged();

        battleType = BattleManager.GetBattleType();
        switch (battleType) {
            case BattleEnum.TOWER:
            case BattleEnum.CAMPAIGN:
            default:
                selectedAllies = new AccountHero[5];
                break;
        }
        SelectEnemiesFromBattleType();
        FillInSelectedAlliesFromState();
        HandleFightButton();
    }

    private void SelectEnemiesFromBattleType() {
        var state = StateManager.GetCurrentState();
        switch (battleType) {
            case BattleEnum.CAMPAIGN:
                var mission = MissionContainer.GetMission(state.CurrentChapter, state.CurrentMission);
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
        FactionEnum faction = (FactionEnum)Enum.GetValues(typeof(FactionEnum)).GetValue(filterPosition);
        if (currentFilter == faction) {
            currentFilter = null;
        } else {
            currentFilter = faction;
        }
        BuildList();
    }

    public void OnBackPressed() {
        switch (battleType) {
            case BattleEnum.CAMPAIGN:
                SceneManager.LoadSceneAsync("CampaignScene");
                break;
            default:
                SceneManager.LoadSceneAsync("HubScene");
                break;
        }
    }

    public bool OnHeroSelected(AccountHero hero, bool isSelected) {
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
        for (int x = 0; x < selectedAllies.Length; x++) {
            if (selectedAllies != null) {
                fightButton.gameObject.SetActive(true);
                return;
            }
        }
        fightButton.gameObject.SetActive(false);
    }

    public async void OnFight() {
        var combatReport = await CombatEvaluator.GenerateCombatReport(selectedAllies, selectedEnemies);

        // TODO: Remove this when I'm done debugging.
        var fileName = "/CombatReport.txt";
        StreamWriter writer = new StreamWriter(Application.persistentDataPath + fileName, false);
        writer.WriteLine(JsonConvert.SerializeObject(combatReport));
        writer.Close();

        var readableReport = combatReport.ToHumanReadableReport();
        fileName = "/ReadableCombatReport.txt";
        writer = new StreamWriter(Application.persistentDataPath + fileName, false);
        foreach (string line in readableReport) {
            writer.WriteLine(line);
        }
        writer.Close();
        Debug.Log("Done writing file.");

        SetCombatMode(combatReport);
    }

    #endregion

    #region Combat.

    private void SetCombatMode(CombatReport report) {
        selectionPanel.SetActive(false);
        statusPanel.SetActive(true);

        statusAdapter = new BattleStatusAdapter(statusPrefab);
        statusAdapter.SetNewHeroState(report.allies, report.enemies);
        statusRecyclerView.SetAdapter(statusAdapter);
        statusRecyclerView.NotifyDataSetChanged();

        StartCoroutine(PlayCombatReport(report));
    }

    private System.Collections.IEnumerator PlayCombatReport(CombatReport report) {
        statusAdapter = new BattleStatusAdapter(statusPrefab);
        statusAdapter.SetNewHeroState(report.allies, report.enemies);
        statusRecyclerView.SetAdapter(statusAdapter);
        statusRecyclerView.NotifyDataSetChanged();
        yield return new WaitForSeconds(0.5f);

        foreach (CombatTurn turn in report.turns) {
            yield return StartCoroutine(PlayCombatTurn(turn));
            yield return new WaitForSeconds(0.5f);
        }
    }

    private System.Collections.IEnumerator PlayCombatTurn(CombatTurn turn) {
        statusAdapter.SetNewHeroState(turn.allies, turn.enemies);

        foreach (CombatStep step in turn.steps) {
            yield return StartCoroutine(PlayCombatStep(step));
        }
    }

    private System.Collections.IEnumerator PlayCombatStep(CombatStep step) {
        yield return null;
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

public class BattleStatusAdapter : RecyclerViewAdapter {

    private GameObject listItemPrefab;
    private List<CombatHero> allHeroes;

    public BattleStatusAdapter(GameObject listItemPrefab) {
        this.listItemPrefab = listItemPrefab;
    }

    public void SetNewHeroState(CombatHero[] allies, CombatHero[] enemies) {
        allHeroes = new List<CombatHero>();
        foreach (CombatHero ally in allies) {
            if (ally != null) allHeroes.Add(ally);
        }
        foreach (CombatHero enemy in enemies) {
            if (enemy != null) allHeroes.Add(enemy);
        }
    }

    public override GameObject OnCreateViewHolder(RectTransform contentHolder) {
        return UnityEngine.Object.Instantiate(listItemPrefab, contentHolder);
    }

    public override void OnBindViewHolder(GameObject viewHolder, int position) {
        var hero = allHeroes[position];
        var behavior = viewHolder.GetComponent<HeroStatusHolder>();
        behavior.SetHero(hero);
    }

    public override int GetItemCount() {
        return allHeroes == null ? 0 : allHeroes.Count;
    }
}
