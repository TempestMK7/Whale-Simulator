using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TooltipPopup : MonoBehaviour {

    public RectTransform popupContainer;
    public Text popupTitle;
    public Text popupContents;

    public int expansionFrames = 12;

    public void Awake() {
        popupContainer.localScale = new Vector3(0, 0);
    }

    public void SetTooltip(string title, string tooltip) {
        popupTitle.text = title;
        popupContents.text = tooltip;
        StartCoroutine(ExpandIntoFrame());
    }

    public void OnDonePressed() {
        StartCoroutine(ShrinkToNothing());
    }

    private IEnumerator ExpandIntoFrame() {
        for (float x = 1f; x <= expansionFrames; x++) {
            float percentage = x / expansionFrames;
            popupContainer.localScale = new Vector3(percentage, percentage);
            yield return null;
        }
    }

    private IEnumerator ShrinkToNothing() {
        for (float x = expansionFrames; x >= 0; x--) {
            float percentage = x / expansionFrames;
            popupContainer.localScale = new Vector3(percentage, percentage);
            yield return null;
        }
        Destroy(gameObject);
    }
}
