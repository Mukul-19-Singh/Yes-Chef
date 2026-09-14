using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Singeton instance of GameManager
    public static GameManager Instance { get; private set; }

    public bool IsPlaying { get; private set; } = false;
    private float timeRemaining = 180f; // 3 minutes in seconds

    private int currentScore = 0;
    private int highScore = 0;

    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text highScoreText;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TMP_Text gameOverText;
    [SerializeField] private Button pauseButton;

    private void Awake()
    {
        if (Instance == null) 
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        // Loads the stored high score
        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    public void StartGame()
    {
        // Ensures the game starts in a playing state
        IsPlaying = true;
        currentScore = 0;
        timeRemaining = 180f;
        UpdateScoreUI();
    }

    private void Update()
    {
        if (!IsPlaying) return;
        UpdateTimer();
    }

    private void UpdateTimer()
    {
        // Ensures the timer counts down only when the game is in a playing state
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            // Formats timer as Minutes:Seconds
            int minutes = Mathf.FloorToInt(timeRemaining / 60);
            int seconds = Mathf.FloorToInt(timeRemaining % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        else
        {
            EndGame();
        }
    }

    public void AddScore(int points)
    {
        currentScore += points;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        scoreText.text = "Score: " + currentScore;
        highScoreText.text = "High Score: " + highScore;
    }

    private void EndGame()
    {
        IsPlaying = false;
        // timeRemaining = 0; 
        timerText.text = "00:00";
        gameOverPanel.SetActive(true);
        pauseButton.gameObject.SetActive(false);
        // Checks and records new high score
        if (currentScore > highScore)
        {
            highScore = currentScore;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
            gameOverText.text = "New High Score!\nFinal Score: " + currentScore; // Acknowledges new high score
        }
        else
        {
            gameOverText.text = "Time's Up!\nFinal Score: " + currentScore;
        }
    }
}
