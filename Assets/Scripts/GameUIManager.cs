using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

/// <summary>
/// GameUIManager - A singleton manager that handles all game UI elements including
/// score tracking, timer display, and victory screen management. Provides centralized
/// access to game state and UI updates throughout the game.
/// </summary>
public class GameUIManager : MonoBehaviour
{
    [Header("Singleton Instance")]
    public static GameUIManager Instance; // Singleton instance for global access

    [Header("UI Text References")]
    [Tooltip("Text component displaying current score during gameplay")]
    public TextMeshProUGUI countText;
    
    [Tooltip("Text component displaying current time during gameplay")]
    public TextMeshProUGUI timerText;
    
    [Tooltip("Text component displaying final score on victory screen")]
    public TextMeshProUGUI victoryCountText;
    
    [Tooltip("Text component displaying final time on victory screen")]
    public TextMeshProUGUI victoryTimerText;
    
    [Header("UI Management")]
    [Tooltip("PageManager component for handling UI page transitions")]
    public PageManager pageManager;
    
    [Tooltip("GameObject containing the status UI elements")]
    public GameObject Status;

    [Header("Game State Variables")]
    private int score;              // Current player score
    private float startTime;        // Time when the game started
    private bool isTimerRunning;    // Whether the timer is currently active

    /// <summary>
    /// Initialize the singleton instance in Awake to ensure it's available early
    /// </summary>
    void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// Initialize the game state when the scene starts
    /// </summary>
    void Start()
    {
        ResetGame();
    }

    /// <summary>
    /// Update the timer display every frame while the timer is running
    /// </summary>
    void Update()
    {
        if (isTimerRunning)
        {
            // Calculate elapsed time and display it with 2 decimal places
            timerText.text = "Time: " + (Time.time - startTime).ToString("F2");
        }
    }

    /// <summary>
    /// Add a point to the player's score and update the UI display
    /// </summary>
    public void AddPoint()
    {
        score++;
        countText.text = "Point: " + score;
    }

    /// <summary>
    /// Reset the game state to initial values
    /// Called at the start of each level
    /// </summary>
    public void ResetGame()
    {
        score = 0;
        startTime = Time.time;
        isTimerRunning = true;
        countText.text = "Point: 0";
        timerText.text = "Time: 0.00";
        // Note: Victory screen deactivation is handled elsewhere
    }

    /// <summary>
    /// Display the victory screen with final score and time
    /// Unlocks the next level and transitions to the next UI page
    /// </summary>
    public void ShowVictory()
    {
        // Stop the timer
        isTimerRunning = false;
        
        // Copy current score and time to victory screen
        victoryCountText.text = countText.text;
        victoryTimerText.text = timerText.text;

        // Unlock the next level based on current scene index
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        LevelProgress.UnlockLevel(currentIndex + 1);

        // Hide status UI and show victory screen
        Status.SetActive(false);
        pageManager.NextPage();
    }
}
