using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    public TextMeshProUGUI countText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI victoryCountText;
    public TextMeshProUGUI victoryTimerText;
    public AudioSource rollingAudio;
    public AudioSource jumpAudio;
    public AudioSource impactAudio;
    public AudioSource endZoneSound;
    public PageManager pageManager;
    public Transform startPoint;
    private int count;
    private float movementX;
    private float movementY;

    public float speed = 0;

    public float jumpForce = 2f;   // tune this value
    private bool isGrounded;       // check if ball is on floor

    private float startTime;
    private bool isTimerRunning = true;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();

        startTime = Time.time;
        isTimerRunning = true;
    }

    public void AddPoint()
    {
        count++;
        SetCountText();
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
    }

    private void Update()
    {
        // Update timer each frame
        if (isTimerRunning)
        {
            float t = Time.time - startTime;
            timerText.text = "Time: " + t.ToString("F2"); // 2 decimal places
        }

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
            AddPoint();
        }

        if (other.CompareTag("EndZone"))
        {
            // StartCoroutine(PlayAndLoad());
            AudioSource.PlayClipAtPoint(endZoneSound.clip, transform.position);
            victoryCountText.text = countText.text;
            victoryTimerText.text = timerText.text;
            pageManager.NextPage();
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
        // Reload the currently active scene
        PageManager.pageToLoad = 2;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    // {
    //     // reset player position & physics
    //     rb.linearVelocity = Vector3.zero;
    //     rb.angularVelocity = Vector3.zero;
    //     transform.position = startPoint.position;
    //     transform.rotation = Quaternion.identity;

    //     // reset timer & counter
    //     startTime = Time.time;
    //     isTimerRunning = true;
    //     count = 0;
    //     SetCountText();

    //     // optionally reset collectibles
    //     foreach (GameObject pickup in GameObject.FindGameObjectsWithTag("PickUp"))
    //     {
    //         pickup.SetActive(true);
    //     }
    // }

    void SetCountText()
    {
        countText.text = "Point: " + count.ToString();
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
