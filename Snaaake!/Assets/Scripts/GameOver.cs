using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOver;
    public GameObject scoreText;
    public GameObject highScoreText;
    private static GameOverManager _instance;
    public static GameOverManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.Log("GM Null Error");
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
        scoreText.SetActive(true);
        highScoreText.SetActive(true);
        Time.timeScale = 1;
        SnakeBehavior.Body.Clear();
        ScoreManager.Instance.ResetScore();
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