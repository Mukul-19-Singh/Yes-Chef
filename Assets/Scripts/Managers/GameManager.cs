using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singeton instance of GameManager
    public static GameManager Instance { get; private set; }

    public bool IsPlaying { get; private set; } = false;
    private float timeRemaining = 180f; // 3 minutes in seconds

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
    }

    public void StartGame()
    {
        // Ensures the game starts in a playing state
        IsPlaying = true;
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
        }
        else
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        IsPlaying = false;
        timeRemaining = 0; 
    }
}
