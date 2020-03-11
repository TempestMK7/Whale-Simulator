using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BattleSceneManager : MonoBehaviour {

    public RecyclerView recyclerView;
    public GameObject listItemPrefab;

    private BattleAdapter adapter;

    private List<AccountHero> unfilteredList;
    private List<AccountHero> filteredList;
    private FactionEnum? currentFilter;

    public void Awake() {
        var state = StateManager.GetCurrentState();
        unfilteredList = state.AccountHeroes;
        filteredList = FilterList();

        adapter = new BattleAdapter(listItemPrefab, this);
        adapter.SetList(filteredList);
        recyclerView.SetAdapter(adapter);
        recyclerView.NotifyDataSetChanged();
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
}

public class BattleAdapter : RecyclerViewAdapter {

    private GameObject listItemPrefab;
    private BattleSceneManager sceneManager;
    private List<AccountHero> heroList;

    public BattleAdapter(GameObject listItemPrefab, BattleSceneManager sceneManager) {
        this.listItemPrefab = listItemPrefab;
        this.sceneManager = sceneManager;
    }

    public void SetList(List<AccountHero> heroList) {
        this.heroList = heroList;
    }

    public override GameObject OnCreateViewHolder(RectTransform contentHolder) {
        return UnityEngine.Object.Instantiate(listItemPrefab, contentHolder);
    }

    public override void OnBindViewHolder(GameObject viewHolder, int position) {
        viewHolder.GetComponent<HeroListItemBehavior>().SetHero(heroList[position], position);
        viewHolder.GetComponent<HeroListItemBehavior>().SetBattleSceneManager(sceneManager);
    }

    public override int GetItemCount() {
        return heroList == null ? 0 : heroList.Count;
    }
}
