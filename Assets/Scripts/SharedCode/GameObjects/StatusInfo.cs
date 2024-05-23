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

        THORN_ARMOR = 10,
        LAVA_ARMOR = 11,
        ICE_ARMOR = 12,
        EARTH_ARMOR = 13,
        SHADY_BRANCHES = 14,

        ATTACK_UP = 15,
        MAGIC_UP = 16,
        ATTACK_MAGIC_UP = 17,
        DEFENSE_UP = 18,
        REFLECTION_UP = 19,
        DEFENSE_REFLECTION_UP = 20,
        SPEED_UP = 21,

        ATTACK_DOWN = 22,
        MAGIC_DOWN = 23,
        ATTACK_MAGIC_DOWN = 24,
        DEFENSE_DOWN = 25,
        REFLECTION_DOWN = 26,
        DEFENSE_REFLECTION_DOWN = 27,
        SPEED_DOWN = 28
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
            statusDict[StatusEnum.THORN_ARMOR] = new StatusInfo(StatusEnum.THORN_ARMOR, "Thorn Arm.", true, false, true);
            statusDict[StatusEnum.LAVA_ARMOR] = new StatusInfo(StatusEnum.LAVA_ARMOR, "Lava Arm.", true, false, true);
            statusDict[StatusEnum.ICE_ARMOR] = new StatusInfo(StatusEnum.ICE_ARMOR, "Ice Arm.", true, false, true);
            statusDict[StatusEnum.EARTH_ARMOR] = new StatusInfo(StatusEnum.EARTH_ARMOR, "Earth Arm.", true, false, true);
            statusDict[StatusEnum.SHADY_BRANCHES] = new StatusInfo(StatusEnum.SHADY_BRANCHES, "Shade", true, false, false);
            statusDict[StatusEnum.ATTACK_UP] = new StatusInfo(StatusEnum.ATTACK_UP, "Att. Up", true, true, false);
            statusDict[StatusEnum.MAGIC_UP] = new StatusInfo(StatusEnum.MAGIC_UP, "Mag. Up", true, true, false);
            statusDict[StatusEnum.ATTACK_MAGIC_UP] = new StatusInfo(StatusEnum.ATTACK_MAGIC_UP, "Off. Up", true, true, false);
            statusDict[StatusEnum.DEFENSE_UP] = new StatusInfo(StatusEnum.DEFENSE_UP, "Def. Up", true, false, true);
            statusDict[StatusEnum.REFLECTION_UP] = new StatusInfo(StatusEnum.REFLECTION_UP, "Ref. Up", true, false, true);
            statusDict[StatusEnum.DEFENSE_REFLECTION_UP] = new StatusInfo(StatusEnum.DEFENSE_REFLECTION_UP, "Def. Up", true, false, true);
            statusDict[StatusEnum.SPEED_UP] = new StatusInfo(StatusEnum.SPEED_UP, "Spe. Up", true, true, false);
            statusDict[StatusEnum.ATTACK_DOWN] = new StatusInfo(StatusEnum.ATTACK_DOWN, "Att. Down", false, true, false);
            statusDict[StatusEnum.MAGIC_DOWN] = new StatusInfo(StatusEnum.MAGIC_DOWN, "Mag. Down", false, true, false);
            statusDict[StatusEnum.ATTACK_MAGIC_DOWN] = new StatusInfo(StatusEnum.ATTACK_MAGIC_DOWN, "Off. Down", false, true, false);
            statusDict[StatusEnum.DEFENSE_DOWN] = new StatusInfo(StatusEnum.DEFENSE_DOWN, "Def. Down", false, false, true);
            statusDict[StatusEnum.REFLECTION_DOWN] = new StatusInfo(StatusEnum.REFLECTION_DOWN, "Ref. Down", false, false, true);
            statusDict[StatusEnum.DEFENSE_REFLECTION_DOWN] = new StatusInfo(StatusEnum.DEFENSE_REFLECTION_DOWN, "Def. Down", false, false, true);
            statusDict[StatusEnum.SPEED_DOWN] = new StatusInfo(StatusEnum.SPEED_DOWN, "Spe. Down", false, true, false);
        }

        public static StatusInfo GetStatusInfo(StatusEnum status) {
            if (statusDict == null) Intialize();
            return statusDict[status];
        }
    }
}
