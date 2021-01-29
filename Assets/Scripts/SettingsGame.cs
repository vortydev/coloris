using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsGame : MonoBehaviour
{
    //[Header("Difficulty")]
    //[SerializeField] Score score;
    //[SerializeField] GameObject maxDifficulty;
    //private Toggle maxDifficultyToggle;
    //private InputField maxDifficultyInput;

    [Header("Score")]
    [SerializeField] GameObject scoreUI;
    [SerializeField] Toggle scoreToggle;

    [Header("Next Piece")]
    [SerializeField] GameObject nextPieceUI;
    [SerializeField] Toggle nextPieceToggle;

    //[Header("Held Piece")]
    //[SerializeField] Image heldPieceUI;
    //[SerializeField] Toggle heldPieceToggle;

    //[Header("Ghost Piece")]
    //[SerializeField] Image ghostPieceUI;
    //[SerializeField] Toggle ghostPieceToggle;
    private void Awake()
    {
        if (PlayerPrefs.GetInt("score") == 0)
        {
            scoreUI.SetActive(false);
            scoreToggle.SetIsOnWithoutNotify(false);
        }

        if (PlayerPrefs.GetInt("next_piece") == 0)
        {
            nextPieceUI.SetActive(false);
            nextPieceToggle.SetIsOnWithoutNotify(false);
        }
    }

    public void ToggleScore()
    {
        scoreUI.SetActive(!scoreUI.activeSelf);
        SettingsManager.ToggleBoolPlayerPrefs("score");
    }

    public void ToggleNextPiece()
    {
        nextPieceUI.SetActive(!nextPieceUI.activeSelf);
        SettingsManager.ToggleBoolPlayerPrefs("next_piece");
    }
}