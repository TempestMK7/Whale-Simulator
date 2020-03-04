using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Coffee.UIExtensions;

public class SummonCardBehavior : MonoBehaviour, IPointerClickHandler {

    public Image glowingBorder;
    public Image heroIcon;

    public GameObject waterSummon;
    public GameObject grassSummon;
    public GameObject fireSummon;
    public GameObject iceSummon;
    public GameObject electricSummon;
    public GameObject earthSummon;

    private BaseHero summonedHero;
    private bool hasRevealed = false;

    public void Awake() {
        glowingBorder.enabled = false;
        heroIcon.enabled = false;
        waterSummon.GetComponent<ParticleSystem>().Stop();
        grassSummon.GetComponent<ParticleSystem>().Stop();
        fireSummon.GetComponent<ParticleSystem>().Stop();
        iceSummon.GetComponent<ParticleSystem>().Stop();
        electricSummon.GetComponent<ParticleSystem>().Stop();
        earthSummon.GetComponent<ParticleSystem>().Stop();
    }

    public void SetHero(BaseHero hero) {
        summonedHero = hero;
        BuildFromHero();
    }

    public void SetParticleScale(float scale) {
        foreach (UIParticle particle in this.GetComponentsInChildren<UIParticle>()) {
            particle.scale = scale;
        }
    }

    private void BuildFromHero() {
        if (summonedHero == null) return;

        heroIcon.sprite = summonedHero.HeroIcon;

        int rarity = summonedHero.Rarity;
        switch (rarity) {
            case 3:
                glowingBorder.color = new Color32(160, 96, 96, 184);
                glowingBorder.enabled = true;
                break;
            case 4:
                glowingBorder.color = new Color32(216, 216, 248, 224);
                glowingBorder.enabled = true;
                break;
            case 5:
                glowingBorder.color = new Color32(200, 232, 64, 255);
                glowingBorder.enabled = true;
                break;
            default:
                glowingBorder.enabled = false;
                break;
        }
    }

    public void OnPointerClick(PointerEventData eventData) {
        OnCardReveal();
    }

    public void OnCardReveal() {
        if (summonedHero == null) return;
        if (hasRevealed) return;
        hasRevealed = true;

        FactionEnum faction = summonedHero.Faction;
        GameObject summonEffect = waterSummon;
        switch (faction) {
            case FactionEnum.WATER:
                summonEffect = waterSummon;
                break;
            case FactionEnum.GRASS:
                summonEffect = grassSummon;
                break;
            case FactionEnum.FIRE:
                summonEffect = fireSummon;
                break;
            case FactionEnum.ICE:
                summonEffect = iceSummon;
                break;
            case FactionEnum.ELECTRIC:
                summonEffect = electricSummon;
                break;
            case FactionEnum.EARTH:
                summonEffect = earthSummon;
                break;
        }
        summonEffect.GetComponent<ParticleSystem>().Play();
        StartCoroutine("RevealHero");
    }

    public bool HasRevealed() {
        return hasRevealed;
    }

    IEnumerator RevealHero() {
        var popup = GetComponentInParent<SummonPopupBehavior>();
        if (popup != null) {
            popup.HideRevealText();
            yield return new WaitForSeconds(1f);
            heroIcon.enabled = true;
            yield return new WaitForSeconds(0.5f);
            popup.RevealDoneButton();
        }

        var tenPopup = GetComponentInParent<TenSummonPopupBehavior>();
        if (tenPopup != null) {
            tenPopup.OnReveal();
            yield return new WaitForSeconds(1f);
            heroIcon.enabled = true;
        }
    }
}
