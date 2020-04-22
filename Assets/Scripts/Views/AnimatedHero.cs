using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Com.Tempest.Whale.Combat;
using Com.Tempest.Whale.GameObjects;
using Com.Tempest.Whale.StateObjects;

public class AnimatedHero : MonoBehaviour {

    private const int MELEE_SLIDE_FRAMES = 12;
    private const float ATTACK_WINDUP_SECONDS = 0.5f;
    private const float ATTACK_SWING_SECONDS = 0.5f;
    private const float PARTICLE_TRAVEL_TIME_SECONDS = 0.2f;

    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public AudioSource soundEffect;
    public Canvas healthCanvas;
    public Image healthBar;
    public Image energyBar;
    public CombatTextHolder combatTextHolder;

    public GameObject particlePrefab;

    private Action handler;
    private Action<AccountHero> heroHandler;

    private AccountHero selectedHero;
    private CombatHero combatHero;
    private HarshByteAnimation harshByteAnimation;

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
        if (harshByteAnimation != null) {
            Destroy(harshByteAnimation.gameObject);
            harshByteAnimation = null;
        }

        var baseHero = BaseHeroContainer.GetBaseHero(hero);
        if (baseHero.HarshPath != null) {
            spriteRenderer.enabled = false;
            harshByteAnimation = Instantiate(Resources.Load<HarshByteAnimation>(baseHero.HarshPath), gameObject.transform);
            harshByteAnimation.OnCreate(gameObject.transform.localScale);
        } else {
            spriteRenderer.enabled = true;
            animator.runtimeAnimatorController = Resources.Load<AnimatorOverrideController>(baseHero.AnimatorPath);
        }
    }

    public void SetHero(CombatHero hero) {
        if (combatHero == null || combatHero.baseHero.Hero != hero.baseHero.Hero) {
            if (harshByteAnimation != null) {
                Destroy(harshByteAnimation.gameObject);
                harshByteAnimation = null;
            }

            if (hero.baseHero.HarshPath != null) {
                spriteRenderer.enabled = false;
                harshByteAnimation = Instantiate(Resources.Load<HarshByteAnimation>(hero.baseHero.HarshPath), gameObject.transform);
                harshByteAnimation.OnCreate(gameObject.transform.localScale);
            } else {
                spriteRenderer.enabled = true;
                animator.runtimeAnimatorController = Resources.Load<AnimatorOverrideController>(hero.baseHero.AnimatorPath);
            }
        }

        handler = null;
        heroHandler = null;
        selectedHero = null;
        combatHero = new CombatHero(hero);

        if (hero.IsAlive()) {
            healthCanvas.gameObject.SetActive(true);
        } else {
            healthCanvas.gameObject.SetActive(false);
            Death();
        }
    }

    public void Attack() {
        if (harshByteAnimation != null) {
            harshByteAnimation.Attack();
        } else {
            animator.SetTrigger("Attack");
        }
    }

    public void Special() {
        if (harshByteAnimation != null) {
            harshByteAnimation.Special();
        } else {
            animator.SetTrigger("Special");
        }
    }

    public void Hurt() {
        if (harshByteAnimation != null) {
            harshByteAnimation.Hurt();
        } else {
            animator.SetTrigger("Hurt");
        }
    }

    public void Death() {
        if (harshByteAnimation != null) {
            harshByteAnimation.Death();
        } else {
            animator.SetTrigger("Death");
        }
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

    public IEnumerator AnimateCombatTurn(CombatTurn turn, Dictionary<Guid, AnimatedHero> placeholders) {
        var attackInfo = AttackInfoContainer.GetAttackInfo(turn.attackUsed);
        if (turn.skippedTurn) {
            combatTextHolder.AnimateSkippedTurn();
            yield return new WaitForSeconds(0.3f);
        } else if (attackInfo.IsMelee && turn.enemyTargets.Count == 1) {
            yield return StartCoroutine(AnimateMeleeAttack(turn, placeholders));
        } else {
            yield return StartCoroutine(AnimateOtherAttacks(turn, placeholders));
        }
    }

    private IEnumerator AnimateMeleeAttack(CombatTurn turn, Dictionary<Guid, AnimatedHero> placeholders) {
        var attackInfo = AttackInfoContainer.GetAttackInfo(turn.attackUsed);
        soundEffect.clip = Resources.Load<AudioClip>(attackInfo.AttackSoundPath);
        soundEffect.volume = SettingsManager.GetInstance().effectVolume * 0.5f;
        var target = placeholders[turn.enemyTargets[0].combatHeroGuid];

        // Each swing has a half second windup, so start the swing before we begin moving.
        if (attackInfo.IsSpecial) {
            Special();
        } else {
            Attack();
        }
        var swingStart = Time.time;

        // All of this moves us to the target's position for the attack animation.
        var destination = new Vector3(target.transform.position.x, target.transform.position.y);
        var destinationOnRight = target.transform.localScale.x < 0;
        if (destinationOnRight) destination.x -= 2;
        else destination.x += 2;
        for (float x = 1; x <= MELEE_SLIDE_FRAMES; x++) {
            float percentage = x / MELEE_SLIDE_FRAMES;
            transform.position = Vector3.Lerp(transform.position, destination, percentage);
            yield return null;
        }
        transform.position = destination;

        // Wait for the rest of the windup time to elapse.
        var slideTime = Time.time - swingStart;
        var windupTimeRemaining = ATTACK_WINDUP_SECONDS - slideTime;
        if (windupTimeRemaining > 0) {
            yield return new WaitForSeconds(windupTimeRemaining);
        }

        // Play attack effects now the the actual swing has started.
        soundEffect.Play();
        combatHero.currentEnergy += turn.energyGained;
        SendDamageInstances(turn.steps, placeholders);
        yield return new WaitForSeconds(ATTACK_SWING_SECONDS);

        // Return to starting position.
        for (float x = 1; x <= MELEE_SLIDE_FRAMES; x++) {
            float percentage = x / MELEE_SLIDE_FRAMES;
            transform.position = Vector3.Lerp(transform.position, startingPosition, percentage);
            yield return null;
        }
        transform.position = startingPosition;
    }

    private IEnumerator AnimateOtherAttacks(CombatTurn turn, Dictionary<Guid, AnimatedHero> placeholders) {
        var attackInfo = AttackInfoContainer.GetAttackInfo(turn.attackUsed);
        soundEffect.clip = Resources.Load<AudioClip>(attackInfo.AttackSoundPath);
        soundEffect.volume = SettingsManager.GetInstance().effectVolume * 0.5f;

        if (attackInfo.IsSpecial) {
            Special();
        } else {
            Attack();
        }
        yield return new WaitForSeconds(ATTACK_WINDUP_SECONDS);

        combatHero.currentEnergy += turn.energyGained;
        soundEffect.Play();
        if (attackInfo.EnemyParticle != null) {
            foreach (CombatHero enemy in turn.enemyTargets) {
                var destination = placeholders[enemy.combatHeroGuid].transform.position;
                destination.y += 0.5f;
                FireParticle(attackInfo.EnemyParticle.GetValueOrDefault(), attackInfo.EnemyParticleOrigin.GetValueOrDefault(), destination);
            }
        }
        if (attackInfo.AllyParticle != null) {
            foreach (CombatHero ally in turn.allyTargets) {
                var destination = placeholders[ally.combatHeroGuid].transform.position;
                destination.y += 0.5f;
                FireParticle(attackInfo.AllyParticle.GetValueOrDefault(), attackInfo.AllyParticleOrigin.GetValueOrDefault(), destination);
            }
        }
        yield return new WaitForSeconds(PARTICLE_TRAVEL_TIME_SECONDS);
        SendDamageInstances(turn.steps, placeholders);

        yield return new WaitForSeconds(ATTACK_SWING_SECONDS - PARTICLE_TRAVEL_TIME_SECONDS);
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

    private void SendDamageInstances(List<CombatStep> steps, Dictionary<Guid, AnimatedHero> placeholders) {
        bool anyCriticals = false;
        foreach (CombatStep step in steps) {
            placeholders[step.targetGuid].AnimateCombatStep(step);
            if (step.hitType == HitType.CRITICAL) anyCriticals = true;
        }
        if (anyCriticals) {
            // Screen shake.
        }
    }

    public void AnimateCombatStep(CombatStep step) {
        combatHero.currentHealth -= step.damage;
        combatHero.currentHealth += step.healing;
        combatHero.currentEnergy += step.targetEnergy;
        combatTextHolder.AnimateCombatStep(step);

        // Play particles associated with attack type.

        if (step.damage > 0 && !step.wasFatal) {
            Hurt();
        }

        if (step.wasFatal) {
            Death();
            StartCoroutine(FadeOutHealthCanvas());
        }
    }

    private IEnumerator FadeOutHealthCanvas() {
        yield return new WaitForSeconds(1f);
        healthCanvas.gameObject.SetActive(false);
    }
}
