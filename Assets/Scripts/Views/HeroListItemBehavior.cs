﻿using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Com.Tempest.Whale.ResourceContainers;
using Com.Tempest.Whale.StateObjects;

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
    private IHeroSelectionListener heroSelectionListener;

    private StateManager stateManager;

    public void Awake() {
        stateManager = FindObjectOfType<StateManager>();
    }

    public void SetHero(AccountHero accountHero, int listPosition) {
        this.accountHero = accountHero;
        this.listPosition = listPosition;
        var baseHero = accountHero.GetBaseHero();
        heroIcon.sprite = Resources.Load<Sprite>(baseHero.HeroIconPath);
        blurryBorder.color = ColorContainer.ColorFromFaction(baseHero.Faction);
        levelText.text = accountHero.CurrentLevel.ToString();
        equipmentIcon.enabled = stateManager.CurrentAccountState.GetEquipmentForHero(accountHero).Count > 0;
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

    public void SetHeroSelectionListener(IHeroSelectionListener listener) {
        heroSelectionListener = listener;
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
        heroSelectionListener?.OnHeroSelected(listPosition, accountHero);
    }
}

public interface IHeroSelectionListener {

    public abstract void OnHeroSelected(int position, AccountHero hero);
}
