using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject startScreenPanel;
    [SerializeField] private GameObject hudPanel;
    [SerializeField] private GameObject pausePanel; 
    [SerializeField] private Button pauseButton;

    private bool isPaused = false;

    private void Start()
    {
        // Ensures that the start screen is visible and HUD is hidden on load
        startScreenPanel.SetActive(true);
        hudPanel.SetActive(false);
        pausePanel.SetActive(false);
        pauseButton.gameObject.SetActive(false);
    }

    public void OnStartGameButtonClicked()
    {
        startScreenPanel.SetActive(false);
        hudPanel.SetActive(true);
        pauseButton.gameObject.SetActive(true);
        // Notifies the GameManager to begin the 3 minute loop
        GameManager.Instance.StartGame();
    }

    public void TogglePause()
    {
        if (!GameManager.Instance.IsPlaying) return;
        isPaused = !isPaused;
        pausePanel.SetActive(isPaused);
        Time.timeScale = isPaused ? 0 : 1; // Freezes game time
    }

    public void ResetGame()
    {
        // Resets the game by reloading the active scene
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        // Quits game
        Application.Quit();
    }
}
