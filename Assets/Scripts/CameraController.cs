using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;      
    public float height = 10f;        // how high above the player
    public float targetFOV = 40f;     // smaller = zoom in
    public float zoomSpeed = 2f;      // how fast zoom happens

    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
        cam.orthographic = false; // force perspective
    }

    void LateUpdate()
    {
        if (player == null) return;

        // Keep camera directly above player
        transform.position = new Vector3(player.position.x, player.position.y + height, player.position.z);

        // Look straight down
        transform.rotation = Quaternion.Euler(90f, 0f, 0f);

        // Smooth zoom with perspective FOV
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetFOV, Time.deltaTime * zoomSpeed);
    }
}
