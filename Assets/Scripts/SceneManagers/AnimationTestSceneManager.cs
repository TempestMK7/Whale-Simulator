using System;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTestSceneManager : MonoBehaviour {

    private List<HarshByteAnimation> discoveredAnimators;

    public void Awake() {
        var animators = FindObjectsOfType<HarshByteAnimation>();
        discoveredAnimators = new List<HarshByteAnimation>(animators);
        foreach (HarshByteAnimation animator in discoveredAnimators) {
            animator.OnCreate(new Vector3(3f, 3f));
        }
    }

    public void OnAttack() {
        foreach (HarshByteAnimation animator in discoveredAnimators) {
            animator.Attack();
        }
    }

    public void OnSpecial() {
        foreach (HarshByteAnimation animator in discoveredAnimators) {
            animator.Special();
        }
    }

    public void OnHurt() {
        foreach (HarshByteAnimation animator in discoveredAnimators) {
            animator.Hurt();
        }
    }

    public void OnDeath() {
        foreach (HarshByteAnimation animator in discoveredAnimators) {
            animator.Death();
        }
    }
}
