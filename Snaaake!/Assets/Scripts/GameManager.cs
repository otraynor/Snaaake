using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject SnakeWorldPrefab;
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
              //  Debug.Log("GM Null Error");
            }
            return _instance;
        }
    }

    public int Score { get; set; }

    private void Awake()
    {
        _instance = this;
    }
}
