using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccountEquipment : IComparable<AccountEquipment> {

    [SerializeField] public Guid EquipmentGuid { get; set; }
    [SerializeField] public EquipmentType EquipType { get; set; }
    [SerializeField] public int Level { get; set; }
    [SerializeField] public Guid? EquippedHeroGuid { get; set; }
    [SerializeField] public EquipmentSlot? EquippedSlot { get; set; }

    [NonSerialized] private BaseEquipment baseEquipment;

    public AccountEquipment(EquipmentType equipType, int level) {
        EquipmentGuid = Guid.NewGuid();
        EquipType = equipType;
        Level = level;
        EquippedHeroGuid = null;
        EquippedSlot = null;
        baseEquipment = BaseEquipmentContainer.GetBaseEquipment(equipType);
    }

    public void LoadBaseEquipment() {
        baseEquipment = BaseEquipmentContainer.GetBaseEquipment(EquipType);
    }

    public BaseEquipment GetBaseEquipment() {
        return baseEquipment;
    }

    public int CompareTo(AccountEquipment other) {
        if (EquippedHeroGuid == null && other.EquippedHeroGuid != null) return -1;
        if (other.EquippedHeroGuid == null && EquippedHeroGuid != null) return 1;
        var level = other.Level - Level;
        if (level != 0) return level;
        return EquipType.CompareTo(other.EquipType);
    }
}
