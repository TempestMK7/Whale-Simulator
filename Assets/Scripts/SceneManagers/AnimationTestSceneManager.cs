using Com.Tempest.Whale.GameObjects;
using System;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTestSceneManager : MonoBehaviour {

    public GameObject prefabHolder;

    private List<HarshByteAnimation> discoveredAnimators;

    public void Awake() {
        var harshAnimation = Instantiate(Resources.Load<HarshByteAnimation>(BaseHero.GetHero(HeroEnum.REZAKAHT).HarshPath), prefabHolder.transform);
        harshAnimation.OnCreate(prefabHolder.transform.localScale);

        var harshAnimations = FindObjectsOfType<HarshByteAnimation>();
        discoveredAnimators = new List<HarshByteAnimation>(harshAnimations);
        foreach (HarshByteAnimation animation in discoveredAnimators) {
            animation.OnCreate(prefabHolder.transform.localScale);
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
