using System;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTestSceneManager : MonoBehaviour {

    private List<Animator> discoveredAnimators;

    public void Awake() {
        var animators = FindObjectsOfType<Animator>();
        discoveredAnimators = new List<Animator>(animators);
    }

    public void OnAttack() {
        foreach (Animator animator in discoveredAnimators) {
            animator.SetTrigger("Attack");
        }
    }

    public void OnSpecial() {
        foreach (Animator animator in discoveredAnimators) {
            animator.SetTrigger("Special");
        }
    }

    public void OnHurt() {
        foreach (Animator animator in discoveredAnimators) {
            animator.SetTrigger("Hurt");
        }
    }

    public void OnDeath() {
        foreach (Animator animator in discoveredAnimators) {
            animator.SetTrigger("Death");
        }
    }
}
