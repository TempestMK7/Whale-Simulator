using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroPlaceholderBehavior : MonoBehaviour {

    public Animator animator;

    private Action handler;
    private Action<AccountHero> heroHandler;

    private AccountHero selectedHero;

    public void SetHero(HeroEnum hero) {
        var baseHero = BaseHeroContainer.GetBaseHero(hero);
        animator.runtimeAnimatorController = baseHero.HeroAnimator;
    }

    public void RegisterOnClick(Action handler) {
        this.handler = handler;
    }

    public void RegisterOnClick(Action<AccountHero> handler, AccountHero selectedHero) {
        heroHandler = handler;
        this.selectedHero = selectedHero;
    }

    public void OnClick() {
        if (handler != null) {
            handler.Invoke();
        }
        if (heroHandler != null && selectedHero != null) {
            Debug.Log("Called invoker.");
            heroHandler.Invoke(selectedHero);
        }
    }
}
