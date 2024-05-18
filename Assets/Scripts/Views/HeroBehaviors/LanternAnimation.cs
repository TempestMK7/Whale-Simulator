using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternAnimation : BaseWhaleAnimation {

    public ParticleSystem specialParticle;

    public override void OnCreate(Vector3 localScale) {
        base.OnCreate(localScale);
        Vector3 particleScale = localScale * 0.6f;
        specialParticle.transform.localScale = particleScale;
    }

    public override void Special() {
        base.Special();
        specialParticle.Play();
    }
}
