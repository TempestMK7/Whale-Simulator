using UnityEngine;

class CandleAnimation : BaseWhaleAnimation {

    public ParticleSystem specialParticle;

    public override void Special() {
        base.Special();
        specialParticle.Play();
    }
}
