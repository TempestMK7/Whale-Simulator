using System;
using System.Collections.Generic;
using Com.Tempest.Whale.GameObjects;

namespace Com.Tempest.Whale.StateObjects {

    [Serializable]
    public class LootCaveEncounter {

        public Guid Id { get; set; }
        public string Date { get; set; }
        public int Floor { get; set; }
        public HeroEnum Position1Hero { get; set; }
        public HeroEnum Position2Hero { get; set; }
        public HeroEnum Position3Hero { get; set; }
        public HeroEnum Position4Hero { get; set; }
        public HeroEnum Position5Hero { get; set; }

        public LootCaveEncounter() {

        }

        public LootCaveEncounter(int floor) {
            Id = Guid.NewGuid();
            Floor = floor;
            Date = EpochTime.GetCurrentDate();

            var rand = new Random((int)EpochTime.CurrentTimeMillis());
            Position1Hero = BaseHeroContainer.ChooseRandomHero(RoleEnum.PROTECTION, rand);
            Position2Hero = BaseHeroContainer.ChooseRandomHero(RoleEnum.DAMAGE, rand);
            Position3Hero = BaseHeroContainer.ChooseRandomHero(RoleEnum.DAMAGE, rand);
            Position4Hero = BaseHeroContainer.ChooseRandomHero(RoleEnum.DAMAGE, rand);
            Position5Hero = BaseHeroContainer.ChooseRandomHero(RoleEnum.SUPPORT, rand);
        }

        public int GetCurrentLevel() {
            return Floor * 3;
        }

        public int GetAwakeningLevel() {
            var awakening = (GetCurrentLevel() / 10) + 1;
            return Math.Min(awakening, 10);
        }

        public PotentialRewardsContainer GetPotentialRewards() {
            var awakening = GetAwakeningLevel();
            return new PotentialRewardsContainer() {
                GoldMin = 1,
                GoldMax = (int)Math.Pow(10, Math.Ceiling(awakening / 3.0) + 1),
                RedCrystalsMin = -2,
                RedCrystalsMax = (int)Math.Ceiling(awakening / 2.0),
                BlueCrystalsMin = -4,
                BlueCrystalsMax = awakening > 5 ? 1 : 0,
                SilverDustMin = 1,
                SilverDustMax = 100 + 10 * awakening,
                GoldDustMin = -100,
                GoldDustMax = -50 + (10 * awakening),
                OldPagesMin = -2,
                OldPagesMax = awakening / 3,
                AncientPagesMin = -9,
                AncientPagesMax = awakening / 6,
                TreatsMin = -3,
                TreatsMax = 6,
                TreatsMaxSize = awakening / 4,
                NumberEquipmentMin = -2,
                NumberEquipmentMax = 3,
                EquipmentLevelMin = 1 + (awakening / 4),
                EquipmentLevelMax = (int)Math.Round(awakening / 2.0)
            };
        }
    }
}
