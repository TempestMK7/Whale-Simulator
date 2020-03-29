using UnityEngine;
using UnityEngine.UI;
using Com.Tempest.Whale.ResourceContainers;
using Com.Tempest.Whale.StateObjects;

public class RewardListItem : MonoBehaviour {

    public Image rewardIcon;
    public Text rewardCountText;
    public RarityBehavior rewardRarityView;

    public void SetReward(RewardType rewardType, int rewardCount) {
        rewardIcon.enabled = true;
        rewardCountText.enabled = true;
        rewardRarityView.gameObject.SetActive(false);

        rewardIcon.sprite = RewardIconContainer.GetIconForReward(rewardType);
        rewardCountText.text = CustomFormatter.Format(rewardCount);
    }

    public void SetReward(AccountEquipment equipment) {
        rewardIcon.enabled = true;
        rewardCountText.enabled = false;
        rewardRarityView.gameObject.SetActive(true);

        rewardIcon.sprite = Resources.Load<Sprite>(equipment.GetBaseEquipment().IconPath);
        rewardRarityView.SetLevel(0, equipment.Level, false);
    }
}
