using UnityEngine;
using UnityEngine.InputSystem; // Unity's new Input System

/// <summary>
/// CameraTiltSimulator - A camera controller that provides a top-down view with
/// dynamic tilting capabilities. Uses WASD keys to tilt the camera in different
/// directions while maintaining a fixed position above the target.
/// </summary>
public class CameraTiltSimulator : MonoBehaviour
{
    [Header("Target Reference")]
    [Tooltip("The target object to follow and look down upon")]
    public Transform target;      
    
    [Header("Camera Settings")]
    [Tooltip("Height of camera above the target")]
    public float height = 10f;        
    
    [Tooltip("Maximum tilt angle in degrees")]
    public float maxTilt = 30f;       
    
    [Tooltip("Speed of tilt transitions (higher = more responsive)")]
    public float tiltSpeed = 5f;      

    [Header("Tilt State Variables")]
    private float tiltX = 0f;         // Current forward/backward tilt angle
    private float tiltY = 0f;         // Current left/right tilt angle
    private float targetTiltX = 0f;   // Desired forward/backward tilt angle
    private float targetTiltY = 0f;   // Desired left/right tilt angle

    /// <summary>
    /// Update camera position and rotation in LateUpdate to ensure smooth following
    /// and responsive tilt controls using WASD input
    /// </summary>
    void LateUpdate()
    {
        // Safety check - ensure we have a valid target reference
        if (target == null) return;

        // --- Input Handling: WASD controls camera tilt ---
        // W/S keys control forward/backward tilt
        if (Keyboard.current.wKey.isPressed) targetTiltX = maxTilt;
        else if (Keyboard.current.sKey.isPressed) targetTiltX = -maxTilt;
        else targetTiltX = 0f;

        // A/D keys control left/right tilt
        if (Keyboard.current.aKey.isPressed) targetTiltY = -maxTilt;
        else if (Keyboard.current.dKey.isPressed) targetTiltY = maxTilt;
        else targetTiltY = 0f;

        // --- Smooth Tilt Transitions ---
        // Interpolate current tilt values towards target values for smooth movement
        tiltX = Mathf.Lerp(tiltX, targetTiltX, Time.deltaTime * tiltSpeed);
        tiltY = Mathf.Lerp(tiltY, targetTiltY, Time.deltaTime * tiltSpeed);

        // --- Camera Positioning ---
        // Keep camera at a fixed height above the target
        transform.position = target.position + Vector3.up * height;

        // --- Camera Rotation: Combine base rotation with tilt ---
        Quaternion baseRotation = Quaternion.Euler(90f, 0f, 0f); // Base: look straight down
        Quaternion xTilt = Quaternion.Euler(tiltX, 0f, 0f);      // Forward/backward tilt
        Quaternion yTilt = Quaternion.Euler(0f, tiltY, 0f);      // Left/right tilt

        // Apply rotations in sequence: base rotation, then Y tilt, then X tilt
        transform.rotation = baseRotation * yTilt * xTilt;
    }
}
