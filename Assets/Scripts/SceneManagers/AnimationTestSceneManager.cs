using Com.Tempest.Whale.GameObjects;
using System;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTestSceneManager : MonoBehaviour {

    public GameObject prefabHolder;

    private List<BaseWhaleAnimation> discoveredAnimators;

    public void Awake() {
        var harshAnimation = Instantiate(Resources.Load<BaseWhaleAnimation>(BaseHero.GetHero(HeroEnum.PULVERIZER).HarshPath), prefabHolder.transform);
        harshAnimation.OnCreate(prefabHolder.transform.localScale);

        var harshAnimations = FindObjectsOfType<BaseWhaleAnimation>();
        discoveredAnimators = new List<BaseWhaleAnimation>(harshAnimations);
        foreach (BaseWhaleAnimation animation in discoveredAnimators) {
            animation.OnCreate(prefabHolder.transform.localScale);
        }
    }

    public void OnAttack() {
        foreach (BaseWhaleAnimation animator in discoveredAnimators) {
            animator.Attack();
        }
    }

    public void OnSpecial() {
        foreach (BaseWhaleAnimation animator in discoveredAnimators) {
            animator.Special();
        }
    }

    public void OnHurt() {
        foreach (BaseWhaleAnimation animator in discoveredAnimators) {
            animator.Hurt();
        }
    }

    public void OnDeath() {
        foreach (BaseWhaleAnimation animator in discoveredAnimators) {
            animator.Death();
        }
    }
}
