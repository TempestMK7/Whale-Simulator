using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPopupManager : MonoBehaviour {

    public Slider musicSlider;
    public Slider effectSlider;

    private MusicManager musicManager;

    public void Awake() {
        musicManager = FindObjectOfType<MusicManager>();
        transform.localScale = new Vector3(0f, 0f);
        musicSlider.value = SettingsManager.GetInstance().musicVolume;
        effectSlider.value = SettingsManager.GetInstance().effectVolume;
    }

    public void LaunchPopup() {
        StartCoroutine(ExpandIntoFrame());
    }

    public void OnDonePressed() {
        StartCoroutine(ShrinkToNothing());
    }

    public void OnMusicChanged() {
        float value = musicSlider.value;
        SettingsManager.GetInstance().musicVolume = value;
        SettingsManager.SaveContainer();
        musicManager.SetVolume(value);
    }

    public void OnEffectChanged() {
        float value = effectSlider.value;
        SettingsManager.GetInstance().effectVolume = value;
        SettingsManager.SaveContainer();
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
