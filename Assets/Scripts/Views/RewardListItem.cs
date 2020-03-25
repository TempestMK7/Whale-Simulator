using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardListItem : MonoBehaviour {

    public Image rewardIcon;
    public Text rewardCountText;
    public RarityBehavior rewardRarityView;

    public void SetReward(RewardType rewardType, int rewardCount) {
        rewardIcon.enabled = true;
        rewardCountText.enabled = true;
        rewardRarityView.gameObject.SetActive(false);

        rewardIcon.sprite = RewardInfoContainer.GetIconForReward(rewardType);
        rewardCountText.text = CustomFormatter.Format(rewardCount);
    }

    public void SetReward(AccountEquipment equipment) {
        rewardIcon.enabled = true;
        rewardCountText.enabled = false;
        rewardRarityView.gameObject.SetActive(true);

        rewardIcon.sprite = equipment.GetBaseEquipment().Icon;
        rewardRarityView.SetLevel(0, equipment.Level, false);
    }
}
