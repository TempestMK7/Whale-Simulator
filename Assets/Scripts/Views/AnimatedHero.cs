using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Com.Tempest.Whale.Combat;
using Com.Tempest.Whale.GameObjects;
using Com.Tempest.Whale.ResourceContainers;
using Com.Tempest.Whale.StateObjects;
using Spine;
using Spine.Unity;

public class AnimatedHero : MonoBehaviour {

    public Animator animator;
    public SpriteRenderer spriteRenderer;
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
    private SkeletonAnimation spineAnimation;

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
        if (spineAnimation != null) {
            Destroy(spineAnimation.gameObject);
            spineAnimation = null;
        }
        var baseHero = BaseHeroContainer.GetBaseHero(hero);
        if (baseHero.SpinePath != null) {
            spriteRenderer.enabled = false;
            spineAnimation = Instantiate(Resources.Load<GameObject>(baseHero.SpinePath), gameObject.transform).GetComponent<SkeletonAnimation>();
        } else {
            spriteRenderer.enabled = true;
            animator.runtimeAnimatorController = Resources.Load<AnimatorOverrideController>(baseHero.SpritePath);
        }

        SetAnimation("Idle", true);
    }

    public void SetHero(CombatHero hero) {
        if (hero.baseHero.SpinePath != null) {
            if (combatHero == null || combatHero.baseHero.Hero != hero.baseHero.Hero) {
                if (spineAnimation != null) {
                    Destroy(spineAnimation.gameObject);
                    spineAnimation = null;
                }
                spriteRenderer.enabled = false;
                spineAnimation = Instantiate(Resources.Load<GameObject>(hero.baseHero.SpinePath), gameObject.transform).GetComponent<SkeletonAnimation>();
            }
        } else {
            if (spineAnimation != null) {
                Destroy(spineAnimation.gameObject);
                spineAnimation = null;
            }
            spriteRenderer.enabled = true;
            animator.runtimeAnimatorController = Resources.Load<AnimatorOverrideController>(hero.baseHero.SpritePath);
        }

        handler = null;
        heroHandler = null;
        selectedHero = null;
        combatHero = new CombatHero(hero);

        if (hero.IsAlive()) {
            healthCanvas.gameObject.SetActive(true);
        } else {
            healthCanvas.gameObject.SetActive(false);
            animator.SetTrigger("Die");
            SetAnimation("Death", false);
        }
    }

    public void SetAnimation(string state, bool loop) {
        if (spineAnimation == null || spineAnimation.state == null) return;
        if (state.Equals("Death") && spineAnimation.state.GetCurrent(0).Animation.Name.Equals("Death")) return;
        spineAnimation.state.SetAnimation(0, state, loop);
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
        transform.position = destination;
        combatHero.currentEnergy += turn.energyGained;
        SendDamageInstances(turn.steps, placeholders);

        if (spineAnimation != null) {
            soundEffect.Play();
            var animationName = attackInfo.IsSpecial ? "UltimateAttack" : "Attack";
            var trackEntry = spineAnimation.state.SetAnimation(0, animationName, false);
            trackEntry.TimeScale = 2;
            yield return new WaitForSpineAnimationComplete(trackEntry, true);
            spineAnimation.state.SetAnimation(0, "Idle", true);
        } else {
            animator.SetTrigger("Attack");
            yield return new WaitForSeconds(attackDurationSeconds);
        }

        for (float x = 1; x <= slideDurationFrames; x++) {
            float percentage = x / slideDurationFrames;
            transform.position = Vector3.Lerp(transform.position, startingPosition, percentage);
            yield return null;
        }
        transform.position = startingPosition;
    }

    private IEnumerator AnimateOtherAttacks(CombatTurn turn, Dictionary<Guid, AnimatedHero> placeholders) {
        var attackInfo = AttackInfoContainer.GetAttackInfo(turn.attackUsed);
        soundEffect.clip = Resources.Load<AudioClip>(attackInfo.AttackSoundPath);
        soundEffect.volume = SettingsManager.GetInstance().effectVolume * 0.5f;
        combatHero.currentEnergy += turn.energyGained;

        if (spineAnimation != null) {
            soundEffect.Play();
            var animationName = attackInfo.IsSpecial ? "UltimateAttack" : "Attack";
            var trackEntry = spineAnimation.state.SetAnimation(0, animationName, false);
            trackEntry.TimeScale = 2;

            yield return new WaitForSeconds(0.4f);
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

            yield return new WaitForSeconds(0.2f);
            SendDamageInstances(turn.steps, placeholders);

            var remainingTime = trackEntry.AnimationEnd - trackEntry.AnimationTime;
            if (remainingTime > 0) yield return new WaitForSeconds(remainingTime);
            spineAnimation.state.SetAnimation(0, "Idle", true);
        } else {
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

            soundEffect.Play();
            animator.SetTrigger("Attack");
            yield return new WaitForSeconds(0.2f);
            SendDamageInstances(turn.steps, placeholders);
            yield return new WaitForSeconds(attackDurationSeconds - 0.2f);
            yield return new WaitForSeconds(slideDurationFrames * 2f / 60f);
        }
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
            StartCoroutine(TakeDamage());
        }

        if (step.wasFatal) {
            PlayDead();
            StartCoroutine(FadeOutHealthCanvas());
        }
    }

    private IEnumerator TakeDamage() {
        if (spineAnimation != null) {
            yield return new WaitForSpineAnimationComplete(spineAnimation.state.SetAnimation(0, "Hurt", false));
            spineAnimation.state.SetAnimation(0, "Idle", true);
        } else {
            animator.SetTrigger("TakeDamage");
            yield return new WaitForSeconds(0.3f);
        }
    }

    private IEnumerator FadeOutHealthCanvas() {
        yield return new WaitForSeconds(1f);
        healthCanvas.gameObject.SetActive(false);
    }

    public void PlayDead() {
        animator.SetTrigger("Die");
        SetAnimation("Death", false);
    }
}
