using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class HubSceneManager : MonoBehaviour {

    private const float rightBound = 45f * 1.5f;
    private const float upperBound = 25f * 0.5f;

    public GameObject backgroundHolder;
    public Camera mainCamera;
    public Canvas mainCanvas;

    public GameObject settingsPopupPrefab;
    public GameObject cheaterPopupPrefab;
    public TooltipPopup tooltipPopupPrefab;

    public float usableWidth = 1.0f;
    public float usableHeight = 1.0f;

    private Vector3? clickPosition;
    private Vector3? startingClickPosition;

    public void Start() {
        var state = StateManager.GetCurrentState();
        if (!state.HasEnteredHub) {
            var tooltip = Instantiate(tooltipPopupPrefab, mainCanvas.transform);
            tooltip.SetTooltip("Welcome to W.H.A.L.E.",
                "Welcome to the World of Horrors And Legends Eternal!\nWe need to get you started with some heroes that you can use to fight against all of the terrible things that have been attacking our village.\nClick on the portal in the middle of the screen.");
            StateManager.NotifyHubEntered();
        }
    }

    public void Update() {
        HandleClickEvents();
    }

    public void OnSettingsClicked() {
        if (PopupOpened()) return;
        var settingsPopup = Instantiate(settingsPopupPrefab, mainCanvas.transform).GetComponent<SettingsPopupManager>();
        settingsPopup.LaunchPopup();
    }
    
    public void OnHeroClicked() {
        SceneManager.LoadSceneAsync("HeroScene");
    }

    public void OnEquipmentClicked() {
        SceneManager.LoadSceneAsync("EquipmentScene");
    }

    public void OnCheatPressed() {
        if (PopupOpened()) return;
        var cheaterPopup = Instantiate(cheaterPopupPrefab, mainCanvas.transform).GetComponent<CheaterPopupBehavior>();
        cheaterPopup.LaunchPopup();
    }

    private bool PopupOpened() {
        return FindObjectOfType<SettingsPopupManager>() != null || FindObjectOfType<CheaterPopupBehavior>() != null || FindObjectOfType<TooltipPopup>() != null;
    }

    private void HandleClickEvents() {
        if (PopupOpened()) return;
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
                PerformClick(Input.mousePosition);
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

    private void PerformClick(Vector3 cursorPosition) {
        var cameraPosition = mainCamera.transform.position;
        var vertExtent = mainCamera.orthographicSize;
        var horzExtent = vertExtent * Screen.width / Screen.height;

        var vertUnitsPerPixel = (vertExtent * 2f) / Screen.height;
        var vertPosition = vertUnitsPerPixel * cursorPosition.y;
        var bottomPosition = cameraPosition.y - vertExtent;
        var cursorVertPosition = vertPosition + bottomPosition;

        var horzUnitsPerPixel = (horzExtent * 2f) / Screen.width;
        var horzPosition = horzUnitsPerPixel * cursorPosition.x;
        var leftPosition = cameraPosition.x - horzExtent;
        var cursorHorzPosition = leftPosition + horzPosition;

        Vector2 cursorTruePosition = new Vector2(cursorHorzPosition, cursorVertPosition);

        Collider2D[] hitColliders = Physics2D.OverlapBoxAll(cursorTruePosition, new Vector2(0.5f, 0.5f), 0f);
        if (hitColliders.Length > 0) {
            Collider2D collider = hitColliders[0];
            collider.GetComponentInParent<HubBuilding>().OnClickEvent();
        }
    }
}
