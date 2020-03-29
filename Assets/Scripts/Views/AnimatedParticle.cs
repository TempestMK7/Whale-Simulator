using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.Tempest.Whale.GameObjects;
using Com.Tempest.Whale.ResourceContainers;

public class AnimatedParticle : MonoBehaviour {

    public Animator particleAnimator;

    public int flightFrames = 15;
    public int lingerFrames = 12;

    public void SetParticleAnimation(AttackParticleEnum particleEnum) {
        var overrideController = AttackParticleContainer.GetParticleController(particleEnum);
        particleAnimator.runtimeAnimatorController = overrideController;
    }

    public void FlyToTarget(Vector3 target) {
        StartCoroutine(AnimateFlight(target));
    }

    private IEnumerator AnimateFlight(Vector3 target) {
        for (float x = 1; x <= flightFrames; x++) {
            transform.position = Vector3.Lerp(transform.position, target, x / flightFrames);
            yield return null;
        }
        for (float x = 1; x <= lingerFrames; x++) {
            yield return null;
        }
        Destroy(gameObject);
    }
}
