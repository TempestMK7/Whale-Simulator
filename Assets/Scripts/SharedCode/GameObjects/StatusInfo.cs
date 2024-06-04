using System;
using System.Collections.Generic;

namespace Com.Tempest.Whale.GameObjects {

    public enum StatusEnum {
        DOWSE = 1,
        CHILL = 2,
        DAZE = 3,
        BLIND = 4,
        ROOT = 5,

        BLEED = 6,
        BURN = 7,
        POISON = 8,

        REGENERATION = 9,

        STRENGTH_UP = 10,
        POWER_UP = 11,
        OFFENSE_UP = 12,
        TOUGHNESS_UP = 13,
        RESISTANCE_UP = 14,
        DEFENSE_UP = 15,
        SPEED_UP = 16,
        CRITICAL_UP = 17,
        DEFLECTION_UP = 18,

        STRENGTH_DOWN = 19,
        POWER_DOWN = 20,
        OFFENSE_DOWN = 21,
        TOUGHNESS_DOWN = 22,
        RESISTANCE_DOWN = 23,
        DEFENSE_DOWN = 24,
        SPEED_DOWN = 25,
        CRITICAL_DOWN = 26,
        DEFLECTION_DOWN = 27,

        THORN_ARMOR = 28,
        LAVA_ARMOR = 29,
        ICE_ARMOR = 30,
        EARTH_ARMOR = 31,
        SHADY_BRANCHES = 32,
        HIGH_GROUND = 33,
        REVERSE_POLARITY = 34,
    }

    public class StatusInfo {

        public StatusEnum Status { get; }
        public string StatusName { get; }
        public bool IsBeneficial { get; }
        public bool ModifiesOffense { get; }
        public bool ModifiesDefense { get; }

        public StatusInfo(StatusEnum status, string statusName, bool isBeneficial, bool modifiesOffense, bool modifiesDefense) {
            Status = status;
            StatusName = statusName;
            ModifiesOffense = modifiesOffense;
            ModifiesDefense = modifiesDefense;
            IsBeneficial = isBeneficial;
        }
    }

    public class StatusInfoContainer {

        private static Dictionary<StatusEnum, StatusInfo> statusDict;

        public static void Intialize() {
            statusDict = new Dictionary<StatusEnum, StatusInfo>();
            statusDict[StatusEnum.DOWSE] = new StatusInfo(StatusEnum.DOWSE, "Dowse", false, false, false);
            statusDict[StatusEnum.CHILL] = new StatusInfo(StatusEnum.CHILL, "Chill", false, true, false);
            statusDict[StatusEnum.DAZE] = new StatusInfo(StatusEnum.DAZE, "Daze", false, true, false);
            statusDict[StatusEnum.BLIND] = new StatusInfo(StatusEnum.BLIND, "Blind", false, true, false);
            statusDict[StatusEnum.ROOT] = new StatusInfo(StatusEnum.ROOT, "Root", false, true, false);
            statusDict[StatusEnum.BLEED] = new StatusInfo(StatusEnum.BLEED, "Bleed", false, false, false);
            statusDict[StatusEnum.BURN] = new StatusInfo(StatusEnum.BURN, "Burn", false, false, false);
            statusDict[StatusEnum.POISON] = new StatusInfo(StatusEnum.POISON, "Poison", false, false, false);
            statusDict[StatusEnum.REGENERATION] = new StatusInfo(StatusEnum.REGENERATION, "Regen", true, true, true);
            statusDict[StatusEnum.STRENGTH_UP] = new StatusInfo(StatusEnum.STRENGTH_UP, "Str. Up", true, true, false);
            statusDict[StatusEnum.POWER_UP] = new StatusInfo(StatusEnum.POWER_UP, "Pow. Up", true, true, false);
            statusDict[StatusEnum.OFFENSE_UP] = new StatusInfo(StatusEnum.OFFENSE_UP, "Off. Up", true, true, false);
            statusDict[StatusEnum.TOUGHNESS_UP] = new StatusInfo(StatusEnum.TOUGHNESS_UP, "Tuf. Up", true, false, true);
            statusDict[StatusEnum.RESISTANCE_UP] = new StatusInfo(StatusEnum.RESISTANCE_UP, "Res. Up", true, false, true);
            statusDict[StatusEnum.DEFENSE_UP] = new StatusInfo(StatusEnum.DEFENSE_UP, "Def. Up", true, false, true);
            statusDict[StatusEnum.SPEED_UP] = new StatusInfo(StatusEnum.SPEED_UP, "Spe. Up", true, true, false);
            statusDict[StatusEnum.CRITICAL_UP] = new StatusInfo(StatusEnum.CRITICAL_UP, "Crit. Up", true, true, false);
            statusDict[StatusEnum.DEFLECTION_UP] = new StatusInfo(StatusEnum.DEFLECTION_UP, "Defl. Up", true, false, true);
            statusDict[StatusEnum.STRENGTH_DOWN] = new StatusInfo(StatusEnum.STRENGTH_DOWN, "Str. Down", false, true, false);
            statusDict[StatusEnum.POWER_DOWN] = new StatusInfo(StatusEnum.POWER_DOWN, "Pow. Down", false, true, false);
            statusDict[StatusEnum.OFFENSE_DOWN] = new StatusInfo(StatusEnum.OFFENSE_DOWN, "Off. Down", false, true, false);
            statusDict[StatusEnum.TOUGHNESS_DOWN] = new StatusInfo(StatusEnum.TOUGHNESS_DOWN, "Tuf. Down", false, false, true);
            statusDict[StatusEnum.RESISTANCE_DOWN] = new StatusInfo(StatusEnum.RESISTANCE_DOWN, "Res. Down", false, false, true);
            statusDict[StatusEnum.DEFENSE_DOWN] = new StatusInfo(StatusEnum.DEFENSE_DOWN, "Def. Down", false, false, true);
            statusDict[StatusEnum.SPEED_DOWN] = new StatusInfo(StatusEnum.SPEED_DOWN, "Spe. Down", false, true, false);
            statusDict[StatusEnum.CRITICAL_DOWN] = new StatusInfo(StatusEnum.CRITICAL_DOWN, "Crit. Down", false, true, false);
            statusDict[StatusEnum.DEFLECTION_DOWN] = new StatusInfo(StatusEnum.DEFLECTION_DOWN, "Defl. Down", false, false, true);
            statusDict[StatusEnum.THORN_ARMOR] = new StatusInfo(StatusEnum.THORN_ARMOR, "Thorn Arm.", true, false, true);
            statusDict[StatusEnum.LAVA_ARMOR] = new StatusInfo(StatusEnum.LAVA_ARMOR, "Lava Arm.", true, false, true);
            statusDict[StatusEnum.ICE_ARMOR] = new StatusInfo(StatusEnum.ICE_ARMOR, "Ice Arm.", true, false, true);
            statusDict[StatusEnum.EARTH_ARMOR] = new StatusInfo(StatusEnum.EARTH_ARMOR, "Earth Arm.", true, false, true);
            statusDict[StatusEnum.SHADY_BRANCHES] = new StatusInfo(StatusEnum.SHADY_BRANCHES, "Shade", true, false, false);
            statusDict[StatusEnum.HIGH_GROUND] = new StatusInfo(StatusEnum.HIGH_GROUND, "High Ground", true, true, true);
            statusDict[StatusEnum.REVERSE_POLARITY] = new StatusInfo(StatusEnum.REVERSE_POLARITY, "Rev. Pol.", false, true, true);
        }

        public static StatusInfo GetStatusInfo(StatusEnum status) {
            if (statusDict == null) Intialize();
            return statusDict[status];
        }
    }
}
