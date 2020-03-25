﻿using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitializationSceneManager : MonoBehaviour {

    void Start() {
        Application.targetFrameRate = 60;
        BaseHeroContainer.Initialize();
        BaseEquipmentContainer.Initialize();
        FactionContainer.Initialize();
        RoleContainer.Initialize();
        AttackInfoContainer.Initialize();
        AbilityInfoContainer.Initialize();
        AttackParticleContainer.Initialize();
        RewardInfoContainer.Initialize();
        StateManager.GetCurrentState();
        SettingsManager.GetInstance();
        SceneManager.LoadSceneAsync("HubScene");
    }
}
