using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelupFanfareBehavior : MonoBehaviour {

    public ParticleSystem levelupParticles;
    public AudioSource levelupSound;

    public void Awake() {
        levelupParticles.Stop();
        levelupSound.Stop();
        levelupSound.volume = SettingsManager.GetInstance().effectVolume * 0.5f;
    }

    public void Play() {
        levelupParticles.Stop();
        levelupParticles.Play();
        levelupSound.time = 0.2f;
        levelupSound.Play();
    }
}
