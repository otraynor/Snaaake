using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public static int score = 0;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI loseText;
    public TextMeshProUGUI highScoreText;

    private int highScore = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    void Start()
    {
        UpdateHighScoreText();
        ResetScore();
    }

    public void AddScore(int value)
    {
        score += value;

        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
            loseText.text = "Score: " + score;
        }

        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            UpdateHighScoreText();
        }
    }

    public int GetScore()
    {
        return score;
    }

    public void ResetScore()
    {
        score = 0;
        scoreText.text = "Score: " + score;
        loseText.text = "Score: " + score;
    }

    private void UpdateHighScoreText()
    {
        if (highScoreText != null)
        {
            highScoreText.text = "High Score: " + highScore;
        }
    }
}