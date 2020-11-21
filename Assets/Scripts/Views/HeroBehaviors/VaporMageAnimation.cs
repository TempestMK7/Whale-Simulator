using UnityEngine;

class VaporMageAnimation : HarshByteAnimation {

    public ParticleSystem specialParticle;

    public override void Special() {
        base.Special();
        specialParticle.Play();
    }
}
