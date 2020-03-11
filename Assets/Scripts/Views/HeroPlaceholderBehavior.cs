using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroPlaceholderBehavior : MonoBehaviour {

    public Animator animator;

    private Action handler;

    public void SetHero(HeroEnum hero) {
        var baseHero = BaseHeroContainer.GetBaseHero(hero);
        animator.runtimeAnimatorController = baseHero.HeroAnimator;
    }

    public void RegisterOnClick(Action handler) {
        this.handler = handler;
    }

    public void OnClick() {
        if (handler != null) {
            handler.Invoke();
        }
    }
}
