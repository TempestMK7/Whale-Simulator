using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatTextHolder : MonoBehaviour {

    public Canvas container;
    public GameObject combatTextPrefab;

    public void AnimateDamage(double damage) {
        var combatText = Instantiate(combatTextPrefab, container.transform as RectTransform);
        combatText.GetComponent<CombatText>().SetText(damage, ColorContainer.EnergyColor());
    }

    public void AnimateHealing(double healing) {
        var combatText = Instantiate(combatTextPrefab, container.transform as RectTransform);
        combatText.GetComponent<CombatText>().SetText(healing, ColorContainer.HealthColor());
    }
}
