using UnityEngine;
using UnityEngine.InputSystem;  // new Input System

public class Collectible : MonoBehaviour
{
    public float collectDistance = .5f;   
    private Transform player;
    private PlayerController playerController;
    private AudioSource audioSource;
    private ParticleSystem part;

    [System.Obsolete]
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        audioSource = GetComponent<AudioSource>();
        playerController = FindObjectOfType<PlayerController>();
        part = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (Vector3.Distance(player.position, transform.position) <= collectDistance)
        {
            part.Play();
            // Mouse click (desktop)
            if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
            {
                TryCollect();
            }

            // Touch tap (mobile)
            if (Touchscreen.current != null && Touchscreen.current.primaryTouch.tapCount.ReadValue() > 0)
            {
                TryCollect();
            }
        }
        else
        {
            part.Stop();
        }
    }

    void TryCollect()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current != null ? Mouse.current.position.ReadValue() : Touchscreen.current.primaryTouch.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.transform == transform)
            {
                AudioSource.PlayClipAtPoint(audioSource.clip, transform.position);
                gameObject.SetActive(false); // collected
                playerController.AddPoint();
            }
        }
    }
}
