using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Coffee.UIExtensions;

public class SummonCardBehavior : MonoBehaviour, IPointerClickHandler {

    public Image glowingBorder;
    public Image heroIcon;

    public GameObject waterSummonPrefab;
    public GameObject grassSummonPrefab;
    public GameObject fireSummonPrefab;
    public GameObject iceSummonPrefab;
    public GameObject electricSummonPrefab;
    public GameObject earthSummonPrefab;

    public AudioSource summonSound;

    private BaseHero summonedHero;
    private bool hasRevealed = false;
    private ParticleSystem summoningParticle;
    private float particleScale;

    public void Awake() {
        glowingBorder.enabled = false;
        heroIcon.enabled = false;
        summonSound.volume = SettingsManager.GetInstance().effectVolume * 0.5f;
    }

    public void SetHero(BaseHero hero, float scale) {
        summonedHero = hero;
        particleScale = scale;
        BuildFromHero();
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

        switch (summonedHero.Faction) {
            case FactionEnum.WATER:
                summoningParticle = Instantiate(waterSummonPrefab, transform).GetComponent<ParticleSystem>();
                break;
            case FactionEnum.GRASS:
                summoningParticle = Instantiate(grassSummonPrefab, transform).GetComponent<ParticleSystem>();
                break;
            case FactionEnum.FIRE:
                summoningParticle = Instantiate(fireSummonPrefab, transform).GetComponent<ParticleSystem>();
                break;
            case FactionEnum.ICE:
                summoningParticle = Instantiate(iceSummonPrefab, transform).GetComponent<ParticleSystem>();
                break;
            case FactionEnum.ELECTRIC:
                summoningParticle = Instantiate(electricSummonPrefab, transform).GetComponent<ParticleSystem>();
                break;
            case FactionEnum.EARTH:
                summoningParticle = Instantiate(earthSummonPrefab, transform).GetComponent<ParticleSystem>();
                break;
        }

        foreach (UIParticle particle in summoningParticle.GetComponentsInChildren<UIParticle>()) {
            particle.scale = particleScale;
        }
        summoningParticle.Stop();
    }

    public void OnPointerClick(PointerEventData eventData) {
        OnCardReveal();
    }

    public void OnCardReveal() {
        if (summonedHero == null) return;
        if (hasRevealed) return;
        hasRevealed = true;

        // We offset the sound effect time to make it line up with the particle's pop effect.
        summonSound.time = 0.2f;
        summonSound.Play();
        summoningParticle.Play();
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
