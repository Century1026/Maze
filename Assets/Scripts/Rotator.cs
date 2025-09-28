using UnityEngine;

/// <summary>
/// Rotator - A basic rotation component that continuously rotates an object
/// around all three axes (X, Y, Z) at different speeds. Creates a complex
/// spinning motion that can be used for decorative objects or collectibles.
/// </summary>
public class Rotator : MonoBehaviour
{
    /// <summary>
    /// Continuously rotate the object around all three axes every frame
    /// Creates a complex spinning motion with different speeds for each axis
    /// </summary>
    void Update()
    {
        // Rotate around X, Y, and Z axes at different speeds (15, 30, 45 degrees per second)
        // Time.deltaTime ensures consistent rotation speed regardless of framerate
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
    }
}
