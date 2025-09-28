using UnityEngine;

/// <summary>
/// DoorController - Manages the opening and closing animation of a door.
/// Provides smooth rotation transitions between open and closed states
/// with configurable angles and animation speed.
/// </summary>
public class DoorController : MonoBehaviour
{
    [Header("Door Animation Settings")]
    [Tooltip("Y-axis rotation angle when the door is fully open")]
    public float openAngle = 180f;     
    
    [Tooltip("Y-axis rotation angle when the door is closed")]
    public float closedAngle = 90f;    
    
    [Tooltip("Speed of door rotation animation")]
    public float openSpeed = 2f;       

    [Header("Door State")]
    private bool isOpen = false;           // Current state of the door
    private Quaternion targetRotation;     // Target rotation for smooth animation

    /// <summary>
    /// Initialize the door in a closed state and set up the target rotation
    /// </summary>
    void Start()
    {
        // Ensure the door starts in the closed position
        transform.rotation = Quaternion.Euler(0, closedAngle, 0);
        targetRotation = transform.rotation;
    }

    /// <summary>
    /// Smoothly animate the door rotation towards the target rotation every frame
    /// </summary>
    void Update()
    {
        // Interpolate current rotation towards target rotation for smooth animation
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * openSpeed);
    }

    /// <summary>
    /// Open the door by setting the target rotation to the open angle
    /// Only opens if the door is currently closed
    /// </summary>
    public void OpenDoor()
    {
        if (!isOpen)
        {
            targetRotation = Quaternion.Euler(0, openAngle, 0);
            isOpen = true;
        }
    }

    /// <summary>
    /// Close the door by setting the target rotation to the closed angle
    /// Only closes if the door is currently open
    /// </summary>
    public void CloseDoor()
    {
        if (isOpen)
        {
            targetRotation = Quaternion.Euler(0, closedAngle, 0);
            isOpen = false;
        }
    }
}
