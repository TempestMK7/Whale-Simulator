using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingPopup : MonoBehaviour {

    public Text titleLabel;
    public Text messageLabel;

    public int animationFrames = 12;

    public void Awake() {
        gameObject.transform.localScale = new Vector3();
    }

    public void LaunchPopup(string title, string message, bool animate = true) {
        titleLabel.text = title;
        messageLabel.text = message;
        if (animate) StartCoroutine(ExpandIntoFrame());
        else gameObject.transform.localScale = new Vector3(1f, 1f);
    }

    private IEnumerator ExpandIntoFrame() {
        for (float x = 1; x <= animationFrames; x++) {
            float percentage = x / animationFrames;
            gameObject.transform.localScale = new Vector3(percentage, percentage);
            yield return null;
        }
        gameObject.transform.localScale = new Vector3(1f, 1f);
    }

    public void DismissPopup() {
        StartCoroutine(ShrinkToNothing());
    }

    private IEnumerator ShrinkToNothing() {
        for (float x = animationFrames - 1; x >= 0; x--) {
            float percentage = x / animationFrames;
            gameObject.transform.localScale = new Vector3(percentage, percentage);
            yield return null;
        }
        Destroy(gameObject);
    }

    public void SetText(string title, string message) {
        titleLabel.text = title;
        messageLabel.text = message;
    }
}
