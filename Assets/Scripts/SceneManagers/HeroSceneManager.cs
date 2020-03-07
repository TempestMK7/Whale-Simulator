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
    public Image factionIconLeft;
    public Image factionIconRight;

    public GameObject heroListItemPrefab;

    private FactionEnum? currentFilter;
    private List<AccountHero> unfilteredList;
    private List<AccountHero> filteredList;

    private int currentPosition;

    public void Awake() {
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
        if (currentFilter == null) return unfilteredList;
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

    public void BindDetailView() {
        var currentHero = filteredList[currentPosition];
        var baseHero = currentHero.GetBaseHero();

        heroLabel.text = baseHero.HeroName;
        factionIconLeft.sprite = FactionContainer.GetIconForFaction(baseHero.Faction);
        factionIconRight.sprite = FactionContainer.GetIconForFaction(baseHero.Faction);
    }
}
