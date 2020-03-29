using System.Collections.Generic;

namespace Com.Tempest.Whale.GameObjects {

    public enum AbilityEnum {
        NONE = 0,

        // Water
        WATER_BODY = 101, // Evaluate negative effects, apply to enemy
        VAPORIZE = 102, // Evaluate negative effects.
        CLEANSING_RAIN = 103, // Apply to ally

        // Grass
        BARK_SKIN = 201, // Evaluate negative effects.
        ABSORB_RAIN = 202, // Evaluate negative effects.
        DEEP_ROOTS = 203, // End of turn

        // Fire
        HOT_BLOODED = 301, // Evaluate negative effects, apply to enemy
        KINDLING = 302, // Evaluate negative effects.
        FEED_THE_INFERNO = 303, // End of turn weird

        // Ice
        MIRROR_ICE = 401, // Evaluate negative effects, apply to enemy
        COLD_BLOODED = 402, // Evaluate negative effects, apply to enemy

        // Earth
        JAGGED_SURFACE = 501, // Evaluate negative effects.
        MOUNTING_RAGE = 502, // End Of turn

        // Electric
        CONDUCTIVITY = 601, // Evaluate negative effects.
        MENTAL_GYMNASTICS = 602 // Apply to enemy, CombatHero.GetModifiedSpeed()
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

            // Water
            abilityDict[AbilityEnum.WATER_BODY] = new AbilityInfo(
                AbilityEnum.WATER_BODY, "Water Body", "Icons/Attacks/River",
                "This hero cannot be affected by the burn status.");
            abilityDict[AbilityEnum.VAPORIZE] = new AbilityInfo(
                AbilityEnum.VAPORIZE, "Vaporize", "Icons/Attacks/BlueLightning",
                "Taking damage from a fire ability raises magic and reflection by 20% for 2 turns.");
            abilityDict[AbilityEnum.CLEANSING_RAIN] = new AbilityInfo(
                AbilityEnum.CLEANSING_RAIN, "Cleansing Rain", "Icons/Attacks/Rainfall",
                "Whenever this hero heals an ally, reduce all negative status effects on that ally by 1 turn.");

            // Grass
            abilityDict[AbilityEnum.BARK_SKIN] = new AbilityInfo(
                AbilityEnum.BARK_SKIN, "Bark Skin", "Icons/Attacks/LeafCircle",
                "Taking damage from a physical attack raises defense by 20% for 2 turns.");
            abilityDict[AbilityEnum.ABSORB_RAIN] = new AbilityInfo(
                AbilityEnum.ABSORB_RAIN, "Absorb Rain", "Icons/Attacks/LightningShield",
                "Taking damage from a water ability raises attack and defense by 20% for 2 turns.");
            abilityDict[AbilityEnum.DEEP_ROOTS] = new AbilityInfo(
                AbilityEnum.DEEP_ROOTS, "Deep Roots", "Icons/Attacks/Roots",
                "Recover 5% of missing health at the end of each turn.");

            // Fire
            abilityDict[AbilityEnum.HOT_BLOODED] = new AbilityInfo(
                AbilityEnum.HOT_BLOODED, "Hot Blooded", "Icons/Attacks/StaffFire",
                "This hero cannot be affected by the poison status.");
            abilityDict[AbilityEnum.KINDLING] = new AbilityInfo(
                AbilityEnum.KINDLING, "Kindling", "Icons/Attacks/BrokenShield",
                "Taking damage from a grass ability raises magic and speed by 20% for 2 turns.");
            abilityDict[AbilityEnum.FEED_THE_INFERNO] = new AbilityInfo(
                AbilityEnum.FEED_THE_INFERNO, "Feed the Inferno", "Icons/Attacks/GrowingFire",
                "Whenever any hero receives the burn status, raise magic by 5% for the rest of the encounter.");

            // Ice
            abilityDict[AbilityEnum.MIRROR_ICE] = new AbilityInfo(
                AbilityEnum.MIRROR_ICE, "Mirror Ice", "Icons/Attacks/Reflection",
                "Negative status caused by magic abilities get reflected back to the attacker.");
            abilityDict[AbilityEnum.COLD_BLOODED] = new AbilityInfo(
                AbilityEnum.COLD_BLOODED, "Cold Blooded", "Icons/Attacks/StaffIce",
                "This hero cannot be affected by the chill or freeze status.");

            // Earth
            abilityDict[AbilityEnum.JAGGED_SURFACE] = new AbilityInfo(
                AbilityEnum.JAGGED_SURFACE, "Jagged Surface", "Icons/Attacks/StoneShield",
                "20% of damage taken from melee attacks is inflicted to the attacker.");
            abilityDict[AbilityEnum.MOUNTING_RAGE] = new AbilityInfo(
                AbilityEnum.MOUNTING_RAGE, "Mounting Rage", "Icons/Attacks/Angery",
                "Permanently increase attack by 20% at the end of each round.");

            // Electric
            abilityDict[AbilityEnum.CONDUCTIVITY] = new AbilityInfo(
                AbilityEnum.CONDUCTIVITY, "Conductivity", "Icons/Attacks/StaffElectric",
                "Taking damage from a magic ability raises magic by 20% for 2 turns.");
            abilityDict[AbilityEnum.MENTAL_GYMNASTICS] = new AbilityInfo(
                AbilityEnum.MENTAL_GYMNASTICS, "Mental Gymnastics", "Icons/Attacks/MentalGymnastics",
                "Any time a magic down or speed down status would be applied to this hero, receive a magic up or speed up for the same amount.");
        }

        public static AbilityInfo GetAbilityInfo(AbilityEnum ability) {
            if (abilityDict == null) Initialize();
            return abilityDict[ability];
        }
    }
}
