using UnityEngine;

public class DoorSwitch : MonoBehaviour
{
    public DoorController door;       // drag DoorPivot here in Inspector
    private AudioSource audioSource;  // reference to sound

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            door.OpenDoor();

            // play switch sound
            AudioSource.PlayClipAtPoint(audioSource.clip, transform.position);

            // make switch disappear (optional)
            gameObject.SetActive(false);
        }
    }
}
