using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SummonPopupBehavior : MonoBehaviour {

    public Text revealText;
    public UnityEngine.UI.Button doneButton;
    public SummonCardBehavior summonCard;

    public void Awake() {
        transform.localScale = new Vector3(0f, 0f);
        revealText.enabled = true;
        doneButton.gameObject.SetActive(false);
    }

    public void LaunchPopup(AccountHero summonedHero) {
        summonCard.SetHero(summonedHero.GetBaseHero());
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
        for (int frame = 0; frame < 60; frame++) {
            float ratio = frame / 60f;
            transform.localScale = new Vector3(ratio, ratio);
            yield return null;
        }
    }

    IEnumerator ShrinkToNothing() {
        for (int frame = 60; frame >= 0; frame--) {
            float ratio = frame / 60f;
            transform.localScale = new Vector3(ratio, ratio);
            yield return null;
        }
        Destroy(gameObject);
        yield return null;
    }
}
