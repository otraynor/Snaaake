using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public static int score = 0;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI loseText;
    
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
    }

    public void AddScore(int value)
    {
        score += value;
        
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
            loseText.text = "Score: " + score;
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

}