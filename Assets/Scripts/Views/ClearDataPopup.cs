﻿using System;
using System.Collections;
using System.IO;
using UnityEngine;

public class ClearDataPopup : MonoBehaviour {

    public int expansionFrames = 12;

    public void Awake() {
        transform.localScale = new Vector3();
    }

    public void ClearData() {
        var credentialsManager = FindObjectOfType<CredentialsManager>();
        credentialsManager.DeleteRefreshToken();
        if (File.Exists(WhaleCredentials.credentialsFile)) {
            File.Delete(WhaleCredentials.credentialsFile);
        }
        if (File.Exists(WhaleCredentials.identityFile)) {
            File.Delete(WhaleCredentials.identityFile);
        }
        if (File.Exists(StateManager.fileName)) {
            File.Delete(StateManager.fileName);
        }
        ClosePopup();
    }

    public void LaunchPopup() {
        StartCoroutine(ExpandIntoFrame());
    }

    public void ClosePopup() {
        StartCoroutine(ShrinkToNothing());
    }

    private IEnumerator ExpandIntoFrame() {
        for (float x = 1f; x <= expansionFrames; x++) {
            float percentage = x / expansionFrames;
            transform.localScale = new Vector3(percentage, percentage);
            yield return null;
        }
    }

    private IEnumerator ShrinkToNothing() {
        for (float x = expansionFrames; x >= 0; x--) {
            float percentage = x / expansionFrames;
            transform.localScale = new Vector3(percentage, percentage);
            yield return null;
        }
        Destroy(gameObject);
    }
}
