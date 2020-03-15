using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroPlaceholderBehavior : MonoBehaviour {

    public Animator animator;
    public Canvas healthCanvas;
    public Image healthBar;
    public Image energyBar;
    public CombatTextHolder combatTextHolder;

    private Action handler;
    private Action<AccountHero> heroHandler;

    private AccountHero selectedHero;
    private CombatHero combatHero;

    public void Awake() {
        healthCanvas.gameObject.SetActive(false);
    }

    public void Update() {
        if (combatHero != null) {
            var healthPercentage = combatHero.currentHealth / combatHero.health;
            var energyPercentage = combatHero.currentEnergy / 100.0;

            if (Math.Abs(healthPercentage - healthBar.fillAmount) < 0.02) {
                healthBar.fillAmount = (float)healthPercentage;
            } else {
                healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, (float)healthPercentage, 0.3f);
            }

            if (Math.Abs(energyPercentage - energyBar.fillAmount) < 0.02) {
                energyBar.fillAmount = (float)energyPercentage;
            } else {
                energyBar.fillAmount = Mathf.Lerp(energyBar.fillAmount, (float)energyPercentage, 0.3f);
            }
        }
    }

    public void SetHero(HeroEnum hero) {
        var baseHero = BaseHeroContainer.GetBaseHero(hero);
        animator.runtimeAnimatorController = baseHero.HeroAnimator;
    }

    public void SetHero(CombatHero hero) {
        handler = null;
        heroHandler = null;
        selectedHero = null;
        combatHero = new CombatHero(hero);
        animator.runtimeAnimatorController = hero.baseHero.HeroAnimator;
        healthCanvas.gameObject.SetActive(true);
    }

    public bool ContainsHero(CombatHero hero) {
        return combatHero.combatHeroGuid.Equals(hero.combatHeroGuid);
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
            heroHandler.Invoke(selectedHero);
        }
    }

    public void AnimateCombatStep(CombatStep step) {
        animator.SetTrigger("Attack");
        combatHero.currentEnergy += step.energyGained;
    }

    public void AnimateDamageInstance(DamageInstance damageInstance) {
        combatHero.currentHealth -= damageInstance.damage;
        combatHero.currentHealth += damageInstance.healing;
        combatHero.currentEnergy += damageInstance.energy;
        combatTextHolder.AnimateDamageInstance(damageInstance);

        if (damageInstance.damage > 0) {
            animator.SetTrigger("TakeDamage");
        }

        if (damageInstance.wasFatal) {
            animator.SetTrigger("Die");
            StartCoroutine(FadeOutHealthCanvas());
        }
    }

    private IEnumerator FadeOutHealthCanvas() {
        yield return new WaitForSeconds(1f);
        healthCanvas.gameObject.SetActive(false);
    }

    public void PlayDead() {
        animator.SetTrigger("Die");
    }
}
