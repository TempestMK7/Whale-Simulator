using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SummonPopupBehavior : MonoBehaviour {

    public float particleScale = 120f;

    public Text revealText;
    public UnityEngine.UI.Button doneButton;
    public SummonCardBehavior summonCard;

    public void Awake() {
        transform.localScale = new Vector3(0f, 0f);
        revealText.enabled = true;
        doneButton.gameObject.SetActive(false);
    }

    public void LaunchPopup(AccountHero summonedHero) {
        summonCard.SetHero(summonedHero.GetBaseHero(), particleScale);
        StartCoroutine("ExpandIntoFrame");
    }

    public void HideRevealText() {
        revealText.enabled = false;
    }

    public void RevealDoneButton() {
        doneButton.gameObject.SetActive(true);
    }

    public void OnDonePressed() {
        StartCoroutine("ShrinkToNothing");
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
