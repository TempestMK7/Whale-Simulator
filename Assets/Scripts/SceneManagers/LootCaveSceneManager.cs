using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LootCaveSceneManager : MonoBehaviour {

    public LayerMask heroAnimationLayer;

    public Text floorLabel;
    public Text levelLabel;
    public Text awakeningLabel;

    public void Awake() {
        var position = StateManager.GetLootCavePosition();
        floorLabel.text = string.Format("Loot Cave: Floor {0}", position);
        int level = position * 5;
        levelLabel.text = string.Format("Level: {0}", level);
        int awakening = (position / 4) + 1;
        awakeningLabel.text = string.Format("Awakening: {0}", awakening);
    }

    public void Update() {
        if (Input.GetMouseButtonUp(0) && !EventSystem.current.IsPointerOverGameObject()) {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out _, heroAnimationLayer)) {
                OnLaunchEncounter();
            }
        }
    }

    public void OnBackPressed() {
        SceneManager.LoadSceneAsync("HubScene");
    }

    public void OnLaunchEncounter() {
        BattleManager.SelectBattleType(BattleEnum.LOOT_CAVE);
        SceneManager.LoadSceneAsync("BattleScene");
    }
}
