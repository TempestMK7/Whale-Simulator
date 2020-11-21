using UnityEngine;

class CandleAnimation : HarshByteAnimation {

    public ParticleSystem specialParticle;

    public override void Special() {
        base.Special();
        specialParticle.Play();
    }
}
