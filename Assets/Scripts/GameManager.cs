using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public TMP_Text ScoreText;
    public int Score=0;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        UpdateScore();
    }

    public void UpdateScore()
    {
        ScoreText.text = "Earning : "+Score;
    }
}
