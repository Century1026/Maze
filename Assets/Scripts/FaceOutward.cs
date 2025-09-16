using UnityEngine;

public class FaceOutward : MonoBehaviour
{
    public Transform pivot; // assign your RotatingPlatformPivot in Inspector

    void Update()
    {
        // Get vector from pivot to this bridge
        Vector3 direction = transform.position - pivot.position;
        direction.y = 0; // flatten so it stays horizontal

        if (direction != Vector3.zero)
        {
            // Make the red arrow (X+) point outward
            transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);
            transform.right = direction.normalized;
        }
    }
}
