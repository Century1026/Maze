using UnityEngine;

/// <summary>
/// FaceOutward - Orients an object to face outward from a pivot point.
/// Useful for objects like bridges or platforms that need to maintain
/// their orientation relative to a rotating platform or central point.
/// </summary>
public class FaceOutward : MonoBehaviour
{
    [Header("Pivot Reference")]
    [Tooltip("The pivot point that this object should face outward from")]
    public Transform pivot; 

    /// <summary>
    /// Update the object's rotation every frame to face outward from the pivot
    /// </summary>
    void Update()
    {
        // Calculate the direction vector from pivot to this object
        Vector3 direction = transform.position - pivot.position;
        
        // Flatten the Y component to keep the object horizontal
        direction.y = 0; 

        // Only update rotation if we have a valid direction
        if (direction != Vector3.zero)
        {
            // Set the object's rotation to face outward from the pivot
            // First set a base rotation (facing forward with up vector)
            transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);
            
            // Then align the right vector (X+) to point outward from the pivot
            transform.right = direction.normalized;
        }
    }
}
