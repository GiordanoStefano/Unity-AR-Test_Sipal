using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.InputSystem;
using System;

public class ARCarController : MonoBehaviour
{
    [Header("AR")]
    [SerializeField] ARRaycastManager raycastManager;
    [SerializeField] ARPlaneManager planeManager;
    [SerializeField] GameObject carPrefab;
    [SerializeField] GameObject placementIndicator;

    [Header("Scale Limits")]
    [SerializeField] float minScale = 0.1f;
    [SerializeField] float maxScale = 5f;

    private GameObject spawnedCar;
    private bool isPlaced = false;
    private static readonly List<ARRaycastHit> hits = new();

    public static EditMode currentEditMode = EditMode.Free;

    private float previousPinchDistance;
    private float previousTwistAngle;
    private Vector2 previousDragPosition;
    private bool wasTwoFinger;

    public static event Action CarPlaced;

    public static event Action ResetRequested;
    public static void RequestReset() => ResetRequested?.Invoke();
    void OnEnable() => ResetRequested += ResetPlacement;
    void OnDisable() => ResetRequested -= ResetPlacement;


    void Update()
    {
        var touchscreen = Touchscreen.current;
        if (touchscreen == null) return;

        // Count the number of active touches
        var t0 = touchscreen.touches[0];
        var t1 = touchscreen.touches[1];
        bool twoFinger = t1.isInProgress;

        if (!isPlaced)
        {
            UpdateIndicator();
            if (t0.phase.ReadValue() == UnityEngine.InputSystem.TouchPhase.Began && !twoFinger)
                PlaceCar(t0.position.ReadValue());
        }
        else
        {
            if (!twoFinger && t0.isInProgress)
                HandleDrag(t0.position.ReadValue());
            else if (twoFinger)
                HandlePinchAndTwist(t0.position.ReadValue(), t1.position.ReadValue());
        }

        wasTwoFinger = twoFinger;
    }


    #region Placement

    // Show placement indicator and update its position
    void UpdateIndicator()
    {
        var center = new Vector2(Screen.width / 2f, Screen.height / 2f);
        if (raycastManager.Raycast(center, hits, TrackableType.PlaneWithinPolygon)) 
        if (raycastManager.Raycast(center, hits, TrackableType.PlaneWithinPolygon))
        {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(hits[0].pose.position, hits[0].pose.rotation);
        }
        else placementIndicator.SetActive(false);
    }

    // Place the car at the touched position on a detected plane
    void PlaceCar(Vector2 screenPos)
    {
        if (!raycastManager.Raycast(screenPos, hits, TrackableType.PlaneWithinPolygon)) return;

        spawnedCar = Instantiate(carPrefab, hits[0].pose.position, hits[0].pose.rotation);
        isPlaced = true;
        placementIndicator.SetActive(false);
        planeManager.SetTrackablesActive(false);
        CarPlaced?.Invoke();
    }
    #endregion


    #region Drag

    // Move the car by dragging on the screen
    void HandleDrag(Vector2 pos)
    {
        if (wasTwoFinger)
        {
            previousDragPosition = pos;
            return;
        }

        if (Touchscreen.current.touches[0].phase.ReadValue() == UnityEngine.InputSystem.TouchPhase.Began)
        {
            previousDragPosition = pos;
            return;
        }

        if (currentEditMode != EditMode.Free && currentEditMode != EditMode.Move) return;

        // Raycast on AR planes
        if (raycastManager.Raycast(pos, hits, TrackableType.PlaneWithinPolygon))
            spawnedCar.transform.position = hits[0].pose.position;

        previousDragPosition = pos;
    }
    #endregion


    #region Pinch & Twist

    // Handle scaling and rotation based on two-finger pinch and twist gestures
    void HandlePinchAndTwist(Vector2 a, Vector2 b)
    {
        float dist = Vector2.Distance(a, b);
        float angle = Mathf.Atan2(b.y - a.y, b.x - a.x) * Mathf.Rad2Deg;

        if (!wasTwoFinger)
        {
            previousPinchDistance = dist;
            previousTwistAngle = angle;
            return;
        }

        // Scaling
        if (currentEditMode == EditMode.Free || currentEditMode == EditMode.Scale) {
            float ratio = dist / previousPinchDistance;
            float newScale = Mathf.Clamp(spawnedCar.transform.localScale.x * ratio, minScale, maxScale);
            spawnedCar.transform.localScale = Vector3.one * newScale;
        }

        // Rotation
        if (currentEditMode == EditMode.Free || currentEditMode == EditMode.Rotate) {
            float delta = angle - previousTwistAngle;
            spawnedCar.transform.Rotate(Vector3.up, -delta, Space.World);
        }

        previousPinchDistance = dist;
        previousTwistAngle = angle;
    }
    #endregion


    // Reset the placement to allow placing a new car
    public void ResetPlacement()
    {
        if (spawnedCar != null) Destroy(spawnedCar);
        isPlaced = false;
        planeManager.SetTrackablesActive(true);
        placementIndicator.SetActive(true);
    }
}