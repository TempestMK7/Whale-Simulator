using UnityEngine;
using UnityEngine.UI;
using Com.Tempest.Whale.GameObjects;
using Com.Tempest.Whale.ResourceContainers;
using Com.Tempest.Whale.StateObjects;

public class RewardListItem : MonoBehaviour {

    public Image rewardIcon;
    public Text rewardCountText;
    public RarityBehavior rewardRarityView;

    public void SetReward(int goldCount, int gemCount) {
        rewardIcon.enabled = true;
        rewardCountText.enabled = true;
        rewardRarityView.gameObject.SetActive(false);

        if (gemCount > 0) {
            rewardIcon.sprite = Resources.Load<Sprite>(BaseInventoryContainer.GEM_ICON_PATH);
            rewardCountText.text = CustomFormatter.Format(gemCount);
        } else {
            rewardIcon.sprite = Resources.Load<Sprite>(BaseInventoryContainer.GOLD_ICON_PATH);
            rewardCountText.text = CustomFormatter.Format(goldCount);
        }
    }

    public void SetReward(AccountInventory inventory) {
        rewardIcon.enabled = true;
        rewardCountText.enabled = true;
        rewardRarityView.gameObject.SetActive(false);

        rewardIcon.sprite = Resources.Load<Sprite>(BaseInventoryContainer.GetBaseInventory(inventory.ItemType).iconName);
        rewardCountText.text = CustomFormatter.Format(inventory.Quantity);
    }

    public void SetReward(AccountEquipment equipment) {
        rewardIcon.enabled = true;
        rewardCountText.enabled = false;
        rewardRarityView.gameObject.SetActive(true);

        rewardIcon.sprite = Resources.Load<Sprite>(BaseEquipmentContainer.GetEquipmentIcon(equipment.Slot, equipment.IconIndex));
        rewardRarityView.SetLevel(0, equipment.Level, false);
    }
}
