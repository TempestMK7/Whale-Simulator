using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Com.Tempest.Whale.GameObjects;

public class CampaignSceneManager : MonoBehaviour {

    private const float rightBound = 45f * 0.5f;
    private const float upperBound = 25f * 0.5f;

    public Camera mainCamera;
    public Canvas mainCanvas;

    public Text nameText;
    public Text chapterText;
    public Text missionText;

    public AnimatedHero[] heroPlaceholders;

    public GameObject fieldBackgroundPrefab;
    public GameObject crystalCaveBackgroundPrefab;

    public TooltipPopup tooltipPrefab;

    public float usableWidth = 1.0f;
    public float usableHeight = 1.0f;

    private Vector3? clickPosition;
    private Vector3? startingClickPosition;
    private GameObject background;

    private StateManager stateManager;

    public void Awake() {
        stateManager = FindObjectOfType<StateManager>();
        BindEverything();
    }

    public void Start() {
        if (!stateManager.CurrentAccountState.HasEnteredCampaign) {
            var tooltip = Instantiate(tooltipPrefab, mainCanvas.transform);
            tooltip.SetTooltip("This is where you fight!", "As you can see, there are a lot of groups of very bad things attacking us.\nWe'll need to fight our way through, so touch the first one to get started.");
            stateManager.CurrentAccountState.HasEnteredCampaign = true;
            _ = FindObjectOfType<CredentialsManager>().UpdateTutorials();
        }
    }

    public void Update() {
        HandleClickEvents();
    }

    public void BindEverything() {
        mainCamera.transform.position = new Vector3(-15f, -2f, -10f);
        if (background != null) Destroy(background);
        background = Instantiate(fieldBackgroundPrefab);
        background.transform.position = new Vector3();

        nameText.text = stateManager.CurrentAccountState.PlayerName;
        chapterText.text = string.Format("Chapter {0}", stateManager.CurrentAccountState.CurrentChapter);
        missionText.text = string.Format("Mission {0}", stateManager.CurrentAccountState.CurrentMission);

        for (int x = 0; x < heroPlaceholders.Length; x++) {
            var mission = MissionContainer.GetMission(stateManager.CurrentAccountState.CurrentChapter, x + 1);
            if (mission == null) {
                heroPlaceholders[x].gameObject.SetActive(false);
            } else {
                heroPlaceholders[x].SetHero(mission.FaceOfMission);

                if (stateManager.CurrentAccountState.CurrentMission > x + 1) {
                    heroPlaceholders[x].Death();
                }

                if (x == stateManager.CurrentAccountState.CurrentMission - 1) {
                    heroPlaceholders[x].RegisterOnClick(LaunchNextMission);
                    mainCamera.transform.position = new Vector3(heroPlaceholders[x].transform.position.x, -2f, -10f);

                    var vertExtent = mainCamera.orthographicSize;
                    var horzExtent = vertExtent * Screen.width / Screen.height;
                    ClampCameraToPlayableArea(horzExtent, vertExtent);
                } else {
                    heroPlaceholders[x].RegisterOnClick(null);
                }
            }
        }
    }

    public void LaunchNextMission() {
        BattleManager.SelectBattleType(BattleEnum.CAMPAIGN);
        SceneManager.LoadSceneAsync("BattleScene");
    }

    public void OnBackPressed() {
        SceneManager.LoadSceneAsync("HubScene");
    }

    private void HandleClickEvents() {
        if (EventSystem.current.IsPointerOverGameObject()) {
            clickPosition = null;
            return;
        }

        if (Input.GetMouseButtonDown(0)) {
            clickPosition = Input.mousePosition;
            startingClickPosition = Input.mousePosition;
            return;
        }
        if (Input.GetMouseButtonUp(0)) {
            clickPosition = null;
            var distance = (startingClickPosition ?? Input.mousePosition) - Input.mousePosition;
            if (distance.magnitude < 5f) {
                PerformClick();
            }
            startingClickPosition = null;
            return;
        }
        if (Input.GetMouseButton(0)) {
            var vertExtent = mainCamera.orthographicSize;
            var horzExtent = vertExtent * Screen.width / Screen.height;

            var vertRatio = (vertExtent * 2f) / Screen.height;
            var horzRatio = (horzExtent * 2f) / Screen.width;

            var oldPosition = clickPosition ?? Input.mousePosition;
            var distance = oldPosition - Input.mousePosition;
            var modifiedDistance = new Vector3(distance.x * horzRatio, distance.y * vertRatio, 0f);
            mainCamera.transform.position += modifiedDistance;
            ClampCameraToPlayableArea(horzExtent, vertExtent);
            clickPosition = Input.mousePosition;
        }
    }

    private void ClampCameraToPlayableArea(float cameraWidth, float cameraHeight) {
        var furthestRight = (rightBound * usableWidth) - cameraWidth;
        var furthestUp = (upperBound * usableHeight) - cameraHeight;

        var newX = Mathf.Clamp(mainCamera.transform.position.x, -furthestRight, furthestRight);
        var newY = Mathf.Clamp(mainCamera.transform.position.y, -furthestUp, furthestUp);

        mainCamera.transform.position = new Vector3(newX, newY, -10f);
    }

    private void PerformClick() {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, LayerMask.NameToLayer("Hero"))) {
            var placeholder = hit.transform.gameObject.GetComponent<AnimatedHero>();
            if (placeholder != null) placeholder.OnClick();
        }
    }
}
