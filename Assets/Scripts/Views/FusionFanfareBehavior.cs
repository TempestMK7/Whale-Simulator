using System.Collections;
using UnityEngine;

public class FusionFanfareBehavior : MonoBehaviour {

    public ParticleSystem particles;
    public AudioSource firstSound;
    public AudioSource secondSound;

    public float secondSoundDelay = 1.6f;

    public void Awake() {
        particles.Stop();
        firstSound.Stop();
        secondSound.Stop();
        firstSound.volume = SettingsManager.GetInstance().effectVolume * 0.8f;
        secondSound.volume = SettingsManager.GetInstance().effectVolume * 0.8f;
    }

    public void Play() {
        StartCoroutine(DoEverything());
    }

    IEnumerator DoEverything() {
        particles.Play();
        firstSound.time = 0.2f;
        firstSound.Play();
        yield return new WaitForSeconds(secondSoundDelay);
        secondSound.Play();
    }
}
