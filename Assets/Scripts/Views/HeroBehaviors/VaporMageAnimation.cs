using UnityEngine;

class VaporMageAnimation : BaseWhaleAnimation {

    public ParticleSystem specialParticle;

    public override void Special() {
        base.Special();
        specialParticle.Play();
    }
}
