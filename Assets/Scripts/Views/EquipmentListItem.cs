using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Com.Tempest.Whale.Combat;
using Com.Tempest.Whale.GameObjects;
using Com.Tempest.Whale.StateObjects;

public class EquipmentListItem : MonoBehaviour, IPointerClickHandler {

    public Image equipmentIcon;
    public RarityBehavior rarityView;
    public Text nameLabel;
    public Text statLabel;
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
            if (nameLabel != null) {
                nameLabel.text = "";
            }
            if (statLabel != null) {
                statLabel.text = "";
            }
            if (equippedText != null) {
                equippedText.enabled = false;
            }
        } else {
            equipmentIcon.enabled = true;
            equipmentIcon.sprite = Resources.Load<Sprite>(BaseEquipmentContainer.GetEquipmentIcon(accountEquipment.Slot, accountEquipment.IconIndex));
            rarityView.SetLevel(0, accountEquipment.Level, false);
            if (nameLabel != null) {
                nameLabel.text = BaseEquipmentContainer.GetEquipmentName(accountEquipment);
            }
            if (statLabel != null) {
                var primaryLabel = BaseEquipmentContainer.GetStatAbbreviation(accountEquipment.PrimaryStat);
                var primaryAmount = BaseEquipmentContainer.FormatStat(accountEquipment.PrimaryStat, CombatHero.CalculateStatFromEquipment(accountEquipment.PrimaryStat, accountEquipment.PrimaryQuality, accountEquipment.Level));
                var secondaryLabel = BaseEquipmentContainer.GetStatAbbreviation(accountEquipment.SecondaryStat);
                var secondaryAmount = BaseEquipmentContainer.FormatStat(accountEquipment.SecondaryStat, CombatHero.CalculateStatFromEquipment(accountEquipment.SecondaryStat, accountEquipment.SecondaryQuality, accountEquipment.Level));
                var tertiaryLabel = BaseEquipmentContainer.GetStatAbbreviation(accountEquipment.TertiaryStat);
                var tertiaryAmount = BaseEquipmentContainer.FormatStat(accountEquipment.TertiaryStat, CombatHero.CalculateStatFromEquipment(accountEquipment.TertiaryStat, accountEquipment.TertiaryQuality, accountEquipment.Level));
                statLabel.text = $"{primaryLabel}: {primaryAmount}, {secondaryLabel}: {secondaryAmount}, {tertiaryLabel}: {tertiaryAmount}";
            }
            if (equippedText != null) {
                equippedText.enabled = showEquippedStatus && accountEquipment.EquippedHeroId != null;
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (clickHandler == null) return;
        clickHandler.Invoke(listPosition);
    }
}
