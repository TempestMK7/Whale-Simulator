using UnityEngine;

class VaporMageAnimation : HarshByteAnimation {

    public ParticleSystem specialParticle;

    public override void OnCreate(Vector3 localScale) {
        // Ignored callback.
    }

    public override void Special() {
        base.Special();
        specialParticle.Play();
    }
}
