using UnityEngine;
using UnityEngine.InputSystem;  // Unity's new Input System

/// <summary>
/// Collectible - An interactive collectible item that can be picked up by the player.
/// Features proximity detection, particle effects, audio feedback, and cross-platform
/// input support (mouse for desktop, touch for mobile).
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

    /// <summary>
    /// Initialize collectible by finding player references and getting particle system
    /// Note: Uses deprecated FindWithTag method - consider using a more modern approach
    /// </summary>
    [System.Obsolete]
    void Start()
    {
        // Find player using tag (deprecated method - consider using singleton or direct reference)
        player = GameObject.FindWithTag("Player").transform;
        playerController = FindObjectOfType<PlayerController>();
        part = GetComponent<ParticleSystem>();
    }

    /// <summary>
    /// Update collectible state every frame - handles proximity detection,
    /// particle effects, audio feedback, and input processing
    /// </summary>
    void Update()
    {
        // Check if player is within collection distance
        if (Vector3.Distance(player.position, transform.position) <= collectDistance)
        {
            // Play particle effects when player is near
            part.Play();
            
            // Play approaching sound if not already playing
            if (!approachingSound.isPlaying)
                approachingSound.Play();
                
            // Handle mouse input (desktop)
            if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
            {
                TryCollect();
            }

            // Handle touch input (mobile)
            if (Touchscreen.current != null && Touchscreen.current.primaryTouch.tapCount.ReadValue() > 0)
            {
                TryCollect();
            }
        }
        else
        {
            // Stop effects when player moves away
            part.Stop();
            if (approachingSound.isPlaying)
                approachingSound.Stop();
        }
    }

    /// <summary>
    /// Attempt to collect the item by casting a ray from input position
    /// to verify the player is actually clicking/tapping on this collectible
    /// </summary>
    void TryCollect()
    {
        // Create ray from camera through input position (mouse or touch)
        Vector2 inputPosition = Mouse.current != null ? 
            Mouse.current.position.ReadValue() : 
            Touchscreen.current.primaryTouch.position.ReadValue();
            
        Ray ray = Camera.main.ScreenPointToRay(inputPosition);
        
        // Cast ray to check if it hits this collectible
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
