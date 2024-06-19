using UnityEngine;
using UnityEngine.UI;
using Com.Tempest.Whale.GameObjects;
using Com.Tempest.Whale.StateObjects;

public class EquipmentFusionButton : MonoBehaviour {

    private const string addIcon = "Icons/add_circle";

    public Text equippedText;
    public Image equipmentIcon;
    public RarityBehavior rarityView;

    public bool isCenter = false;

    private Sprite addSprite;
    private EquipmentSceneManager parentManager;

    private EquipmentSlot requiredEquipment;
    private int requiredLevel;

    private AccountEquipment selectedEquipment;

    public void Awake() {
        addSprite = Resources.Load<Sprite>(addIcon);
        parentManager = FindObjectOfType<EquipmentSceneManager>();
    }

    public void SetCardRequirements(EquipmentSlot equipmentSlot, int level) {
        requiredEquipment = equipmentSlot;
        requiredLevel = level;
    }

    public void SetAccountEquipment(AccountEquipment equipment) {
        selectedEquipment = equipment;
        equippedText.enabled = equipment.EquippedHeroId != null;
        equipmentIcon.sprite = Resources.Load<Sprite>(BaseEquipmentContainer.GetEquipmentIcon(equipment.Slot, equipment.IconIndex));
        equipmentIcon.color = new Color(1, 1, 1, 1);
        rarityView.SetLevel(0, equipment.Level, false);
    }

    public void SetEmpty() {
        equippedText.enabled = false;
        selectedEquipment = null;
        equipmentIcon.sprite = addSprite;
        equipmentIcon.color = new Color(1, 1, 1, 1);
        rarityView.SetLevel(0, requiredLevel, false);
    }

    public AccountEquipment GetSelectedEquipment() {
        return selectedEquipment;
    }

    public void OnButtonClicked() {
        if (isCenter) return;
        parentManager.RequestFusionPopup(this, requiredEquipment, requiredLevel);
    }
}
