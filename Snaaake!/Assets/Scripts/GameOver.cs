using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOver;
    public GameObject scoreText;
    public static bool IsOver = false;

    private void Start()
    {
        Debug.Log("GameOverManager Start: Initializing game over state.");
        IsOver = false;
        gameOver.SetActive(false);
        Time.timeScale = 1f;
    }

    public void GameOver()
    {
        Debug.Log("Game Over!");
        IsOver = true;
        gameOver.SetActive(true);
        Time.timeScale = 0;
    }

    public void GoToMainMenu()
    {
        Debug.Log("Returning to Main Menu.");
        IsOver = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void TryAgain()
    {
        Debug.Log("Retrying the game.");
        IsOver = false;
        Time.timeScale = 1f;

        // Call ResetGame from GameBehavior script
        if (GameBehavior.Instance != null)
        {
            GameBehavior.Instance.ResetGame();
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting the game.");
        Application.Quit();
    }
}