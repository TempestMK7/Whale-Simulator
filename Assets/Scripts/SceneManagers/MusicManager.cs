using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

    public AudioSource backgroundMusic;
    public AudioSource battleMusic;

    public static MusicManager Instance { get; set; }

    private float volume;

    void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        Instance = this;
        SetVolume(SettingsManager.GetInstance().musicVolume);
    }

    public void PlayMusic(SongEnum music) {
        switch (music) {
            case SongEnum.HUB:
                backgroundMusic.Play();
                break;
            case SongEnum.BATTLE:
                battleMusic.Play();
                break;
        }
    }

    public void StopMusic() {
        backgroundMusic.Stop();
        battleMusic.Stop();
    }

    public void SetVolume(float volume) {
        this.volume = volume;
        backgroundMusic.volume = volume;
        battleMusic.volume = volume;
        SettingsManager.GetInstance().musicVolume = volume;
        SettingsManager.SaveContainer();
    }
}

public enum SongEnum {
    HUB, BATTLE
}
