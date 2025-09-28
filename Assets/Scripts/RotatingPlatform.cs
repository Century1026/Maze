using UnityEngine;

/// <summary>
/// RotatingPlatform - A simple component that continuously rotates a platform
/// around its Y-axis at a specified speed. Useful for creating moving platforms
/// or obstacles that rotate in the game world.
/// </summary>
public class RotatingPlatform : MonoBehaviour
{
    [Header("Rotation Settings")]
    [Tooltip("Rotation speed in degrees per second around the Y-axis")]
    public float rotationSpeed = 30f; 

    /// <summary>
    /// Continuously rotate the platform around the Y-axis every frame
    /// </summary>
    void Update()
    {
        // Rotate around the Y-axis at the specified speed
        // Time.deltaTime ensures consistent rotation speed regardless of framerate
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }
}