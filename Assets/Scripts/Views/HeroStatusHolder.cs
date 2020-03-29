using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Com.Tempest.Whale.Combat;
using Com.Tempest.Whale.ResourceContainers;

public class HeroStatusHolder : MonoBehaviour {

    public Image blurryBorder;
    public Image heroIcon;
    public Image heroHealth;
    public Image heroEnergy;

    public int healthAnimationFrames = 20;

    private float healthWidth;
    private float energyWidth;

    private CombatHero hero;

    public void Awake() {
        healthWidth = heroHealth.rectTransform.rect.width;
        energyWidth = heroEnergy.rectTransform.rect.width;
    }

    public void SetHero(CombatHero hero) {
        this.hero = hero;
        heroIcon.sprite = Resources.Load<Sprite>(hero.baseHero.HeroIconPath);
        blurryBorder.color = ColorContainer.ColorFromFaction(hero.baseHero.Faction);
        SetBarsWithoutAnimation();
    }

    private void SetBarsWithoutAnimation() {
        float healthPercent = (float)(hero.currentHealth / hero.health);
        if (healthPercent < 0f) healthPercent = 0f;
        float energyPercent = (float)(hero.currentEnergy / 100f);
        if (energyPercent > 1f) energyPercent = 1f;
        float endingHealthWidth = healthWidth * healthPercent;
        float endingEnergyWidth = energyWidth * energyPercent;
        heroHealth.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, endingHealthWidth);
        heroEnergy.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, endingEnergyWidth);
    }

    public void Animate(CombatHero hero) {
        this.hero = hero;
        StartCoroutine(AnimateHealthBars());
    }

    public Guid GetCurrentHeroGuid() {
        return hero.combatHeroGuid;
    }

    private IEnumerator AnimateHealthBars() {
        float startingHealthWidth = heroHealth.rectTransform.rect.width;
        float startingEnergyWidth = heroEnergy.rectTransform.rect.width;

        float healthPercent = (float)(hero.currentHealth / hero.health);
        if (healthPercent < 0f) healthPercent = 0f;
        float energyPercent = (float)(hero.currentEnergy / 100f);
        if (energyPercent > 1f) energyPercent = 1f;

        float endingHealthWidth = healthWidth * healthPercent;
        float endingEnergyWidth = energyWidth * energyPercent;

        for (int x = 1; x <= healthAnimationFrames; x++) {
            float percentage = (float)x / healthAnimationFrames;
            heroHealth.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Mathf.Lerp(startingHealthWidth, endingHealthWidth, percentage));
            heroEnergy.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Mathf.Lerp(startingEnergyWidth, endingEnergyWidth, percentage));
            yield return null;
        }
    }
}
