using System;
using System.Collections.Generic;
using UnityEngine;

public class OracleAnimation : HarshByteAnimation {

    public ParticleSystem specialParticle;

    public override void OnCreate(Vector3 localScale) {
        base.OnCreate(localScale);
        var particles = GetComponentsInChildren<ParticleSystem>();
        Vector3 particleScale = localScale * 0.8f;
        foreach (ParticleSystem particle in particles) {
            particle.transform.localScale = particleScale;
        }
    }

    public override void Special() {
        base.Special();
        specialParticle.Play();
    }
}
