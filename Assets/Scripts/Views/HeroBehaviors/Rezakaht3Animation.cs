using UnityEngine;

public class Rezakaht3Animation : HarshByteAnimation {

    public Transform spriteTransform;
    public ParticleSystem deathParticle;

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
        yield return new WaitForSeconds(0.3f);
        spriteTransform.localScale = new Vector3(0f, 0f, 0f);
    }
}
