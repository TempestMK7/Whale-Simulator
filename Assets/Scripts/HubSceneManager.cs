using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubSceneManager : MonoBehaviour {

    private const float rightBound = 45f * 1.5f;
    private const float upperBound = 25f * 0.5f;

    public GameObject backgroundHolder;
    public Camera mainCamera;

    public float usableWidth = 1.0f;
    public float usableHeight = 1.0f;

    private Vector3? clickPosition;
    private Vector3? startingClickPosition;

    void Start() {

    }

    void Update() {
        HandleClickEvents();
        HandleTouchEvents();
    }

    private void HandleClickEvents() {
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

    private void HandleTouchEvents() {
        if (Input.touchCount > 0) {
            var touch = Input.GetTouch(0);
            var touchPosition = (Vector3)touch.position;
            if (startingClickPosition == null) {
                startingClickPosition = touchPosition;
            }

            var vertExtent = mainCamera.orthographicSize;
            var horzExtent = vertExtent * Screen.width / Screen.height;

            var vertRatio = (vertExtent * 2f) / Screen.height;
            var horzRatio = (horzExtent * 2f) / Screen.width;

            var oldPosition = clickPosition ?? touchPosition;
            var distance = oldPosition - touchPosition;
            var modifiedDistance = new Vector3(distance.x * horzRatio, distance.y * vertRatio, 0f);
            mainCamera.transform.position += modifiedDistance;
            ClampCameraToPlayableArea(horzExtent, vertExtent);
            clickPosition = touchPosition;

            if (touch.phase == TouchPhase.Ended) {
                var totalDistance = (startingClickPosition ?? touchPosition) - touchPosition;
                if (totalDistance.magnitude < 5f) {
                    PerformClick(touchPosition);
                }
                startingClickPosition = null;
            }
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
