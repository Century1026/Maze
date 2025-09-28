using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameUIManager : MonoBehaviour
{
    public static GameUIManager Instance;

    public TextMeshProUGUI countText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI victoryCountText;
    public TextMeshProUGUI victoryTimerText;
    public PageManager pageManager;
    public GameObject Status;

    private int score;
    private float startTime;
    private bool isTimerRunning;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        ResetGame();
    }

    void Update()
    {
        if (isTimerRunning)
        {
            timerText.text = "Time: " + (Time.time - startTime).ToString("F2");
        }
    }

    public void AddPoint()
    {
        score++;
        countText.text = "Point: " + score;
    }

    public void ResetGame()
    {
        score = 0;
        startTime = Time.time;
        isTimerRunning = true;
        countText.text = "Point: 0";
        timerText.text = "Time: 0.00";
        // victoryScreen.SetActive(false);
    }

    public void ShowVictory()
    {
        isTimerRunning = false;
        victoryCountText.text = countText.text;
        victoryTimerText.text = timerText.text;
        Status.SetActive(false);
        pageManager.NextPage();
    }
}
