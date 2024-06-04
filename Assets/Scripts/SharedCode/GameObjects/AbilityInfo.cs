using System.Collections.Generic;
using System.Diagnostics.Tracing;

namespace Com.Tempest.Whale.GameObjects {

    public enum AbilityEnum {
        NONE = 0,

        FIND_THE_PATH = 1, // each hit gives a stack of crit chance for 3 turns.
        RETALIATION = 2, // user retaliates using their stronger attack stat whenever they are hit.
        SCATTER_BERRIES = 3, // user heals a random ally whenever hit.
        STICKY_SPORES = 4, // all status effects last 1 turn longer than normal.
        WORLD_TRAVELLER = 5, // has stab on all factions.
        FORECFUL_BLOWS = 6, // all hits daze for an additional 20%.
        HELPING_HAND = 7, // attacks targetting allies grant an additional 10 energy.
        CONTINUOUS_WINDING = 8, // attack stats increase by 5% per turn.

        RISING_TIDE = 9, // raises power whenever an ally is healed
        CHOKING_VINES = 10, // getting hit entangles the attacker by 20% for 3 turns
        FEED_THE_INFERNO = 11, // permanently raises power whenever an enemy takes burn damage
        MIRROR_ICE = 12, // reflects all negative status back to the attacker
        CRYSTALLINE = 13, // taking damage raises the associated defense stat
        MENTAL_GYMNASTICS = 14, // all debuffs that affect stats are applied as buffs instead
    }

    public class AbilityInfo {

        public AbilityEnum Ability { get; }
        public string AbilityName { get; }
        public string AbilityIconPath { get; }
        public string AbilityDescription { get; }

        public AbilityInfo(AbilityEnum ability, string abilityName, string abilityIconPath, string abilityDescription) {
            Ability = ability;
            AbilityName = abilityName;
            AbilityIconPath = abilityIconPath;
            AbilityDescription = abilityDescription;
        }
    }

    public class AbilityInfoContainer {

        private static Dictionary<AbilityEnum, AbilityInfo> abilityDict;

        public static void Initialize() {
            if (abilityDict != null) return;
            abilityDict = new Dictionary<AbilityEnum, AbilityInfo>();
            abilityDict[AbilityEnum.NONE] = new AbilityInfo(
                AbilityEnum.NONE, "None", "Icons/Attacks/None",
                "I haven't gotten around to giving this hero an ability yet.");

            // CombatMath.ApplyPassiveBuffs()
            abilityDict[AbilityEnum.FIND_THE_PATH] = new AbilityInfo(
                AbilityEnum.FIND_THE_PATH, "Find the Path", "Icons/Attacks/None",
                "Whenever this Gachafren attacks, gain 10% increased critical chance for 3 turns.");
            abilityDict[AbilityEnum.RETALIATION] = new AbilityInfo(
                AbilityEnum.RETALIATION, "Retaliation", "Icons/Attacks/None",
                "Whenever this Gachafren is attacked, it performs a counter attack with 100 strength.");
            abilityDict[AbilityEnum.SCATTER_BERRIES] = new AbilityInfo(
                AbilityEnum.SCATTER_BERRIES, "Scatter Berries", "Icons/Attacks/None",
                "Whenever this Gachafren is attacked, heal a random ally with 100 power.");
            // AttackInfo.ApplyAttackToEnemy()
            // AttackInfo.ApplyAttackToAlly()
            abilityDict[AbilityEnum.STICKY_SPORES] = new AbilityInfo(
                AbilityEnum.STICKY_SPORES, "Sticky Spores", "Icons/Attacks/None",
                "Whenever this Gachafren applies any status, the duration is increased by 1 turn.");
            // AttackInfo.ApplyAttackToEnemy()
            // AttackInfo.ApplyAttackToAlly()
            abilityDict[AbilityEnum.WORLD_TRAVELLER] = new AbilityInfo(
                AbilityEnum.WORLD_TRAVELLER, "World Traveller", "Icons/Attacks/None",
                "This Gachafren can use attacks of any type as though it were that type.");
            // AttackInfo.ApplyAttackToEnemy()
            abilityDict[AbilityEnum.FORECFUL_BLOWS] = new AbilityInfo(
                AbilityEnum.FORECFUL_BLOWS, "Forceful Blows", "Icons/Attacks/None",
                "Each attack applies a 20% daze for 2 turns.");
            // AttackInfo.ApplyAttackToAlly()
            abilityDict[AbilityEnum.HELPING_HAND] = new AbilityInfo(
                AbilityEnum.HELPING_HAND, "Helping Hand", "Icons/Attacks/None",
                "Moves that target allies grant an additional 20 energy to each target.");
            // CombatMath.EvaluatePassives()
            abilityDict[AbilityEnum.CONTINUOUS_WINDING] = new AbilityInfo(
                AbilityEnum.CONTINUOUS_WINDING, "Continuous Winding", "Icons/Attacks/None",
                "Permanently gain 10% to all attack stats at the end of each turn.");

            // CombatEvaluator.EvaluateRisingTide()
            abilityDict[AbilityEnum.RISING_TIDE] = new AbilityInfo(
                AbilityEnum.RISING_TIDE, "Rising Tide", "Icons/Attacks/None",
                "At the end of each turn, permanently gain 5% power for each instance of healing applied to any Gachafren this turn. (Heal over time effects do not count.)");
            // CombatMath.EvaluateNegativeSideEffects()
            abilityDict[AbilityEnum.CHOKING_VINES] = new AbilityInfo(
                AbilityEnum.CHOKING_VINES, "Choking Vines", "Icons/Attacks/None",
                "Whenever this Gachafren is attacked, the attacker is entangled by 20% for 3 turns.");
            // CombatEvaluator.EvaluateFeedTheInferno()
            abilityDict[AbilityEnum.FEED_THE_INFERNO] = new AbilityInfo(
                AbilityEnum.FEED_THE_INFERNO, "Feed the Inferno", "Icons/Attacks/GrowingFire",
                "At the end of each turn, permanently gain 5% power for each burn effect applied to any Gachafren this turn.");
            // AttackInfo.ApplyAttackToEnemy()
            abilityDict[AbilityEnum.MIRROR_ICE] = new AbilityInfo(
                AbilityEnum.MIRROR_ICE, "Mirror Ice", "Icons/Attacks/Reflection",
                "Negative status effects from power attacks get reflected back to the attacker.");
            // AttackInfo.ApplyAttackToEnemy()
            abilityDict[AbilityEnum.CRYSTALLINE] = new AbilityInfo(
                AbilityEnum.CRYSTALLINE, "Crystalline", "Icons/Attacks/None",
                "Whenever this Gachafren is attacked, the corresponding offense and defense stats are raised by 20% for 2 turns.");
            // CombatHero.GetModifiedStrength()
            // CombatHero.GetModifiedPower()
            // CombatHero.GetModifiedCrit()
            abilityDict[AbilityEnum.MENTAL_GYMNASTICS] = new AbilityInfo(
                AbilityEnum.MENTAL_GYMNASTICS, "Mental Gymnastics", "Icons/Attacks/MentalGymnastics",
                "Any time this Gachafren's offensive stats would be lowered, they are raised instead.");
        }

        public static AbilityInfo GetAbilityInfo(AbilityEnum ability) {
            if (abilityDict == null) Initialize();
            return abilityDict[ability];
        }
    }
}
