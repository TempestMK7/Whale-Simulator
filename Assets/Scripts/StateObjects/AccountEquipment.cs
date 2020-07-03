using System;
using Com.Tempest.Whale.GameObjects;

namespace Com.Tempest.Whale.StateObjects {

    [Serializable]
    public class AccountEquipment : IComparable<AccountEquipment> {

        public Guid Id { get; set; }
        public EquipmentType EquipType { get; set; }
        public int Level { get; set; }
        public Guid? EquippedHeroId { get; set; }
        public EquipmentSlot? EquippedSlot { get; set; }

        [NonSerialized] private BaseEquipment baseEquipment;

        public AccountEquipment() {

        }

        public AccountEquipment(EquipmentType equipType, int level) {
            Id = Guid.NewGuid();
            EquipType = equipType;
            Level = level;
            EquippedHeroId = null;
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
            var typeComparison = EquipType.CompareTo(other.EquipType);
            if (typeComparison != 0) return typeComparison;
            if (other.Level != Level) return other.Level - Level;
            if (EquippedHeroId == null && other.EquippedHeroId != null) return 1;
            if (other.EquippedHeroId == null && EquippedHeroId != null) return -1;
            return 0;
        }

        public double GetDefense() {
            return baseEquipment.BaseDefense + (baseEquipment.DefensePerLevel * Level);
        }

        public double GetReflection() {
            return baseEquipment.BaseReflection + (baseEquipment.ReflectionPerLevel * Level);
        }
    }
}
