using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FusionPopupBehavior : MonoBehaviour {

    public RectTransform heroListContent;
    public GameObject heroListItemPrefab;

    private FactionEnum faction;
    private int level;
    private HeroEnum? filteredHero;
    private List<AccountHero> alreadySelected;
    private FusionSelectionBehavior summoner;

    public void Awake() {
        transform.localScale = new Vector3(0f, 0f);
    }

    private void BuildList() {
        var allHeroes = StateManager.GetCurrentState().AccountHeroes;
        var filteredList = allHeroes.FindAll(delegate (AccountHero hero) {
            var baseHero = hero.GetBaseHero();
            if (filteredHero != null && baseHero.Hero != filteredHero) return false;
            return baseHero.Faction == faction && hero.AwakeningLevel == level && !alreadySelected.Contains(hero);
        });

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
            listItem.GetComponent<HeroListItemBehavior>().SetFusionPopup(this);
            listItem.transform.SetParent(heroListContent);
            var transform = listItem.transform as RectTransform;
            float rowPosition = x % numItemsPerRow;
            transform.anchorMin = new Vector2((rowPosition + 1) * anchorMultiple, 1f);
            transform.anchorMax = transform.anchorMin;
            int rowNum = x / numItemsPerRow;
            float verticalPosition = (rowNum + 0.5f) * heightPerRow * -1f;
            transform.anchoredPosition = new Vector2(0f, verticalPosition);
        }
        heroListContent.anchoredPosition = new Vector2();
    }

    public void OnCancelPressed() {
        summoner.SetEmpty();
        StartCoroutine("ShrinkToNothing");
    }

    public void OnFusionListItemPressed(AccountHero hero) {
        summoner.SetAccountHero(hero);
        StartCoroutine("ShrinkToNothing");
    }

    public void LaunchPopup(FactionEnum faction, int level, HeroEnum? filteredHero, List<AccountHero> alreadySelected, FusionSelectionBehavior summoner) {
        this.faction = faction;
        this.level = level;
        this.filteredHero = filteredHero;
        this.alreadySelected = alreadySelected;
        this.summoner = summoner;
        StartCoroutine("ExpandIntoFrame");
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
