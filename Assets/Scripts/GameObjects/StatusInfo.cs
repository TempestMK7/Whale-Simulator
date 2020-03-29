using System;
using System.Collections.Generic;

namespace Com.Tempest.Whale.GameObjects {

    public enum StatusEnum {
        STUN = 1,
        BLIND = 2,
        ROOT = 3,

        BLEED = 4,
        BURN = 5,
        POISON = 6,

        REGENERATION = 7,

        THORN_ARMOR = 8,
        LAVA_ARMOR = 9,
        ICE_ARMOR = 10,
        EARTH_ARMOR = 11,

        DOWSE = 12,
        CHILL = 13,
        FREEZE = 14,

        DAZE = 15,

        ATTACK_UP = 16,
        MAGIC_UP = 17,
        DEFENSE_UP = 18,
        REFLECTION_UP = 19,
        SPEED_UP = 20,

        ATTACK_DOWN = 21,
        MAGIC_DOWN = 22,
        DEFENSE_DOWN = 23,
        REFLECTION_DOWN = 24,
        SPEED_DOWN = 25
    }

    public class StatusInfo {

        public StatusEnum Status { get; }
        public string StatusName { get; }
        public FactionEnum AssociatedFaction { get; }
        public bool IsBeneficial { get; }
        public bool ModifiesAttack { get; }
        public bool BlocksMelee { get; }
        public bool BlocksRanged { get; }

        public StatusInfo(StatusEnum status, string statusName, FactionEnum associatedFaction,
            bool isBeneficial, bool modifiesAttack, bool blocksMelee, bool blocksRanged) {
            Status = status;
            StatusName = statusName;
            AssociatedFaction = associatedFaction;
            IsBeneficial = isBeneficial;
            ModifiesAttack = modifiesAttack;
            BlocksMelee = blocksMelee;
            BlocksRanged = blocksRanged;
        }
    }

    public class StatusInfoContainer {

        private static Dictionary<StatusEnum, StatusInfo> statusDict;

        public static void Intialize() {
            statusDict = new Dictionary<StatusEnum, StatusInfo>();
            statusDict[StatusEnum.STUN] = new StatusInfo(StatusEnum.STUN, "Stun", FactionEnum.ELECTRIC,
                false, true, true, true);
            statusDict[StatusEnum.BLIND] = new StatusInfo(StatusEnum.BLIND, "Blind", FactionEnum.ELECTRIC,
                false, true, false, true);
            statusDict[StatusEnum.ROOT] = new StatusInfo(StatusEnum.ROOT, "Root", FactionEnum.GRASS,
                false, true, true, false);
            statusDict[StatusEnum.BLEED] = new StatusInfo(StatusEnum.BLEED, "Bleed", FactionEnum.GRASS,
                false, false, false, false);
            statusDict[StatusEnum.BURN] = new StatusInfo(StatusEnum.BURN, "Burn", FactionEnum.FIRE,
                false, false, false, false);
            statusDict[StatusEnum.POISON] = new StatusInfo(StatusEnum.POISON, "Poison", FactionEnum.GRASS,
                false, false, false, false);
            statusDict[StatusEnum.REGENERATION] = new StatusInfo(StatusEnum.REGENERATION, "Regen", FactionEnum.WATER,
                true, false, false, false);
            statusDict[StatusEnum.THORN_ARMOR] = new StatusInfo(StatusEnum.THORN_ARMOR, "Thorn Arm.", FactionEnum.GRASS,
                true, false, false, false);
            statusDict[StatusEnum.LAVA_ARMOR] = new StatusInfo(StatusEnum.LAVA_ARMOR, "Lava Arm.", FactionEnum.FIRE,
                true, false, false, false);
            statusDict[StatusEnum.ICE_ARMOR] = new StatusInfo(StatusEnum.ICE_ARMOR, "Ice Arm.", FactionEnum.ICE,
                true, false, false, false);
            statusDict[StatusEnum.EARTH_ARMOR] = new StatusInfo(StatusEnum.EARTH_ARMOR, "Earth Arm.", FactionEnum.EARTH,
                true, false, false, false);
            statusDict[StatusEnum.DOWSE] = new StatusInfo(StatusEnum.DOWSE, "Dowse", FactionEnum.WATER,
                false, true, false, false);
            statusDict[StatusEnum.CHILL] = new StatusInfo(StatusEnum.CHILL, "Chill", FactionEnum.ICE,
                false, true, false, false);
            statusDict[StatusEnum.FREEZE] = new StatusInfo(StatusEnum.FREEZE, "Freeze", FactionEnum.ICE,
                false, true, true, true);
            statusDict[StatusEnum.DAZE] = new StatusInfo(StatusEnum.DAZE, "Daze", FactionEnum.ELECTRIC,
                false, true, false, false);
            statusDict[StatusEnum.ATTACK_UP] = new StatusInfo(StatusEnum.ATTACK_UP, "Att. Up", FactionEnum.GRASS,
                true, true, false, false);
            statusDict[StatusEnum.MAGIC_UP] = new StatusInfo(StatusEnum.MAGIC_UP, "Mag. Up", FactionEnum.FIRE,
                true, true, false, false);
            statusDict[StatusEnum.DEFENSE_UP] = new StatusInfo(StatusEnum.DEFENSE_UP, "Def. Up", FactionEnum.EARTH,
                true, false, false, false);
            statusDict[StatusEnum.REFLECTION_UP] = new StatusInfo(StatusEnum.REFLECTION_UP, "Ref. Up", FactionEnum.ICE,
                true, false, false, false);
            statusDict[StatusEnum.SPEED_UP] = new StatusInfo(StatusEnum.SPEED_UP, "Spe. Up", FactionEnum.ELECTRIC,
                true, true, false, false);
            statusDict[StatusEnum.ATTACK_DOWN] = new StatusInfo(StatusEnum.ATTACK_DOWN, "Att. Down", FactionEnum.GRASS,
                false, true, false, false);
            statusDict[StatusEnum.MAGIC_DOWN] = new StatusInfo(StatusEnum.MAGIC_DOWN, "Mag. Down", FactionEnum.FIRE,
                false, true, false, false);
            statusDict[StatusEnum.DEFENSE_DOWN] = new StatusInfo(StatusEnum.DEFENSE_DOWN, "Def. Down", FactionEnum.EARTH,
                false, false, false, false);
            statusDict[StatusEnum.REFLECTION_DOWN] = new StatusInfo(StatusEnum.REFLECTION_DOWN, "Ref. Down", FactionEnum.ICE,
                false, false, false, false);
            statusDict[StatusEnum.SPEED_DOWN] = new StatusInfo(StatusEnum.SPEED_DOWN, "Spe. Down", FactionEnum.ELECTRIC,
                false, true, false, false);
        }

        public static StatusInfo GetStatusInfo(StatusEnum status) {
            if (statusDict == null) Intialize();
            return statusDict[status];
        }
    }
}
