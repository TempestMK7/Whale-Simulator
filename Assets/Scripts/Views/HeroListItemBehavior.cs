using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HeroListItemBehavior : MonoBehaviour, IPointerClickHandler {

    public Image blurryBorder;
    public Image heroIcon;
    public Text levelText;
    public Image equipmentIcon;
    public RarityBehavior rarityView;
    public Image selectionIcon;

    private AccountHero accountHero;
    private int listPosition;
    private bool isSelectable = false;
    private bool isSelected = false;

    private FusionPopupBehavior fusionPopup;
    private HeroSceneManager heroSceneManager;
    private BattleSceneManager battleSceneManager;

    public void SetHero(AccountHero accountHero, int listPosition) {
        this.accountHero = accountHero;
        this.listPosition = listPosition;
        var baseHero = accountHero.GetBaseHero();
        heroIcon.sprite = baseHero.HeroIcon;
        blurryBorder.color = ColorContainer.ColorFromFaction(baseHero.Faction);
        levelText.text = accountHero.CurrentLevel.ToString();
        equipmentIcon.enabled = StateManager.GetCurrentState().GetEquipmentForHero(accountHero).Count > 0;
        rarityView.SetLevel(baseHero.Rarity, accountHero.AwakeningLevel, false);
        HandleSelectionIcon();
    }

    private void HandleSelectionIcon() {
        selectionIcon.enabled = isSelected;
    }

    public void SetSelectionStatus(bool isSelected, bool isSelectable) {
        this.isSelected = isSelected;
        this.isSelectable = isSelectable;
        HandleSelectionIcon();
    }

    public void SetHeroSceneManager(HeroSceneManager manager) {
        heroSceneManager = manager;
    }

    public void SetFusionPopup(FusionPopupBehavior popup) {
        fusionPopup = popup;
    }

    public void SetBattleSceneManager(BattleSceneManager manager) {
        battleSceneManager = manager;
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (isSelectable && battleSceneManager != null && battleSceneManager.OnHeroSelected(accountHero, !isSelected)) {
            isSelected = !isSelected;
            HandleSelectionIcon();
        }

        if (fusionPopup != null) {
            fusionPopup.OnFusionListItemPressed(accountHero);
        }
        if (heroSceneManager != null) {
            heroSceneManager.NotifyListSelection(listPosition);
        }
    }
}
