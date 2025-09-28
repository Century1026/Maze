using UnityEngine;

/// <summary>
/// CameraController - A top-down camera system that follows the player from above.
/// Provides a bird's eye view with smooth zoom functionality and maintains
/// a consistent overhead perspective for gameplay.
/// </summary>
public class CameraController : MonoBehaviour
{
    [Header("Target Reference")]
    [Tooltip("The player transform to follow")]
    public Transform player;      
    
    [Header("Camera Positioning")]
    [Tooltip("Height of camera above the player")]
    public float height = 10f;        
    
    [Header("Zoom Settings")]
    [Tooltip("Target field of view (smaller values = more zoomed in)")]
    public float targetFOV = 40f;     
    
    [Tooltip("Speed of zoom transitions")]
    public float zoomSpeed = 2f;      

    [Header("Private References")]
    private Camera cam; // Reference to the camera component

    /// <summary>
    /// Initialize the camera controller by getting the camera component
    /// and ensuring it's set to perspective mode
    /// </summary>
    void Start()
    {
        cam = GetComponent<Camera>();
        cam.orthographic = false; // Force perspective mode for FOV control
    }

    /// <summary>
    /// Update camera position and rotation in LateUpdate to ensure
    /// it follows the player after all movement has been processed
    /// </summary>
    void LateUpdate()
    {
        // Safety check - ensure we have a valid player reference
        if (player == null) return;

        // Position camera directly above the player at the specified height
        transform.position = new Vector3(player.position.x, player.position.y + height, player.position.z);

        // Set camera to look straight down (90 degrees on X-axis)
        transform.rotation = Quaternion.Euler(90f, 0f, 0f);

        // Smoothly interpolate the field of view towards the target FOV
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetFOV, Time.deltaTime * zoomSpeed);
    }
}
