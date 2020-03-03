using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HubBuilding : MonoBehaviour {

    public string targetScene;

    public void OnClickEvent() {
        SceneManager.LoadScene(targetScene);
    }
}
