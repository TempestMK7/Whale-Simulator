using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimatedHero : MonoBehaviour {

    public Animator animator;
    public AudioSource soundEffect;
    public Canvas healthCanvas;
    public Image healthBar;
    public Image energyBar;
    public CombatTextHolder combatTextHolder;

    public GameObject particlePrefab;

    public int slideDurationFrames = 20;
    public float attackDurationSeconds = 0.5f;

    private Action handler;
    private Action<AccountHero> heroHandler;

    private AccountHero selectedHero;
    private CombatHero combatHero;

    private Vector3 startingPosition;

    public void Awake() {
        healthCanvas.gameObject.SetActive(false);
        startingPosition = transform.position;
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
        if (hero.IsAlive()) healthCanvas.gameObject.SetActive(true);
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

    public IEnumerator AnimateCombatStep(CombatStep step, Dictionary<Guid, AnimatedHero> placeholders) {
        var attackInfo = AttackInfoContainer.GetAttackInfo(step.attackUsed);
        if (attackInfo.IsMelee && step.enemyTargets.Count == 1) {
            yield return StartCoroutine(AnimateMeleeAttack(step, placeholders));
        } else {
            yield return StartCoroutine(AnimateOtherAttacks(step, placeholders));
        }
    }

    private IEnumerator AnimateMeleeAttack(CombatStep step, Dictionary<Guid, AnimatedHero> placeholders) {
        var attackInfo = AttackInfoContainer.GetAttackInfo(step.attackUsed);
        soundEffect.clip = attackInfo.AttackSound;
        soundEffect.volume = SettingsManager.GetInstance().effectVolume * 0.5f;
        var target = placeholders[step.enemyTargets[0].combatHeroGuid];

        // All of this moves us to the target's position for the attack animation.
        var destination = new Vector3(target.transform.position.x, target.transform.position.y);
        var destinationOnRight = target.transform.localScale.x < 0;
        if (destinationOnRight) destination.x -= 2;
        else destination.x += 2;
        for (float x = 1; x <= slideDurationFrames; x++) {
            float percentage = x / slideDurationFrames;
            transform.position = Vector3.Lerp(transform.position, destination, percentage);
            yield return null;
        }

        animator.SetTrigger("Attack");
        transform.position = destination;
        soundEffect.Play();
        combatHero.currentEnergy += step.energyGained;
        SendDamageInstances(step.damageInstances, placeholders);
        yield return new WaitForSeconds(attackDurationSeconds);

        for (float x = 1; x <= slideDurationFrames; x++) {
            float percentage = x / slideDurationFrames;
            transform.position = Vector3.Lerp(transform.position, startingPosition, percentage);
            yield return null;
        }
        transform.position = startingPosition;
    }

    private IEnumerator AnimateOtherAttacks(CombatStep step, Dictionary<Guid, AnimatedHero> placeholders) {
        var attackInfo = AttackInfoContainer.GetAttackInfo(step.attackUsed);

        animator.SetTrigger("Attack");
        soundEffect.clip = attackInfo.AttackSound;
        soundEffect.volume = SettingsManager.GetInstance().effectVolume * 0.5f;
        soundEffect.Play();
        yield return new WaitForSeconds(0.3f);

        if (attackInfo.EnemyParticle != null) {
            foreach (CombatHero enemy in step.enemyTargets) {
                var destination = placeholders[enemy.combatHeroGuid].transform.position;
                destination.y += 0.5f;
                FireParticle(attackInfo.EnemyParticle.GetValueOrDefault(), attackInfo.EnemyParticleOrigin.GetValueOrDefault(), destination);
            }
        }
        if (attackInfo.AllyParticle != null) {
            foreach (CombatHero ally in step.allyTargets) {
                var destination = placeholders[ally.combatHeroGuid].transform.position;
                destination.y += 0.5f;
                FireParticle(attackInfo.AllyParticle.GetValueOrDefault(), attackInfo.AllyParticleOrigin.GetValueOrDefault(), destination);
            }
        }

        combatHero.currentEnergy += step.energyGained;
        SendDamageInstances(step.damageInstances, placeholders);
        yield return new WaitForSeconds(attackDurationSeconds);
    }

    private void FireParticle(AttackParticleEnum particle, ParticleOriginEnum origin, Vector3 target) {
        Vector3 originPoint;
        switch (origin) {
            case ParticleOriginEnum.OVERHEAD:
                originPoint = new Vector3(0, 14, 0);
                break;
            case ParticleOriginEnum.TARGET:
                originPoint = target;
                break;
            case ParticleOriginEnum.ATTACKER:
            default:
                originPoint = new Vector3(transform.position.x, transform.position.y + 0.5f);
                break;
        }
        var animatedParticle = Instantiate(particlePrefab, originPoint, Quaternion.identity).GetComponent<AnimatedParticle>();
        animatedParticle.SetParticleAnimation(particle);
        animatedParticle.FlyToTarget(target);
    }

    private void SendDamageInstances(List<DamageInstance> instances, Dictionary<Guid, AnimatedHero> placeholders) {
        bool anyCriticals = false;
        foreach (DamageInstance damageInstance in instances) {
            placeholders[damageInstance.targetGuid].AnimateDamageInstance(damageInstance);
            if (damageInstance.hitType == HitType.CRITICAL) anyCriticals = true;
        }
        if (anyCriticals) {
            // Screen shake.
        }
    }

    public void AnimateDamageInstance(DamageInstance damageInstance) {
        combatHero.currentHealth -= damageInstance.damage;
        combatHero.currentHealth += damageInstance.healing;
        combatHero.currentEnergy += damageInstance.targetEnergy;
        combatTextHolder.AnimateDamageInstance(damageInstance);

        // Play particles associated with attack type.

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
