using System.Collections;
using System.Collections.Generic;
using Com.Tempest.Whale.StateObjects;

namespace Com.Tempest.Whale.GameObjects {

    public class PotentialRewardsContainer {

        public int Summons { get; }
        public int Gold { get; }
        public int Souls { get; }
        public int PlayerExperience { get; }
        public int NumberEquipment { get; }
        public int MaxEquipmentLevel { get; }

        public PotentialRewardsContainer(int summons, int gold, int souls, int playerExperience, int numberEquipment, int maxEquipmentLevel) {
            Summons = summons;
            Gold = gold;
            Souls = souls;
            PlayerExperience = playerExperience;
            NumberEquipment = numberEquipment;
            MaxEquipmentLevel = maxEquipmentLevel;
        }
    }

    public class EarnedRewardsContainer {

        public int Summons { get; }
        public int Gold { get; }
        public int Souls { get; }
        public int PlayerExperience { get; }
        public List<AccountEquipment> EarnedEquipment { get; }

        public EarnedRewardsContainer(int summons, int gold, int souls, int playerExperience, List<AccountEquipment> earnedEquipment) {
            Summons = summons;
            Gold = gold;
            Souls = souls;
            PlayerExperience = playerExperience;
            EarnedEquipment = earnedEquipment;
        }
    }
}
