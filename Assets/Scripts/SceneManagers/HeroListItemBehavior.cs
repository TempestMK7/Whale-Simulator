using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HeroListItemBehavior : MonoBehaviour, IPointerClickHandler {

    public Image blurryBorder;
    public Image heroIcon;

    private int listPosition;

    public void SetHero(AccountHero hero, int listPosition) {
        this.listPosition = listPosition;
        var baseHero = hero.GetBaseHero();
        heroIcon.sprite = baseHero.HeroIcon;
        blurryBorder.color = ColorContainer.ColorFromFaction(baseHero.Faction);
    }

    public void OnPointerClick(PointerEventData eventData) {
        FindObjectOfType<HeroSceneManager>().NotifyListSelection(listPosition);
    }
}
