using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class BattleSceneManager : MonoBehaviour {

    public RecyclerView recyclerView;
    public GameObject listItemPrefab;

    public HeroPlaceholderBehavior[] allyHolders;
    public HeroPlaceholderBehavior[] enemyHolders;

    public UnityEngine.UI.Button fightButton;

    private BattleAdapter adapter;

    private List<AccountHero> unfilteredList;
    private List<AccountHero> filteredList;
    private FactionEnum? currentFilter;

    private BattleEnum battleType;
    private List<AccountHero> selectedAllies = new List<AccountHero>();
    private List<AccountHero> selectedEnemies = new List<AccountHero>();

    public void Awake() {
        var state = StateManager.GetCurrentState();
        unfilteredList = state.AccountHeroes;
        filteredList = FilterList();

        adapter = new BattleAdapter(listItemPrefab, this);
        adapter.SetList(filteredList);
        recyclerView.SetAdapter(adapter);
        recyclerView.NotifyDataSetChanged();

        battleType = BattleManager.GetBattleType();
        SelectEnemiesFromBattleType();
        FillInSelectedAlliesFromState();
        HandleFightButton();
    }

    private void SelectEnemiesFromBattleType() {
        selectedEnemies = new List<AccountHero>();
        var state = StateManager.GetCurrentState();
        switch (battleType) {
            case BattleEnum.CAMPAIGN:
                var mission = MissionContainer.GetMission(state.CurrentChapter, state.CurrentMission);
                foreach (HeroEnum hero in mission.MissionHeroes) {
                    var accountHero = new AccountHero(hero) {
                        AwakeningLevel = mission.HeroAwakening,
                        CurrentLevel = mission.HeroLevel
                    };
                    selectedEnemies.Add(accountHero);
                }
                break;
            default:
                selectedEnemies = new List<AccountHero>();
                // TODO: Launch error popup.
                break;
        }
        for (int x = 0; x < selectedEnemies.Count; x++) {
            enemyHolders[x].SetHero(selectedEnemies[x].HeroType);
            enemyHolders[x].gameObject.SetActive(true);
        }
        for (int x = selectedEnemies.Count; x < enemyHolders.Length; x++) {
            enemyHolders[x].gameObject.SetActive(false);
        }
    }

    private void FillInSelectedAlliesFromState() {
        for (int x = 0; x < selectedAllies.Count; x++) {
            allyHolders[x].SetHero(selectedAllies[x].HeroType);
            allyHolders[x].gameObject.SetActive(true);
        }
        for (int x = selectedAllies.Count; x < allyHolders.Length; x++) {
            allyHolders[x].gameObject.SetActive(false);
        }
    }

    private void BuildList() {
        filteredList = FilterList();
        adapter.SetList(filteredList);
        recyclerView.NotifyDataSetChanged();
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
        if (isSelected && selectedAllies.Count >= 5) { return false; }

        if (isSelected) {
            selectedAllies.Add(hero);
        } else {
            selectedAllies.Remove(hero);
        }
        HandleFightButton();
        FillInSelectedAlliesFromState();
        adapter.SetSelectedList(selectedAllies);
        return true;
    }

    private void HandleFightButton() {
        fightButton.gameObject.SetActive(selectedAllies.Count > 0);
    }

    public void OnFight() {
        var combatReport = CombatEvaluator.GenerateCombatReport(selectedAllies, selectedEnemies);
        Debug.Log("Combat report generated with " + combatReport.turns.Count + " turns.");
        var fileName = "/CombatReport.txt";
        StreamWriter writer = new StreamWriter(Application.persistentDataPath + fileName, false);
        writer.WriteLine(JsonUtility.ToJson(combatReport));
        writer.Close();
        Debug.Log("Done writing file.");
    }
}

public class BattleAdapter : RecyclerViewAdapter {

    private GameObject listItemPrefab;
    private BattleSceneManager sceneManager;
    private List<AccountHero> heroList = new List<AccountHero>();
    private List<AccountHero> selectedHeroes = new List<AccountHero>();

    public BattleAdapter(GameObject listItemPrefab, BattleSceneManager sceneManager) {
        this.listItemPrefab = listItemPrefab;
        this.sceneManager = sceneManager;
    }

    public void SetList(List<AccountHero> heroList) {
        this.heroList = heroList;
    }

    public void SetSelectedList(List<AccountHero> selectedHeroes) {
        this.selectedHeroes = selectedHeroes;
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
