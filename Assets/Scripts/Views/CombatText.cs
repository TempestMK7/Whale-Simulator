using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatText : MonoBehaviour {

    public Text display;

    public int totalFrames = 60;
    public int fontSize = 50;

    public void SetText(double amount, Color textColor) {
        display.text = CustomFormatter.Format(amount);
        display.color = textColor;
    }

    public void SetText(string text, Color textColor) {
        display.text = text;
        display.color = textColor;
    }

    public void Awake() {
        StartCoroutine(FloatUpwards());
        StartCoroutine(AnimateFontSize());
    }

    private IEnumerator FloatUpwards() {
        var transform = gameObject.transform as RectTransform;
        float frames = totalFrames;
        for (int x = 1; x < frames; x++) {
            transform.anchorMin = new Vector2(0.5f, x / frames);
            transform.anchorMax = transform.anchorMin;
            transform.anchoredPosition = new Vector2();
            yield return null;
        }
        Destroy(gameObject);
    }

    private IEnumerator AnimateFontSize() {
        int fadeDuration = totalFrames / 10;
        int fullDuration = fadeDuration * 8;

        for (int x = 1; x <= fadeDuration; x++) {
            float percentage = (float)x / fadeDuration;
            display.fontSize = (int)(percentage * fontSize);
            yield return null;
        }

        for (int x = 1; x <= fullDuration; x++) {
            yield return null;
        }

        for (int x = fadeDuration; x >= 0; x--) {
            float percentage = (float)x / fadeDuration;
            display.fontSize = (int)(percentage * fontSize);
            yield return null;
        }
    }
}
