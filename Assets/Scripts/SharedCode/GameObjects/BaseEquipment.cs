using Com.Tempest.Whale.Combat;
using Com.Tempest.Whale.StateObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Tempest.Whale.GameObjects {

    public enum EquipmentSlot {
        HEAD = 1,
        NECK = 2,
        BELT = 3
    }

    public enum EquipmentStat {
        STRENGTH = 1,
        POWER = 2,
        TOUGHNESS = 3,
        RESISTANCE = 4,

        HEALTH = 11,
        SPEED = 12,
        CRITICAL = 13,
        DEFLECTION = 14,

        APTITUDE = 21, // Damage/Healing bonus when using off-faction moves.
        PRECISION = 22, // Critical hit damage
        REFLEX = 23, // Deflection damage reduction
        PERSISTENCE = 24, // Increase to damage and healing over time effects
        DURABILITY = 25, // Flat damage reduction
        VIGOR = 26, // Increases energy generation
    }

    public class BaseEquipmentContainer {

        private static Dictionary<EquipmentSlot, string> pathDict = new Dictionary<EquipmentSlot, string>() {
            [EquipmentSlot.HEAD] = "Icons/Equipment/Head/head_",
            [EquipmentSlot.NECK] = "Icons/Equipment/Neck/necklace_",
            [EquipmentSlot.BELT] = "Icons/Equipment/Waist/Belt_"
        };

        private static Dictionary<EquipmentSlot, string> slotNameDict = new Dictionary<EquipmentSlot, string>() {
            [EquipmentSlot.HEAD] = "Hat",
            [EquipmentSlot.NECK] = "Necklace",
            [EquipmentSlot.BELT] = "Belt"
        };

        private static Dictionary<EquipmentStat, string> statNameDict = new Dictionary<EquipmentStat, string>() {
            [EquipmentStat.STRENGTH] = "Strength",
            [EquipmentStat.POWER] = "Power",
            [EquipmentStat.TOUGHNESS] = "Toughness",
            [EquipmentStat.RESISTANCE] = "Resistance",
            [EquipmentStat.HEALTH] = "Health",
            [EquipmentStat.SPEED] = "Speed",
            [EquipmentStat.CRITICAL] = "Critical",
            [EquipmentStat.DEFLECTION] = "Deflection",
            [EquipmentStat.APTITUDE] = "Aptitude",
            [EquipmentStat.PRECISION] = "Precision",
            [EquipmentStat.REFLEX] = "Reflex",
            [EquipmentStat.PERSISTENCE] = "Persistence",
            [EquipmentStat.DURABILITY] = "Durability",
            [EquipmentStat.VIGOR] = "Vigor",
        };

        private static Dictionary<EquipmentStat, string> statDescriptionDict = new Dictionary<EquipmentStat, string>() {
            [EquipmentStat.APTITUDE] = "Aptitude raises the effect of moves that are not the same type as the user.",
            [EquipmentStat.PRECISION] = "Precision raises the user's critical hit multiplier.  The base critical hit multiplier is 150%.",
            [EquipmentStat.REFLEX] = "Reflex raises the user's deflection multiplier.  The base deflection modifier is 150%.",
            [EquipmentStat.PERSISTENCE] = "Persistence raises the effect statuses that apply damage or healing.",
            [EquipmentStat.DURABILITY] = "Durability adds a flat damage reduction that is applied after damage has been calculated.",
            [EquipmentStat.VIGOR] = "Vigor adds a flat amount of energy generation to each basic attack.  This energy is only given to the user of the attack.",
        };

        private static Dictionary<EquipmentStat, Dictionary<EquipmentStat, string>> primaryNameDict = new Dictionary<EquipmentStat, Dictionary<EquipmentStat, string>>() {
            [EquipmentStat.STRENGTH] = new Dictionary<EquipmentStat, string>() {
                [EquipmentStat.HEALTH] = "Soldier's",
                [EquipmentStat.SPEED] = "Athlete's",
                [EquipmentStat.CRITICAL] = "Rogue's",
                [EquipmentStat.DEFLECTION] = "Monk's"
            },
            [EquipmentStat.POWER] = new Dictionary<EquipmentStat, string>() {
                [EquipmentStat.HEALTH] = "Alchemist's",
                [EquipmentStat.SPEED] = "Sorcerer's",
                [EquipmentStat.CRITICAL] = "Wizard's",
                [EquipmentStat.DEFLECTION] = "Druid's"
            },
            [EquipmentStat.TOUGHNESS] = new Dictionary<EquipmentStat, string>() {
                [EquipmentStat.HEALTH] = "Survivalist's",
                [EquipmentStat.SPEED] = "Warrior's",
                [EquipmentStat.CRITICAL] = "Ranger's",
                [EquipmentStat.DEFLECTION] = "Defender's"
            },
            [EquipmentStat.RESISTANCE] = new Dictionary<EquipmentStat, string>() {
                [EquipmentStat.HEALTH] = "Paladin's",
                [EquipmentStat.SPEED] = "Champion's",
                [EquipmentStat.CRITICAL] = "Oracle's",
                [EquipmentStat.DEFLECTION] = "Cleric's"
            },
        };

        private static Dictionary<EquipmentStat, string> tertiaryNameDict = new Dictionary<EquipmentStat, string> {
            [EquipmentStat.APTITUDE] = "Aptitude",
            [EquipmentStat.PRECISION] = "Precision",
            [EquipmentStat.REFLEX] = "Reflex",
            [EquipmentStat.PERSISTENCE] = "Persistence",
            [EquipmentStat.DURABILITY] = "Durability",
            [EquipmentStat.VIGOR] = "Vigor",
        };

        public static string GetEquipmentIcon(EquipmentSlot slot, int iconNumber) {
            return pathDict[slot] + iconNumber.ToString("00");
        }

        public static string GetStatName(EquipmentStat stat) {
            return statNameDict[stat];
        }

        public static string GetStatDescription(EquipmentStat tertiaryStat) {
            return statDescriptionDict[tertiaryStat];
        }

        public static string GetEquipmentName(EquipmentSlot slot, EquipmentStat primaryStat, EquipmentStat secondaryStat, EquipmentStat tertiaryStat) {
            StringBuilder nameBuilder = new StringBuilder();
            nameBuilder.Append(primaryNameDict[primaryStat][secondaryStat]);
            nameBuilder.Append(' ');
            nameBuilder.Append(slotNameDict[slot]);
            nameBuilder.Append(" of ");
            nameBuilder.Append(tertiaryNameDict[tertiaryStat]);
            return nameBuilder.ToString();
        }

        public static string GetEquipmentName(AccountEquipment equipment) {
            return GetEquipmentName(equipment.Slot, equipment.PrimaryStat, equipment.SecondaryStat, equipment.TertiaryStat);
        }

        public static AccountEquipment GenerateRandomEquipment(int level, EquipmentSlot? slot = null, EquipmentStat? primaryStat = null, EquipmentStat? secondaryStat = null, EquipmentStat? tertiaryStat = null) {
            if (slot == null) {
                var allSlots = (EquipmentSlot[])Enum.GetValues(typeof(EquipmentSlot));
                slot = allSlots[CombatMath.RandomInt(0, allSlots.Length)];
            }
            if (primaryStat == null) {
                var allPrimaryStats = new EquipmentStat[] { EquipmentStat.STRENGTH, EquipmentStat.POWER, EquipmentStat.TOUGHNESS, EquipmentStat.RESISTANCE };
                primaryStat = allPrimaryStats[CombatMath.RandomInt(0, allPrimaryStats.Length)];
            }
            if (secondaryStat == null) {
                var allSecondaryStats = new EquipmentStat[] { EquipmentStat.HEALTH, EquipmentStat.SPEED, EquipmentStat.CRITICAL, EquipmentStat.DEFLECTION };
                secondaryStat = allSecondaryStats[CombatMath.RandomInt(0, allSecondaryStats.Length)];
            }
            if (tertiaryStat == null) {
                var allTertiaryStats = new EquipmentStat[] { EquipmentStat.APTITUDE, EquipmentStat.PRECISION, EquipmentStat.REFLEX, EquipmentStat.PERSISTENCE, EquipmentStat.DURABILITY, EquipmentStat.VIGOR };
                tertiaryStat = allTertiaryStats[CombatMath.RandomInt(0, allTertiaryStats.Length)];
            }
            var iconMaxIndex = 1;
            switch (slot) {
                case EquipmentSlot.HEAD:
                    iconMaxIndex = 15;
                    break;
                case EquipmentSlot.NECK:
                    iconMaxIndex = 34;
                    break;
                case EquipmentSlot.BELT:
                    iconMaxIndex = 36;
                    break;
            }
            var iconIndex = CombatMath.RandomInt(0, iconMaxIndex) + 1;

            return new AccountEquipment((EquipmentSlot)slot, (EquipmentStat)primaryStat, (EquipmentStat)secondaryStat, (EquipmentStat)tertiaryStat, CombatMath.RandomInt(1, 11), CombatMath.RandomInt(1, 11), CombatMath.RandomInt(1, 11), level, iconIndex);
        }
    }
}
