using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class CampaignSceneManager : MonoBehaviour {

    private const float rightBound = 45f * 0.5f;
    private const float upperBound = 25f * 0.5f;

    public Camera mainCamera;
    public Canvas mainCanvas;

    public Text nameText;
    public Text chapterText;
    public Text missionText;

    public HeroPlaceholderBehavior[] heroPlaceholders;

    public GameObject fieldBackgroundPrefab;
    public GameObject crystalCaveBackgroundPrefab;

    public float usableWidth = 1.0f;
    public float usableHeight = 1.0f;

    private Vector3? clickPosition;
    private Vector3? startingClickPosition;
    private GameObject background;

    public void Awake() {
        BindEverything();
    }

    public void Update() {
        HandleClickEvents();
    }

    public void BindEverything() {
        mainCamera.transform.position = new Vector3(-15f, -2f, -10f);
        if (background != null) Destroy(background);
        background = Instantiate(fieldBackgroundPrefab);
        background.transform.position = new Vector3();

        var state = StateManager.GetCurrentState();
        nameText.text = state.PlayerName;
        chapterText.text = string.Format("Chapter {0}", state.CurrentChapter);
        missionText.text = string.Format("Mission {0}", state.CurrentMission);

        for (int x = 0; x < heroPlaceholders.Length; x++) {
            var mission = MissionContainer.GetMission(state.CurrentChapter, x + 1);
            heroPlaceholders[x].SetHero(mission.FaceOfMission);

            if (state.CurrentChapter > x + 1) {
                heroPlaceholders[x].GetComponent<Animator>().SetTrigger("Die");
            }

            if (x == state.CurrentMission - 1) {
                heroPlaceholders[x].RegisterOnClick(LaunchNextMission);
            } else {
                heroPlaceholders[x].RegisterOnClick(null);
            }
        }
    }

    public void LaunchNextMission() {
        var state = StateManager.GetCurrentState();
        var currentMission = MissionContainer.GetMission(state.CurrentChapter, state.CurrentMission);
        Debug.Log(string.Format("Launching Chapter {0} Mission {1}", state.CurrentChapter, state.CurrentMission));
    }

    public void OnBackPressed() {
        SceneManager.LoadScene("HubScene");
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
            var placeholder = hit.transform.gameObject.GetComponent<HeroPlaceholderBehavior>();
            if (placeholder != null) placeholder.OnClick();
        }
    }
}
