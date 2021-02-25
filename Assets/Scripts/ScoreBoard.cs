using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    public int score;
    public int highScore;
    TextMeshProUGUI scoreText;

    void Start()
    {
        score = 0;

        scoreText = GetComponent<TextMeshProUGUI>();
        scoreText.text = score.ToString();
        LoadHighScore();
    }

    private void LoadHighScore()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        if (data != null)
        {
            highScore = data.highScore;
        }
    }

    public void AddScore(int scoreIncrease)
    {
        score += scoreIncrease;
        scoreText.text = score.ToString();
    }
    public int GetScore()
    {
        return score;
    }
}
