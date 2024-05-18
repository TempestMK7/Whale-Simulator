using UnityEngine;

public class DefaultDeathParticleAnimation : BaseWhaleAnimation {

    public Transform spriteTransform;
    public ParticleSystem deathParticle;
    public float deathParticleDelay = 0.3f;

    public override void OnCreate(Vector3 localScale) {
        base.OnCreate(localScale);
        var particles = GetComponentsInChildren<ParticleSystem>();
        Vector3 particleScale = localScale * 2f;
        foreach (ParticleSystem particle in particles) {
            particle.transform.localScale = particleScale;
        }
    }

    public override void Death() {
        base.Death();
        StartCoroutine(PlayDeathAnimation());
    }

    private System.Collections.IEnumerator PlayDeathAnimation() {
        deathParticle.Play();
        yield return new WaitForSeconds(deathParticleDelay);
        spriteTransform.localScale = new Vector3(0f, 0f, 0f);
    }
}
