using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOver;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    private static GameOverManager _instance;
    public static GameOverManager Instance
    {
        get
        {
            if (_instance == null)
            {
                // Debug.Log("GM Null Error");
            }
            return _instance;
        }
    }

    public int Score { get; set; }

    private void Awake()
    {
        _instance = this;
    }

    public void Start()
    {
        ResetGame();
    }

    public void ResetGame()
    {
        gameOver.SetActive(false);
        scoreText.gameObject.SetActive(true);
        highScoreText.gameObject.SetActive(true);
        Time.timeScale = 1;
        SnakeBehavior.Body.Clear();
        ScoreManager.Instance.ResetScore();
    }

    public void GameOver()
    {
        gameOver.SetActive(true);
        scoreText.gameObject.SetActive(false);
        highScoreText.gameObject.SetActive(false);
        Time.timeScale = 0;
        if (ScoreManager.Instance != null)
        {
            highScoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScore", 0);
        }
    }
}