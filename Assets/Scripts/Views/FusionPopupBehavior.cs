using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FusionPopupBehavior : MonoBehaviour {

    public RecyclerView recyclerView;
    public GameObject heroListItemPrefab;

    private FactionEnum? faction;
    private int level;
    private HeroEnum? filteredHero;
    private List<AccountHero> alreadySelected;
    private FusionSelectionBehavior summoner;
    private HeroSceneManager sceneManager;

    private FusionPopupAdapter adapter;

    public void Awake() {
        transform.localScale = new Vector3(0f, 0f);
        sceneManager = FindObjectOfType<HeroSceneManager>();
        adapter = new FusionPopupAdapter(heroListItemPrefab, this);
        recyclerView.SetAdapter(adapter);
    }

    private void BuildList() {
        var allHeroes = StateManager.GetCurrentState().AccountHeroes;
        var filteredList = allHeroes.FindAll(delegate (AccountHero hero) {
            var baseHero = hero.GetBaseHero();
            if (filteredHero != null && baseHero.Hero != filteredHero) return false;
            bool fitsRequirements = hero.AwakeningLevel == level && !alreadySelected.Contains(hero);
            if (faction != null) fitsRequirements = fitsRequirements && baseHero.Faction == faction;
            return fitsRequirements;
        });
        adapter.SetList(filteredList);
        recyclerView.NotifyDataSetChanged();
    }

    public void OnCancelPressed() {
        summoner.SetEmpty();
        if (sceneManager != null) sceneManager.OnFusionHeroSelected();
        StartCoroutine(ShrinkToNothing());
    }

    public void OnFusionListItemPressed(AccountHero hero) {
        summoner.SetAccountHero(hero);
        if (sceneManager != null) sceneManager.OnFusionHeroSelected();
        StartCoroutine(ShrinkToNothing());
    }

    public void LaunchPopup(FactionEnum? faction, int level, HeroEnum? filteredHero, List<AccountHero> alreadySelected, FusionSelectionBehavior summoner) {
        this.faction = faction;
        this.level = level;
        this.filteredHero = filteredHero;
        this.alreadySelected = alreadySelected;
        this.summoner = summoner;
        StartCoroutine(ExpandIntoFrame());
    }

    IEnumerator ExpandIntoFrame() {
        float duration = 12f;
        for (float frame = 0f; frame <= duration; frame++) {
            float ratio = frame / duration;
            transform.localScale = new Vector3(ratio, ratio);
            yield return null;
        }
        BuildList();
    }

    IEnumerator ShrinkToNothing() {
        float duration = 12f;
        for (float frame = duration; frame >= 0; frame--) {
            float ratio = frame / duration;
            transform.localScale = new Vector3(ratio, ratio);
            yield return null;
        }
        Destroy(gameObject);
    }
}

public class FusionPopupAdapter : RecyclerViewAdapter {

    private GameObject listItemPrefab;
    private FusionPopupBehavior parent;
    private List<AccountHero> heroes;

    public FusionPopupAdapter(GameObject listItemPrefab, FusionPopupBehavior parent) {
        this.listItemPrefab = listItemPrefab;
        this.parent = parent;
    }

    public void SetList(List<AccountHero> heroes) {
        this.heroes = heroes;
    }

    public override GameObject OnCreateViewHolder(RectTransform contentHolder) {
        return Object.Instantiate(listItemPrefab, contentHolder);
    }

    public override void OnBindViewHolder(GameObject viewHolder, int position) {
        var behavior = viewHolder.GetComponent<HeroListItemBehavior>();
        behavior.SetHero(heroes[position], position);
        behavior.SetFusionPopup(parent);
    }
    public override int GetItemCount() {
        return heroes == null ? 0 : heroes.Count;
    }
}
