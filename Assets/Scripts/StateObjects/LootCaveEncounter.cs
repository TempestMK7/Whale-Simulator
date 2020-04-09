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
    }
}
