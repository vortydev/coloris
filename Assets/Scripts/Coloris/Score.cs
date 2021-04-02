/*
 * File:        Score.cs
 * Author:      Étienne Ménard
 * Description: Handles the score counted in lines cleared, the difficulty threshhold, and old piece deletion.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] TextMeshProUGUI scoreText;

    [Header("Score")]
    public int linesCleared = 0;        // the amount of lines the player has cleared
    public int difficultyLevel;         // the difficulty level for the game

    [Header("Difficulty")]
    public int globalDifficulty = 0;    // the current difficulty of the game        
    public int difficultyTreshhold;     // the number of lines to clear before incrementing the difficulty

    private void Awake()
    {
        difficultyLevel = PlayerPrefsManager.GetIntPlayerPref(PlayerPrefsManager.difficultyLevelKEY, 1);
        difficultyTreshhold = 12 - (difficultyLevel * 2);
        UpdateDifficultyLevel(difficultyLevel);
    }

    private void Start()
    {
        if (FindObjectOfType<DiscordController>() != null)
            FindObjectOfType<DiscordController>().UpdateRichPresence("Lines cleared: 0", "Difficulty: " + GetDifficultyString(), "Coloris - Classic", FindObjectOfType<CanDo>().cellFace);
    }

    public void IncrementScore()
    {
        linesCleared++; // increment the amount of lines cleared

        if (linesCleared % difficultyTreshhold == 0)    // cheeky math to check when the amount of lines cleared equals the threshold
            IncrementDifficulty();

        scoreText.text = "Lines Cleared\n" + linesCleared;  // update score text

        if (FindObjectOfType<DiscordController>() != null)
            FindObjectOfType<DiscordController>().UpdateRichPresence("Lines cleared: " + linesCleared.ToString(), "Difficulty: " + GetDifficultyString(), "Coloris - Classic", FindObjectOfType<CanDo>().cellFace);

        CleanupOldPieces(); // remove old piece clones
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
        oldPieces = GameObject.FindGameObjectsWithTag("Piece"); // find objects with the "Piece" tag

        foreach (GameObject oldPiece in oldPieces)
        {
            if (oldPiece.transform.childCount == 0) // if the piece has no more cells as children
            {
                Destroy(oldPiece);                  // destroy the object
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
