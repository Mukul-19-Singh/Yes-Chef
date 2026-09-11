using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject startScreenPanel;
    [SerializeField] private GameObject hudPanel;

    private void Start()
    {
        // Ensures that the start screen is visible and HUD is hidden on load
        startScreenPanel.SetActive(true);
        hudPanel.SetActive(false);
    }

    public void OnStartGameButtonClicked()
    {
        startScreenPanel.SetActive(false);
        hudPanel.SetActive(true);

        // Notifies the GameManager to begin the 3 minute loop
        GameManager.Instance.StartGame();
    }
}
