using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject scoreText;
    public static bool IsPaused;

    void Start()
    {
        pauseMenu.SetActive(false); 
        scoreText.SetActive(true);
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        Debug.Log("Game Paused");
        pauseMenu.SetActive(true);
        scoreText.SetActive(false); // Hide score text
        Time.timeScale = 0f;
        IsPaused = true;
    }

    public void ResumeGame()
    {
        Debug.Log("Game Resumed");
        pauseMenu.SetActive(false);
        scoreText.SetActive(true);
        Time.timeScale = 1f;
        IsPaused = false;
    }

    public void GoToMainMenu()
    {
        Debug.Log("Going to Main Menu");
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
        IsPaused = false;
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }
}