using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;
using System;

/// <summary>
/// PlayerController - The main player character controller for a ball-based physics game.
/// Handles movement input, physics-based movement, jumping, audio feedback, collision detection,
/// and game state interactions. Uses Unity's new Input System for cross-platform input handling.
/// </summary>
public class PlayerController : MonoBehaviour
{
    [Header("Physics References")]
    private Rigidbody rb; // Reference to the player's Rigidbody component
    

    [Header("Audio Sources")]
    [Tooltip("Audio source for rolling sound effects")]
    public AudioSource rollingAudio;
    
    [Tooltip("Audio source for jump sound effects")]
    public AudioSource jumpAudio;
    
    [Tooltip("Audio source for impact/collision sound effects")]
    public AudioSource impactAudio;
    
    [Tooltip("Audio source for end zone completion sound")]
    public AudioSource endZoneSound;
    

    [Header("Movement Variables")]
    private float movementX; // Horizontal movement input
    private float movementY; // Vertical movement input (forward/backward)
    

    [Header("Movement Settings")]
    [Tooltip("Force applied to the player for movement")]
    public float speed = 0;
    
    [Tooltip("Force applied when jumping")]
    public float jumpForce = 2f;   


    [Header("Tilt Settings")]
    [Tooltip("Scales how strong the tilt input feels")]
    public float tiltSensitivity = 10f;

    [Tooltip("Ignore tiny accidental tilts below this threshold")]
    public float tiltDeadzone = 0.05f;

    
    [Header("Player State")]
    private bool isGrounded; // Whether the player is currently touching the ground

    private bool tiltAvailable;

    /// <summary>
    /// Initialize the player controller by getting the Rigidbody component
    /// and resetting the game state
    /// </summary>
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        GameUIManager.Instance.ResetGame();
    }

    /// <summary>
    /// Apply movement forces to the player's Rigidbody in FixedUpdate
    /// for consistent physics-based movement
    /// </summary>
    private void FixedUpdate()
    {
        // Create movement vector from input (no Y component for ground-based movement)
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        
        // Apply movement force to the Rigidbody
        rb.AddForce(movement * speed);
    }

    /// <summary>
    /// Handle player state updates, audio feedback, and fall detection
    /// </summary>
    private void Update()
    {
        Vector3 accel = Input.acceleration.normalized;
        // Check if player has fallen below the level (fell into a hole)
        if (transform.position.y < -2f) // Adjust threshold to match hole depth
        {
            OnRestart();
        }

        // Handle rolling audio based on movement speed
        float currentSpeed = rb.linearVelocity.magnitude;
        if (isGrounded && currentSpeed > 0.1f) // Threshold to avoid playing when almost still
        {
            // Start rolling audio if not already playing
            if (!rollingAudio.isPlaying)
                rollingAudio.Play();

            // Adjust volume and pitch based on movement speed for dynamic audio
            rollingAudio.volume = Mathf.Clamp01(currentSpeed / 10f);
            rollingAudio.pitch = 1f + (currentSpeed / 20f);
        }
        else
        {
            // Stop rolling audio when not moving or not grounded
            if (rollingAudio.isPlaying)
                rollingAudio.Stop();
        }

        float tiltForward = accel.y;   // forward/backward tilt
        float tiltRight   = accel.x;   // left/right tilt

        // Deadzone
        if (Mathf.Abs(tiltForward) < tiltDeadzone) tiltForward = 0f;
        if (Mathf.Abs(tiltRight)   < tiltDeadzone) tiltRight   = 0f;

        // Apply sensitivity
        movementX = tiltRight   * tiltSensitivity;
        movementY = tiltForward * tiltSensitivity;
}

    /// <summary>
    /// Handle trigger collisions for collectibles and end zone
    /// </summary>
    /// <param name="other">The collider that was triggered</param>
    void OnTriggerEnter(Collider other)
    {
        // Handle collectible pickup
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            GameUIManager.Instance.AddPoint();
        }

        // Handle end zone completion
        if (other.CompareTag("EndZone"))
        {
            // Play completion sound at player position
            AudioSource.PlayClipAtPoint(endZoneSound.clip, transform.position);
            
            // Show victory screen and unlock next level
            GameUIManager.Instance.ShowVictory();
        }
    }

    /// <summary>
    /// Handle movement input from the new Input System
    /// </summary>
    /// <param name="movementValue">The input value containing movement data</param>
    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    /// <summary>
    /// Handle jump input - applies upward force if player is grounded
    /// </summary>
    public void OnJump()
    {
        if (isGrounded)
        {
            // Apply upward impulse force for jumping
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpAudio.Play();
        }
    }

    /// <summary>
    /// Restart the current level by reloading the scene
    /// Called when player falls or needs to reset
    /// </summary>
    public void OnRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// Handle collision events for ground detection and impact sounds
    /// </summary>
    /// <param name="collision">The collision data</param>
    private void OnCollisionEnter(Collision collision)
    {
        // Check for ground collision to enable jumping
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        
        // Handle wall impact sounds
        if (collision.gameObject.CompareTag("Wall"))
        {
            // Calculate impact speed for dynamic audio
            float impactSpeed = collision.relativeVelocity.magnitude;

            // Only play impact sound for significant impacts
            if (impactSpeed > 0.2f) // Ignore tiny bumps
            {
                // Scale volume based on impact speed
                impactAudio.volume = Mathf.Clamp01(impactSpeed / 10f);
                impactAudio.PlayOneShot(impactAudio.clip);
            }
        }
    }

    /// <summary>
    /// Handle collision exit events for ground detection
    /// </summary>
    /// <param name="collision">The collision data</param>
    private void OnCollisionExit(Collision collision)
    {
        // Check for ground collision exit to disable jumping
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
