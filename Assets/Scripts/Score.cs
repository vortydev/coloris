using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI difficultyLevelText;

    [Header("Score")]
    public int linesCleared = 0;            // the amount of lines the player has cleared
    public int difficultyLevel;

    [Header("Difficulty")]
    public int globalDifficulty = 0;        // the current difficulty of the game        
    public int difficultyTreshhold;    // the number of lines to clear before incrementing the difficulty

    private void Awake()
    {
        difficultyLevel = PlayerPrefsManager.GetIntPlayerPref(PlayerPrefsManager.difficultyLevelKEY, 1);
        difficultyTreshhold = 12 - (difficultyLevel * 2);
        UpdateDifficultyLevel(difficultyLevel);
    }

    public void IncrementScore(int clearedLines = 1)
    {
        linesCleared++;
        if (linesCleared % difficultyTreshhold == 0)
            IncrementDifficulty();

        scoreText.text = "Lines Cleared\n" + linesCleared;

        CleanupOldPieces();
    }

    private void IncrementDifficulty()
    {
        if (globalDifficulty < 9)
        {
            globalDifficulty++;
        }
    }

    public void UpdateDifficultyLevel(int d)
    {
        if (difficultyLevel != d)
        {
            difficultyLevel = d;
            PlayerPrefsManager.SaveIntPlayerPref(PlayerPrefsManager.difficultyLevelKEY, d);
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

    public string GetDifficultyString()
    {
        switch (difficultyLevel)
        {
            case 0:
                return "Easy";
            case 1:
                return "Normal";
            case 2:
                return "Hard";
            case 3:
                return "Insane";
        }

        return "???";
    }

    private void CleanupOldPieces()
    {
        GameObject[] oldPieces;
        oldPieces = GameObject.FindGameObjectsWithTag("Piece");

        foreach (GameObject oldPiece in oldPieces)
        {
            if (oldPiece.transform.childCount == 0)
            {
                Destroy(oldPiece);
            }
        }

        GameObject[] oldSquarePieces;
        oldSquarePieces = GameObject.FindGameObjectsWithTag("SquarePiece");

        foreach (GameObject oldSquare in oldSquarePieces)
        {
            if (oldSquare.transform.childCount == 0)
            {
                Destroy(oldSquare);
            }
        }
    }

    public void ResetScore()
    {
        linesCleared = 0;
        scoreText.text = "Lines Cleared\n" + linesCleared;

        globalDifficulty = 0;
    }
}
