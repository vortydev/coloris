using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] TextMeshProUGUI scoreText;

    [Header("Score Data")]
    public int linesCleared = 0;            // the amount of lines the player has cleared
    public int globalDifficulty = 0;        // the current difficulty of the game
    public int maxDifficulty = 10;          // the maximum difficulty speed
    public int difficultyTreshhold = 10;    // the number of lines to clear before incrementing the difficulty

    public void IncrementScore()
    {
        linesCleared++;
        if (linesCleared % difficultyTreshhold == 0)
            IncrementDifficulty();

        scoreText.text = "Lines Cleared\n" + linesCleared;
    }

    public void IncrementDifficulty()
    {
        if (globalDifficulty < maxDifficulty - 1)
        {
            globalDifficulty++;
        }
    }
}
