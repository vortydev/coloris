using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] Text text;
    public int points = 0;
    public int difficulty = 0;

    public void IncrementScore()
    {
        points++;
        if (points % 10 == 0) IncrementDifficulty();
        text.text = "Lines Cleared\n" + points;
    }

    public void IncrementDifficulty()
    {
        if (difficulty < 9)
        {
            difficulty++;
        }
    }

    public int ReturnDifficulty()
    {
        return difficulty;
    }
}
