using System.Collections;

namespace Com.Tempest.Whale.GameObjects {

    public enum BattleEnum {
        NONE, CAMPAIGN, LOOT_CAVE
    }

    public enum RoleEnum {
        PROTECTION = 1, DAMAGE = 2, SUPPORT = 3
    }

    public enum FactionEnum {
        NONE = 0, WATER = 1, GRASS = 2, FIRE = 3, ICE = 4, EARTH = 5, ELECTRIC = 6
    }

    public enum AttackParticleEnum {
        NONE, WATER, GRASS, FIRE, ICE, EARTH, ELECTRIC
    }

    public enum ParticleOriginEnum {
        ATTACKER, OVERHEAD, TARGET
    }
}
