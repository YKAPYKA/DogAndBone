using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("UI Panels")]
    public GameObject winPanel;
    public GameObject gameOverPanel;

    public bool IsEnded { get; private set; } = false;

    void Awake()
    {
        // Destroy old instances if any, keep only one per scene
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        ResetPanels();
    }

    // Shows Win Panel
    public void Win()
    {
        if (IsEnded) return;
        IsEnded = true;

        if (winPanel != null) winPanel.SetActive(true);

        Cursor.visible = true; // show cursor for menu

        Time.timeScale = 0f; // pause game
    }

    // Shows Game Over Panel
    public void Lose()
    {
        if (IsEnded) return;
        IsEnded = true;

        if (gameOverPanel != null) gameOverPanel.SetActive(true);

        Cursor.visible = true; // show cursor for menu

        Time.timeScale = 0f; // pause game
    }

    // Called by Try Again button
    public void Restart()
    {
        Time.timeScale = 1f; // unpause
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Called by Main Menu button
    public void LoadMenu()
    {
        Time.timeScale = 1f; // unpause
        SceneManager.LoadScene("MainMenu");
    }

    // Reset panels and game state — call at Start or before scene reload
    private void ResetPanels()
    {
        IsEnded = false;
        Time.timeScale = 1f;

        if (winPanel != null) winPanel.SetActive(false);
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
    }
}
