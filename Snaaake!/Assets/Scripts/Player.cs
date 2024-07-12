using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int _score;

    public int Score
    {
        get => _score;
        set
        {
            _score = value;
            ScoreGui.text = Score.ToString();
        }
    }

    [SerializeField] TextMeshProUGUI ScoreGui;
}
