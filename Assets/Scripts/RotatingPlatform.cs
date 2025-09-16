using UnityEngine;

public class RotatingPlatform : MonoBehaviour
{
    public float rotationSpeed = 30f; // degrees per second

    void Update()
    {
        // Rotate around the Y axis
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }
}