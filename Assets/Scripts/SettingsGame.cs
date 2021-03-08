/*
 * File:        SettingsGame.cs
 * Author:      Étienne Ménard
 * Description: In-game game options.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsGame : MonoBehaviour
{
    [Header("Difficulty")]
    [SerializeField] Score score;
    [SerializeField] Slider difficultyLevelSlider;

    [Header("Hard Dropping")]
    [SerializeField] CanDo canDo;
    [SerializeField] Toggle hardDropToggle;

    [Header("Score")]
    [SerializeField] GameObject scoreUI;
    [SerializeField] Toggle scoreToggle;

    [Header("Next Piece")]
    [SerializeField] GameObject nextPieceUI;
    [SerializeField] Toggle nextPieceToggle;

    [Header("Hold Piece")]
    [SerializeField] GameObject holdPieceUI;
    [SerializeField] Toggle holdPieceToggle;

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

        if (PlayerPrefsManager.GetIntPlayerPref(PlayerPrefsManager.holdPieceKEY, 1) == 0)
        {
            canDo.canHold = false;
            holdPieceUI.SetActive(false);
            holdPieceToggle.SetIsOnWithoutNotify(false);
        }

        if (PlayerPrefsManager.GetIntPlayerPref(PlayerPrefsManager.hardDropKEY, 1) == 0)
        {
            canDo.canHardDrop = false;
            hardDropToggle.SetIsOnWithoutNotify(false);
        }
    }

    private void Start()
    {
        difficultyLevelSlider.value = score.difficultyLevel;
    }

    public void UpdateSliderDifficulty()
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

    public void ToggleHoldPiece()
    {
        canDo.canHold = !canDo.canHold;
        holdPieceUI.SetActive(!holdPieceUI.activeSelf);
        PlayerPrefsManager.ToggleBoolPlayerPref(PlayerPrefsManager.holdPieceKEY);
    }

    public void ToggleHardDropping()
    {
        canDo.canHardDrop = !canDo.canHardDrop;
        PlayerPrefsManager.ToggleBoolPlayerPref(PlayerPrefsManager.hardDropKEY);
    }
}