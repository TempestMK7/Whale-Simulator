using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackParticleContainer {

    private static Dictionary<AttackParticleEnum, AnimatorOverrideController> particleDict;

    public static void Initialize() {
        if (particleDict != null) return;
        particleDict = new Dictionary<AttackParticleEnum, AnimatorOverrideController>();
        particleDict[AttackParticleEnum.WATER] = Resources.Load<AnimatorOverrideController>("AttackParticles/WaterParticle");
        particleDict[AttackParticleEnum.GRASS] = Resources.Load<AnimatorOverrideController>("AttackParticles/GrassParticle");
        particleDict[AttackParticleEnum.FIRE] = Resources.Load<AnimatorOverrideController>("AttackParticles/FireParticle");
        particleDict[AttackParticleEnum.ICE] = Resources.Load<AnimatorOverrideController>("AttackParticles/IceParticle");
        particleDict[AttackParticleEnum.ELECTRIC] = Resources.Load<AnimatorOverrideController>("AttackParticles/EarthParticle");
        particleDict[AttackParticleEnum.EARTH] = Resources.Load<AnimatorOverrideController>("AttackParticles/ElectricParticle");
    }

    public static AnimatorOverrideController GetParticleController(AttackParticleEnum particle) {
        if (particleDict == null) Initialize();
        return particleDict[particle];
    }
}

public enum AttackParticleEnum {
    NONE, WATER, GRASS, FIRE, ICE, EARTH, ELECTRIC
}

public enum ParticleOriginEnum {
    ATTACKER, OVERHEAD, TARGET
}
