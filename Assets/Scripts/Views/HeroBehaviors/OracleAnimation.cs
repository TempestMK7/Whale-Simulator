using System;
using System.Collections.Generic;
using UnityEngine;

public class OracleAnimation : HarshByteAnimation {

    public ParticleSystem specialParticle;

    public override void Special() {
        base.Special();
        specialParticle.Play();
    }
}
