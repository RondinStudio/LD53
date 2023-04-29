using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public enum EScoreType
{
    Box
}

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

    public void AddScore(EScoreType type)
    {
        switch (type)
        {
            case EScoreType.Box:
                score += 1;
                break;
        }

        UpdateScoreText();
    }
}
