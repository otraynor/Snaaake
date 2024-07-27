using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class LoadGame : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI loseText;
    public SnakeBehavior snakeBehavior; // Reference to SnakeBehavior

    public void PlayGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("SinglePlayer");
    }

    public void TryAgain()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}