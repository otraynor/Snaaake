using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class LoadGame : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI loseText;
    public GameObject Object;
    public static Transform ObjectsParent;

    void Start()
    {
        PlayGame();
    }
    public void PlayGame()
    {
        Time.timeScale = 1;
        ObjectsParent = Instantiate(Object, Vector3.zero, Quaternion.identity).transform;
    }

    public void TryAgain()
    {
        Time.timeScale = 1;
        GameObject.Destroy(ObjectsParent.gameObject);
        ObjectsParent = Instantiate(Object, Vector3.zero, Quaternion.identity).transform;
        GameOverManager.Instance.ResetGame();
    }
    
}