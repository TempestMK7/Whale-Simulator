using System;
using Com.Tempest.Whale.GameObjects;

namespace Com.Tempest.Whale.StateObjects {

    [Serializable]
    public class AccountEquipment : IComparable<AccountEquipment> {

        public Guid Id { get; set; }
        public EquipmentSlot Slot { get; set; }
        public EquipmentStat PrimaryStat { get; set; }
        public EquipmentStat SecondaryStat { get; set; }
        public EquipmentStat TertiaryStat { get; set; }
        public int PrimaryQuality { get; set; }
        public int SecondaryQuality { get; set; }
        public int TertiaryQuality { get; set; }
        public int Level { get; set; }
        public int IconIndex { get; set; }
        public Guid? EquippedHeroId { get; set; }

        public AccountEquipment() {

        }

        public AccountEquipment(EquipmentSlot slot, EquipmentStat primaryStat, EquipmentStat secondaryStat, EquipmentStat tertiaryStat, int primaryQuality, int secondaryQuality, int tertiaryQuality, int level, int iconIndex) {
            Id = Guid.NewGuid();
            Slot = slot;
            PrimaryStat = primaryStat;
            SecondaryStat = secondaryStat;
            TertiaryStat = tertiaryStat;
            PrimaryQuality = primaryQuality;
            SecondaryQuality = secondaryQuality;
            TertiaryQuality = tertiaryQuality;
            Level = level;
            IconIndex = iconIndex;
        }

        public int CompareTo(AccountEquipment other) {
            if (Slot != other.Slot) return Slot.CompareTo(other.Slot);
            if (Level != other.Level) return Level.CompareTo(other.Level);
            if (PrimaryStat != other.PrimaryStat) return PrimaryStat.CompareTo(other.PrimaryStat);
            if (SecondaryStat != other.SecondaryStat) return SecondaryStat.CompareTo(other.SecondaryStat);
            if (TertiaryStat != other.TertiaryStat) return TertiaryStat.CompareTo(other.TertiaryStat);
            if (EquippedHeroId == null && other.EquippedHeroId != null) return 1;
            if (other.EquippedHeroId == null && EquippedHeroId != null) return -1;
            return (PrimaryQuality + SecondaryQuality + TertiaryQuality) - (other.PrimaryQuality + other.SecondaryQuality + other.TertiaryQuality);
        }
    }
}
