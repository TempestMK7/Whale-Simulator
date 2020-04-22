using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Com.Tempest.Whale.Combat;
using Com.Tempest.Whale.GameObjects;

public class GraphEntryView : MonoBehaviour {

    public Image heroIcon;
    public Image damageFilling;
    public Image healingFilling;
    public Text damageText;
    public Text healingText;

    public void SetInformation(CombatHero hero, double damage, double healing, double max) {
        var baseHero = hero.baseHero;
        heroIcon.sprite = Resources.Load<Sprite>(baseHero.HeroIconPath);
        damageFilling.fillAmount = (float)(damage / max);
        healingFilling.fillAmount = (float)(healing / max);
        damageText.text = CustomFormatter.Format(damage);
        healingText.text = CustomFormatter.Format(healing);
    }
}
