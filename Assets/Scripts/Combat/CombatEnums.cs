﻿using System;

namespace Com.Tempest.Whale.Combat {

    public enum HitType {
        NORMAL = 1, CRITICAL = 2, DEFLECTION = 3
    }

    public enum HitEffectivity {
        NORMAL = 1, EMPOWERED = 2, RESISTED = 3
    }

    public enum TargetType {
        NONE = 0, SELF = 1, FIRST_ALIVE = 2, RANDOM = 3, LOWEST_HEALTH = 4, HIGHEST_HEALTH = 5, LOWEST_ENERGY = 6, HIGHEST_ENERGY = 7
    }
}
