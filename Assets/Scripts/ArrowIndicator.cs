using UnityEngine;
using UnityEngine.UI;

public class ArrowIndicator : MonoBehaviour
{
    [Header("Target References")]
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

        // Convert world position of end zone to screen space
        Vector3 endZoneScreenPos = mainCamera.WorldToScreenPoint(endZone.position);

        // Check if end zone is within screen bounds
        bool isVisible = endZoneScreenPos.z > 0 &&
                         endZoneScreenPos.x > 0 && endZoneScreenPos.x < Screen.width &&
                         endZoneScreenPos.y > 0 && endZoneScreenPos.y < Screen.height;

        GetComponent<Image>().enabled = !isVisible;

        if (!isVisible)
        {
            // Calculate direction from screen center to target
            Vector2 screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);
            Vector2 dir = ((Vector2)endZoneScreenPos - screenCenter).normalized;

            // Angle for arrow rotation
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            arrowRect.rotation = Quaternion.Euler(0, 0, angle);

            // Find intersection point with screen edges
            Vector2 edgePos = screenCenter + dir * 1000f; // large distance
            edgePos.x = Mathf.Clamp(edgePos.x, 50f, Screen.width - 50f); // padding
            edgePos.y = Mathf.Clamp(edgePos.y, 50f, Screen.height - 50f);

            arrowRect.position = edgePos;
        }
    }
}
