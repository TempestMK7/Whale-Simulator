using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Com.Tempest.Whale.StateObjects;

public class TenSummonPopupBehavior : MonoBehaviour {

    public float particleScale = 60f;

    public Text revealText;
    public UnityEngine.UI.Button doneButton;
    public UnityEngine.UI.Button revealButton;

    public SummonCardBehavior[] summonCard;

    private int revealCount;

    void Awake() {
        transform.localScale = new Vector3(0f, 0f);
        revealText.enabled = true;
        doneButton.gameObject.SetActive(false);
        revealButton.gameObject.SetActive(true);
        revealCount = 0;
    }

    public void LaunchPopup(List<AccountHero> summonedHero) {
        if (summonedHero.Count != 10) {
            Destroy(gameObject);
            return;
        }
        for (int x = 0; x < 10; x++) {
            summonCard[x].SetHero(summonedHero[x].GetBaseHero(), particleScale);
        }
        StartCoroutine(ExpandIntoFrame());
    }

    public void OnReveal() {
        revealCount++;
        if (revealCount >= 10) {
            SwapUi();
        }
    }

    public void OnRevealPressed() {
        StartCoroutine(RevealAll());
        revealText.enabled = false;
        revealButton.gameObject.SetActive(false);
    }

    private void SwapUi() {
        revealText.enabled = false;
        revealButton.gameObject.SetActive(false);
        doneButton.gameObject.SetActive(true);
    }

    IEnumerator RevealAll() {
        foreach (SummonCardBehavior card in summonCard) {
            if (!card.HasRevealed()) {
                card.OnCardReveal();
                yield return new WaitForSeconds(0.2f);
            }
        }
    }

    public void OnDonePressed() {
        StartCoroutine(ShrinkToNothing());
    }

    IEnumerator ExpandIntoFrame() {
        float duration = 12f;
        for (float frame = 0f; frame <= duration; frame++) {
            float ratio = frame / duration;
            transform.localScale = new Vector3(ratio, ratio);
            yield return null;
        }
    }

    IEnumerator ShrinkToNothing() {
        float duration = 12f;
        for (float frame = duration; frame >= 0; frame--) {
            float ratio = frame / duration;
            transform.localScale = new Vector3(ratio, ratio);
            yield return null;
        }
        Destroy(gameObject);
    }
}
