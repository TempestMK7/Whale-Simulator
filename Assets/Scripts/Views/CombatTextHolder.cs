﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.Tempest.Whale.Combat;
using Com.Tempest.Whale.GameObjects;
using Com.Tempest.Whale.ResourceContainers;

public class CombatTextHolder : MonoBehaviour {

    public Canvas container;
    public GameObject combatTextPrefab;

    public void AnimateDamageInstance(DamageInstance instance) {
        StartCoroutine(AnimateAllComponents(instance));
    }

    public void AnimateSkippedTurn() {
        var skipText = Instantiate(combatTextPrefab, container.transform as RectTransform);
        skipText.GetComponent<CombatText>().SetText("Skipped.", ColorContainer.LightTextColor());
    }

    private IEnumerator AnimateAllComponents(DamageInstance instance) {
        if (instance.damage > 0) {
            var combatText = Instantiate(combatTextPrefab, container.transform as RectTransform);
            combatText.GetComponent<CombatText>().SetText(instance.damage, ColorContainer.EnergyColor());
            yield return new WaitForSeconds(0.3f);
        }
        if (instance.healing > 0) {
            var combatText = Instantiate(combatTextPrefab, container.transform as RectTransform);
            combatText.GetComponent<CombatText>().SetText(instance.healing, ColorContainer.HealthColor());
            yield return new WaitForSeconds(0.3f);
        }
        foreach (CombatStatus status in instance.inflictedStatus) {
            var statusDisplay = StatusInfoContainer.GetStatusInfo(status.status);
            var combatText = Instantiate(combatTextPrefab, container.transform as RectTransform);
            combatText.GetComponent<CombatText>().SetText(statusDisplay.StatusName, ColorContainer.ColorFromFaction(statusDisplay.AssociatedFaction));
            yield return new WaitForSeconds(0.3f);
        }
    }
}
