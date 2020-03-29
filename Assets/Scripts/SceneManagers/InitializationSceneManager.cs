using UnityEngine;
using UnityEngine.SceneManagement;
using Com.Tempest.Whale.GameObjects;
using Com.Tempest.Whale.ResourceContainers;

public class InitializationSceneManager : MonoBehaviour {

    void Start() {
        Application.targetFrameRate = 60;

        BaseHeroContainer.Initialize();
        BaseEquipmentContainer.Initialize();
        AttackInfoContainer.Initialize();
        AbilityInfoContainer.Initialize();

        FactionIconContainer.Initialize();
        RoleIconContainer.Initialize();
        AttackParticleContainer.Initialize();
        RewardIconContainer.Initialize();

        StateManager.GetCurrentState();
        SettingsManager.GetInstance();
        SceneManager.LoadSceneAsync("HubScene");
    }
}
