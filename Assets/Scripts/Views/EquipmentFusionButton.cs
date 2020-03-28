using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentFusionButton : MonoBehaviour {

    private const string addIcon = "Icons/add_circle";

    public Image equipmentIcon;
    public RarityBehavior rarityView;

    public bool isCenter = false;

    private Sprite addSprite;
    private EquipmentSceneManager parentManager;

    private EquipmentType requiredEquipment;
    private int requiredLevel;

    private AccountEquipment selectedEquipment;

    public void Awake() {
        addSprite = Resources.Load<Sprite>(addIcon);
        parentManager = FindObjectOfType<EquipmentSceneManager>();
    }

    public void SetCardRequirements(EquipmentType equipmentType, int level) {
        requiredEquipment = equipmentType;
        requiredLevel = level;
    }

    public void SetAccountEquipment(AccountEquipment equipment) {
        selectedEquipment = equipment;
        equipmentIcon.sprite = equipment.GetBaseEquipment().Icon;
        equipmentIcon.color = new Color(1, 1, 1, 1);
        rarityView.SetLevel(0, equipment.Level, false);
    }

    public void SetEmpty() {
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
