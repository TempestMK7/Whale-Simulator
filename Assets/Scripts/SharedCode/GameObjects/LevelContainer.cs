using System;
using System.Collections.Generic;
using Com.Tempest.Whale.StateObjects;

namespace Com.Tempest.Whale.GameObjects {

    public class LevelContainer {

        private static readonly FusionRequirement[] fusionRequirements = {
            new FusionRequirement(1, 1, 1, 2, 1, true),
            new FusionRequirement(2, 1, 2, 2, 2, true),
            new FusionRequirement(3, 1, 3, 2, 3, true),
            new FusionRequirement(4, 1, 4, 2, 4, true),
            new FusionRequirement(5, 1, 5, 3, 5, true),
            new FusionRequirement(6, 0, 5, 1, 6, true),
            new FusionRequirement(7, 0, 5, 2, 6, true),
            new FusionRequirement(8, 0, 5, 3, 6, true),
            new FusionRequirement(9, 2, 5, 1, 9, false)
        };

        public static int MaxLevelForAwakeningValue(int awakeningValue) {
            if (awakeningValue <= 5) return 75;
            return 50 + (awakeningValue * 5);
        }

        public static FusionRequirement? GetFusionRequirementForLevel(int awakeningLevel) {
            if (awakeningLevel >= 10) return null;
            return fusionRequirements[awakeningLevel - 1];
        }

        public static bool FusionIsLegal(AccountHero fusedHero, List<AccountHero> destroyedHeroes) {
            FusionRequirement? requirement = GetFusionRequirementForLevel(fusedHero.AwakeningLevel);
            if (requirement == null) return false;

            int selectedSameHeroes = 0;
            int selectedFactionHeroes = 0;
            foreach (AccountHero destroyed in destroyedHeroes) {
                if (destroyed == fusedHero) return false;
                if (destroyed.GetBaseHero().Hero == fusedHero.GetBaseHero().Hero && destroyed.AwakeningLevel == requirement?.SameHeroLevel && selectedSameHeroes != requirement?.SameHeroRequirement) {
                    selectedSameHeroes++;
                } else if (destroyed.AwakeningLevel == requirement?.FactionHeroLevel) {
                    if (requirement?.RequireSameFaction == false || destroyed.GetBaseHero().Faction == fusedHero.GetBaseHero().Faction) selectedFactionHeroes++;
                    else return false;
                } else {
                    return false;
                }
            }
            return selectedSameHeroes == requirement?.SameHeroRequirement && selectedFactionHeroes == requirement?.FactionHeroRequirement;
        }

        public static bool FusionIsLegal(AccountEquipment fusedEquipment, List<AccountEquipment> destroyedEquipment) {
            int selectedEquipment = 0;
            foreach (AccountEquipment destroyed in destroyedEquipment) {
                if (destroyed == fusedEquipment) return false;
                if (destroyed == null) return false;
                if (destroyed.Slot == fusedEquipment.Slot && destroyed.Level == fusedEquipment.Level) {
                    selectedEquipment++;
                } else {
                    return false;
                }
            }
            return selectedEquipment == 2;
        }
    }

    public struct FusionRequirement {

        public int CurrentAwakeningLevel { get; }
        public int SameHeroRequirement { get; }
        public int SameHeroLevel { get; }
        public int FactionHeroRequirement { get; }
        public int FactionHeroLevel { get; }
        public bool RequireSameFaction { get; }

        public FusionRequirement(int currentAwakeningLevel, int sameHeroRequirement, int sameHeroLevel, int factionHeroRequirement, int factionHeroLevel, bool requireSameFaction) {
            CurrentAwakeningLevel = currentAwakeningLevel;
            SameHeroRequirement = sameHeroRequirement;
            SameHeroLevel = sameHeroLevel;
            FactionHeroRequirement = factionHeroRequirement;
            FactionHeroLevel = factionHeroLevel;
            RequireSameFaction = requireSameFaction;
        }
    }
}
