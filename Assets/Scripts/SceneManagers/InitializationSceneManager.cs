using UnityEngine;
using UnityEngine.SceneManagement;
using Com.Tempest.Whale.GameObjects;
using Com.Tempest.Whale.ResourceContainers;

public class InitializationSceneManager : MonoBehaviour {

    public async void Start() {
        Application.targetFrameRate = 60;

        BaseHeroContainer.Initialize();
        BaseEquipmentContainer.Initialize();
        AttackInfoContainer.Initialize();
        AbilityInfoContainer.Initialize();

        FactionIconContainer.Initialize();
        RoleIconContainer.Initialize();
        AttackParticleContainer.Initialize();
        RewardIconContainer.Initialize();

        SettingsManager.GetInstance();

        var credentialsManager = FindObjectOfType<CredentialsManager>();
        Debug.Log("Initializing Credentials.");
        await credentialsManager.InitializeEverything();
        Debug.Log("Downloading State.");
        await credentialsManager.DownloadState();
        Debug.Log("Loading Hub.");
        SceneManager.LoadSceneAsync("HubScene");
    }
}
