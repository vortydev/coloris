using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI difficultyLevelText;

    [Header("Score")]
    public int linesCleared = 0;            // the amount of lines the player has cleared
    public int difficultyLevel;             // the difficulty level

    [Header("Difficulty")]
    public int globalDifficulty = 0;        // the current difficulty of the game
    public int maxDifficulty = 10;          
    public int difficultyTreshhold;    // the number of lines to clear before incrementing the difficulty

    private void Awake()
    {
        difficultyLevel = PlayerPrefsManager.GetIntPlayerPrefs(PlayerPrefsManager.difficultyLevelKEY, 1);
        difficultyTreshhold = 12 - (difficultyLevel * 2);
        UpdateDifficultyLevel(difficultyLevel);
    }

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

    public void UpdateDifficultyLevel(int d)
    {
        if (difficultyLevel != d)
        {
            difficultyLevel = d;
            PlayerPrefsManager.SaveIntPlayerPrefs(PlayerPrefsManager.difficultyLevelKEY, d);
        }

        difficultyTreshhold = 12 - (difficultyLevel * 2);

        switch (difficultyLevel)
        {
            case 0:
                difficultyLevelText.text = "Easy";
                break;
            case 1:
                difficultyLevelText.text = "Normal";
                break;
            case 2:
                difficultyLevelText.text = "Hard";
                break;
            case 3:
                difficultyLevelText.text = "Insane";
                break;
        }
    }
}
