using UnityEngine;

public class DoorController : MonoBehaviour
{
    public float openAngle = 180f;     // target Y angle when open
    public float closedAngle = 90f;    // starting Y angle when closed
    public float openSpeed = 2f;       // how fast to rotate

    private bool isOpen = false;
    private Quaternion targetRotation;

    void Start()
    {
        // Ensure the door starts closed
        transform.rotation = Quaternion.Euler(0, closedAngle, 0);
        targetRotation = transform.rotation;
    }

    void Update()
    {
        // Smoothly rotate toward target
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * openSpeed);
    }

    public void OpenDoor()
    {
        if (!isOpen)
        {
            targetRotation = Quaternion.Euler(0, openAngle, 0);
            isOpen = true;
        }
    }

    public void CloseDoor()
    {
        if (isOpen)
        {
            targetRotation = Quaternion.Euler(0, closedAngle, 0);
            isOpen = false;
        }
    }
}
