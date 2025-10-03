using UnityEngine;
using UnityEngine.InputSystem;  // Unity's new Input System

/// <summary>
/// Collectible - An interactive collectible item that can be picked up by the player.
/// Features proximity detection, particle effects, audio feedback, and mobile touch input.
/// </summary>
public class Collectible : MonoBehaviour
{
    [Header("Collection Settings")]
    [Tooltip("Distance at which the collectible becomes interactive")]
    public float collectDistance = .5f;   
    
    [Header("Audio References")]
    [Tooltip("Sound played when the collectible is successfully collected")]
    public AudioSource collectSound;
    
    [Tooltip("Sound played when player is near the collectible")]
    public AudioSource approachingSound;
    
    [Header("Private References")]
    private Transform player;                    // Reference to player transform
    private PlayerController playerController;   // Reference to player controller
    private ParticleSystem part;                 // Reference to particle system for visual effects

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        playerController = FindFirstObjectByType<PlayerController>();
        part = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        // Check if player is within collection distance
        if (Vector3.Distance(player.position, transform.position) <= collectDistance)
        {
            // Play particle effects when player is near
            if (!part.isPlaying) part.Play();
            
            // Play approaching sound if not already playing
            if (!approachingSound.isPlaying)
                approachingSound.Play();
                
            // Handle touch input (mobile only)
            if (Touchscreen.current != null && 
                Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
            {
                TryCollect();
            }
        }
        else
        {
            // Stop effects when player moves away
            if (part.isPlaying) part.Stop();
            if (approachingSound.isPlaying) approachingSound.Stop();
        }
    }

    void TryCollect()
    {
        // Use touch position
        Vector2 inputPosition = Touchscreen.current.primaryTouch.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(inputPosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.transform == transform)
            {
                // Play collection sound at collectible position
                AudioSource.PlayClipAtPoint(collectSound.clip, transform.position);
                
                // Deactivate the collectible (mark as collected)
                gameObject.SetActive(false);
                
                // Add point to game score
                GameUIManager.Instance.AddPoint();
            }
        }
    }
}
