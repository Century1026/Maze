using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ArrowIndicator - A UI component that displays a directional arrow pointing to the end zone
/// when the end zone is not visible on screen. This helps players navigate when the target
/// is off-screen or obscured.
/// </summary>
public class ArrowIndicator : MonoBehaviour
{
    [Header("Target References")]
    [Tooltip("The player transform to calculate direction from")]
    public Transform player;
    
    [Tooltip("The end zone transform to point towards")]
    public Transform endZone;
    
    [Tooltip("The main camera used for screen space calculations")]
    public Camera mainCamera;

    [Header("Private References")]
    private RectTransform arrowRect; // Reference to the UI arrow's RectTransform

    /// <summary>
    /// Initialize the arrow indicator by getting the RectTransform component
    /// </summary>
    void Start()
    {
        arrowRect = GetComponent<RectTransform>();
    }

    /// <summary>
    /// Update the arrow position and rotation every frame
    /// Shows arrow only when end zone is not visible on screen
    /// </summary>
    void Update()
    {
        // Safety check - ensure we have valid references
        if (endZone == null || player == null) return;

        // Convert world positions to screen space coordinates
        Vector3 playerScreenPos = mainCamera.WorldToScreenPoint(player.position);
        Vector3 endZoneScreenPos = mainCamera.WorldToScreenPoint(endZone.position);

        // Check if the end zone is currently visible in the camera's view
        bool isVisible = endZone.GetComponent<Renderer>().isVisible;
        
        // Show/hide the arrow based on end zone visibility
        GetComponent<Image>().enabled = !isVisible;

        // Only update arrow position and rotation when end zone is not visible
        if (!isVisible)
        {
            // Calculate the direction vector from player to end zone in screen space
            Vector2 dir = (endZoneScreenPos - playerScreenPos).normalized;

            // Convert direction to angle in degrees for rotation
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            // Position the arrow in the center of the screen
            arrowRect.position = new Vector3(Screen.width / 2, Screen.height / 2, 0f);

            // Rotate the arrow to point towards the end zone
            arrowRect.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
