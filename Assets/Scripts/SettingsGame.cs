using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsGame : MonoBehaviour
{
    [Header("Difficulty")]
    [SerializeField] Score score;
    [SerializeField] Slider difficultyLevelSlider;

    [Header("Score")]
    [SerializeField] GameObject scoreUI;
    [SerializeField] Toggle scoreToggle;

    [Header("Next Piece")]
    [SerializeField] GameObject nextPieceUI;
    [SerializeField] Toggle nextPieceToggle;

    //[Header("Held Piece")]
    //[SerializeField] GameObject heldPieceUI;
    //[SerializeField] Toggle heldPieceToggle;

    //[Header("Ghost Piece")]
    //[SerializeField] GameObject ghostPieceUI;
    //[SerializeField] Toggle ghostPieceToggle;

    private void Awake()
    {
        if (PlayerPrefsManager.GetIntPlayerPref(PlayerPrefsManager.scoreKEY, 1) == 0)
        {
            scoreUI.SetActive(false);
            scoreToggle.SetIsOnWithoutNotify(false);
        }

        if (PlayerPrefsManager.GetIntPlayerPref(PlayerPrefsManager.nextPieceKEY, 1) == 0)
        {
            nextPieceUI.SetActive(false);
            nextPieceToggle.SetIsOnWithoutNotify(false);
        }
    }

    private void Start()
    {
        difficultyLevelSlider.value = score.difficultyLevel;
    }

    private void Update()
    {
        score.UpdateDifficultyLevel((int)difficultyLevelSlider.value);
    }

    public void ToggleScore()
    {
        scoreUI.SetActive(!scoreUI.activeSelf);
        PlayerPrefsManager.ToggleBoolPlayerPref(PlayerPrefsManager.scoreKEY);
    }

    public void ToggleNextPiece()
    {
        nextPieceUI.SetActive(!nextPieceUI.activeSelf);
        PlayerPrefsManager.ToggleBoolPlayerPref(PlayerPrefsManager.nextPieceKEY);
    }
}