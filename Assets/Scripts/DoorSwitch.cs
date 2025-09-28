using UnityEngine;

/// <summary>
/// DoorSwitch - A trigger-based switch that opens a door when the player enters its collider.
/// Plays audio feedback and optionally deactivates itself after activation.
/// </summary>
public class DoorSwitch : MonoBehaviour
{
    [Header("Door Reference")]
    [Tooltip("The DoorController component to activate when switch is triggered")]
    public DoorController door;       
    
    [Header("Private References")]
    private AudioSource audioSource;  // Reference to the switch's audio source component

    /// <summary>
    /// Initialize the door switch by getting the AudioSource component
    /// </summary>
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Handle player entering the switch trigger zone
    /// Opens the connected door, plays sound, and deactivates the switch
    /// </summary>
    /// <param name="other">The collider that entered the trigger</param>
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering is the player
        if (other.CompareTag("Player"))
        {
            // Open the connected door
            door.OpenDoor();

            // Play the switch activation sound at the switch's position
            AudioSource.PlayClipAtPoint(audioSource.clip, transform.position);

            // Deactivate the switch so it can't be triggered again
            // This makes it a one-time use switch
            gameObject.SetActive(false);
        }
    }
}
