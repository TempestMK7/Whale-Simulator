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

        public static string GetEquipmentName(EquipmentSlot slot, EquipmentStat primaryStat, EquipmentStat secondaryStat, EquipmentStat tertiaryStat) {
            StringBuilder nameBuilder = new StringBuilder();
            nameBuilder.Append(primaryNameDict[primaryStat][secondaryStat]);
            nameBuilder.Append(' ');
            nameBuilder.Append(slotNameDict[slot]);
            nameBuilder.Append(" of ");
            nameBuilder.Append(tertiaryNameDict[tertiaryStat]);
            return nameBuilder.ToString();
        }
    }
}
