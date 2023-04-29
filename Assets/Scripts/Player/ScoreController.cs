using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreText;

    private int score = 0;

    private void UpdateScoreText()
    {
        scoreText.text = "Score : " + score;
    }

    public int GetScore()
    { 
        return score;
    }

    public void AddScore(int value)
    {
        score += value;

        UpdateScoreText();
    }
}
