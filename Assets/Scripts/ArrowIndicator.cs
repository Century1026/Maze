using UnityEngine;
using UnityEngine.UI;

public class ArrowIndicator : MonoBehaviour
{
    public Transform player;
    public Transform endZone;
    public Camera mainCamera;

    private RectTransform arrowRect;

    void Start()
    {
        arrowRect = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (endZone == null || player == null) return;

        // Convert positions to screen space
        Vector3 playerScreenPos = mainCamera.WorldToScreenPoint(player.position);
        Vector3 endZoneScreenPos = mainCamera.WorldToScreenPoint(endZone.position);

        bool isVisible = endZone.GetComponent<Renderer>().isVisible;
        GetComponent<Image>().enabled = !isVisible;

        if (!isVisible)
        {
            // Direction from player to EndZone (in screen space)
            Vector2 dir = (endZoneScreenPos - playerScreenPos).normalized;

            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            // Place arrow in middle for now
            arrowRect.position = new Vector3(Screen.width / 2, Screen.height / 2, 0f);

            // Rotate arrow
            arrowRect.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
