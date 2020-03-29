using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatReportPopup : MonoBehaviour {

    public RectTransform popupHolder;
    public Text reportHolder;

    public int animationFrames = 12;

    public void Awake() {
        popupHolder.localScale = new Vector3(0f, 0f);
    }

    public void SetReport(List<string> readableReport) {
        string report = "";
        foreach (string line in readableReport) {
            report += line + "\n";
        }
        reportHolder.text = report;
        StartCoroutine(ExpandIntoFrame());
    }

    private void ResizeReportHolder() {
        Canvas.ForceUpdateCanvases();
        int lines = reportHolder.cachedTextGenerator.lineCount;
        reportHolder.rectTransform.sizeDelta = new Vector2(0, lines * 15f);
    }

    public void OnDonePressed() {
        StartCoroutine(ShrinkToNothing());
    }

    private IEnumerator ExpandIntoFrame() {
        for (float x = 1f; x <= animationFrames; x++) {
            float percentage = x / animationFrames;
            popupHolder.localScale = new Vector3(percentage, percentage);
            yield return null;
        }
        ResizeReportHolder();
    }

    private IEnumerator ShrinkToNothing() {
        for (float x = animationFrames - 1f; x >= 0; x--) {
            float percentage = x / animationFrames;
            popupHolder.localScale = new Vector3(percentage, percentage);
            yield return null;
        }
        Destroy(gameObject);
    }
}
