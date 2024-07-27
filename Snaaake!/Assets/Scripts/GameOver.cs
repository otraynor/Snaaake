using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOver;
    public GameObject scoreText;
    public GameObject highScoreText;

    public void Start()
    {
        gameOver.SetActive(false);
        Time.timeScale = 1f;
    }

    public void GameOver()
    {
        gameOver.SetActive(true);
        scoreText.SetActive(false);
        highScoreText.SetActive(false);
        Time.timeScale = 0;
       // PlayerPrefs.SetInt("HighScore", game score);
    }
}