using UnityEngine;
using UnityEngine.InputSystem; // new Input System

public class CameraTiltSimulator : MonoBehaviour
{
    public Transform target;      
    public float height = 10f;        // camera height above target
    public float maxTilt = 30f;       // maximum tilt angle in degrees
    public float tiltSpeed = 5f;      // how fast the camera tilts (higher = snappier)

    private float tiltX = 0f;         // current forward/back tilt
    private float tiltY = 0f;         // current left/right tilt
    private float targetTiltX = 0f;   // desired forward/back tilt
    private float targetTiltY = 0f;   // desired left/right tilt

    void LateUpdate()
    {
        if (target == null) return;

        // --- Input: WASD controls target tilt ---
        if (Keyboard.current.wKey.isPressed) targetTiltX = maxTilt;
        else if (Keyboard.current.sKey.isPressed) targetTiltX = -maxTilt;
        else targetTiltX = 0f;

        if (Keyboard.current.aKey.isPressed) targetTiltY = -maxTilt;
        else if (Keyboard.current.dKey.isPressed) targetTiltY = maxTilt;
        else targetTiltY = 0f;

        // --- Smooth transition (Lerp) ---
        tiltX = Mathf.Lerp(tiltX, targetTiltX, Time.deltaTime * tiltSpeed);
        tiltY = Mathf.Lerp(tiltY, targetTiltY, Time.deltaTime * tiltSpeed);

        // --- Camera position: fixed above target ---
        transform.position = target.position + Vector3.up * height;

        // --- Camera rotation: base look down + tilt ---
        Quaternion baseRotation = Quaternion.Euler(90f, 0f, 0f); // straight down
        Quaternion xTilt = Quaternion.Euler(tiltX, 0f, 0f);      // forward/back
        Quaternion yTilt = Quaternion.Euler(0f, tiltY, 0f);      // left/right

        transform.rotation = baseRotation * yTilt * xTilt;
    }
}
