using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheaterPopupBehavior : MonoBehaviour {

    public void Awake() {
        transform.localScale = new Vector3();
    }

    public void CheatIdleCurrency() {
        StateManager.CheatIdleCurrency(1000 * 60 * 60 * 8);
    }

    public void CheatSummons() {
        StateManager.CheatSummons(100);
    }

    public void LaunchPopup() {
        StartCoroutine(ExpandIntoFrame());
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
