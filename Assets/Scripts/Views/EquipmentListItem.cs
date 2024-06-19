using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Com.Tempest.Whale.GameObjects;
using Com.Tempest.Whale.StateObjects;

public class EquipmentListItem : MonoBehaviour, IPointerClickHandler {

    public Image equipmentIcon;
    public RarityBehavior rarityView;
    public Text equippedText;

    private AccountEquipment accountEquipment;
    private bool showEquippedStatus;
    private int listPosition;
    private Action<int> clickHandler;

    public void SetEquipment(AccountEquipment accountEquipment, bool showEquippedStatus, Action<int> clickHandler, int listPosition = 0) {
        this.accountEquipment = accountEquipment;
        this.showEquippedStatus = showEquippedStatus;
        this.clickHandler = clickHandler;
        this.listPosition = listPosition;
        BindEquipmentToListItem();
    }

    private void BindEquipmentToListItem() {
        if (accountEquipment == null) {
            equipmentIcon.enabled = false;
            rarityView.SetLevel(0, 0, false);
            equippedText.enabled = false;
        } else {
            equipmentIcon.enabled = true;
            equipmentIcon.sprite = Resources.Load<Sprite>(BaseEquipmentContainer.GetEquipmentIcon(accountEquipment.Slot, accountEquipment.IconIndex));
            rarityView.SetLevel(0, accountEquipment.Level, false);
            equippedText.enabled = showEquippedStatus && accountEquipment.EquippedHeroId != null;
        }
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (clickHandler == null) return;
        clickHandler.Invoke(listPosition);
    }
}
