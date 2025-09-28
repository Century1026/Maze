using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    public AudioSource rollingAudio;
    public AudioSource jumpAudio;
    public AudioSource impactAudio;
    public AudioSource endZoneSound;
    private float movementX;
    private float movementY;

    public float speed = 0;

    public float jumpForce = 2f;   // tune this value
    private bool isGrounded;       // check if ball is on floor


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        GameUIManager.Instance.ResetGame();
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
    }

    private void Update()
    {
        // Update timer each frame

        if (transform.position.y < -2f) // adjust threshold to match hole depth
        {
            OnRestart();
        }

        float speed = rb.linearVelocity.magnitude;
        if (isGrounded && speed > 0.1f) // threshold so it doesn't play when "almost still"
        {
            if (!rollingAudio.isPlaying)
                rollingAudio.Play();

            // Adjust volume or pitch by speed
            rollingAudio.volume = Mathf.Clamp01(speed / 10f); 
            rollingAudio.pitch = 1f + (speed / 20f);
        }
        else
        {
            if (rollingAudio.isPlaying)
                rollingAudio.Stop();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            GameUIManager.Instance.AddPoint();
        }

        if (other.CompareTag("EndZone"))
        {
            // StartCoroutine(PlayAndLoad());
            AudioSource.PlayClipAtPoint(endZoneSound.clip, transform.position);
            GameUIManager.Instance.ShowVictory();
        }
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    public void OnJump()
    {
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpAudio.Play();
        }
    }

    public void OnRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))  // make sure your floor has tag = "Ground"
        {
            isGrounded = true;
        }
        
        if (collision.gameObject.CompareTag("Wall")) // make sure walls have tag = Wall
        {
            float impactSpeed = collision.relativeVelocity.magnitude;

            if (impactSpeed > 0.2f) // ignore tiny bumps
            {
                impactAudio.volume = Mathf.Clamp01(impactSpeed / 10f); // scale volume
                impactAudio.PlayOneShot(impactAudio.clip);
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }


}
