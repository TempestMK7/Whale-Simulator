using UnityEngine;
using UnityEngine.SceneManagement;
using Com.Tempest.Whale.GameObjects;
using Com.Tempest.Whale.ResourceContainers;

public class InitializationSceneManager : MonoBehaviour {

    public void Start() {
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

        Debug.Log("Downloading state.");
        FindObjectOfType<CredentialsManager>().DownloadStateFromServer(OnStateDownloaded);
    }

    public void OnStateDownloaded() {
        Debug.Log("Releasing to hub scene.");
        SceneManager.LoadSceneAsync("HubScene");
    }
}
