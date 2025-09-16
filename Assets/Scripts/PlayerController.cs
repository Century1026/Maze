using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    public TextMeshProUGUI countText;
    public TextMeshProUGUI timerText;
    public GameObject winTextObject;
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
        winTextObject.SetActive(false);

        startTime = Time.time;
        isTimerRunning = true;
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
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
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
        }
    }

    public void OnRestart()
    {
        // Reload the currently active scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void SetCountText()
    {
        countText.text = "Point: " + count.ToString();
        if (count >= 2)
        {
            winTextObject.SetActive(true);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))  // make sure your floor has tag = "Ground"
        {
            isGrounded = true;
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
