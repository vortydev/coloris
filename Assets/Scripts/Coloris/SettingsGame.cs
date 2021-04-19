/*
 * File:        SettingsGame.cs
 * Author:      Étienne Ménard
 * Description: In-game game options.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsGame : MonoBehaviour
{
    private GameplayController _gameplayController;

    [Header("Difficulty")]
    [SerializeField] Score score;
    [SerializeField] TMP_Dropdown difficultyDropdown;

    [Header("Lock Delay")]
    [SerializeField] Slider lockDelaySlider;
    [SerializeField] TextMeshProUGUI lockDelayValue;

    [Header("Hard Dropping")]
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

    [Header("Ghost Piece")]
    [SerializeField] Toggle ghostPieceToggle;

    private void Awake()
    {
        _gameplayController = FindObjectOfType<GameplayController>();

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
            _gameplayController.canHold = false;
            holdPieceUI.SetActive(false);
            holdPieceToggle.SetIsOnWithoutNotify(false);
        }

        if (PlayerPrefsManager.GetIntPlayerPref(PlayerPrefsManager.hardDropKEY, 1) == 0)
        {
            _gameplayController.canHardDrop = false;
            hardDropToggle.SetIsOnWithoutNotify(false);
        }

        if (PlayerPrefsManager.GetIntPlayerPref(PlayerPrefsManager.ghostPieceKEY, 1) == 0)
        {
            _gameplayController.canGhost = false;
            ghostPieceToggle.SetIsOnWithoutNotify(false);
        }
    }

    private void Start()
    {
        difficultyDropdown.value = score.difficultyLevel;
        lockDelaySlider.value = _gameplayController.lockDelay;
        UpdateSliderLockDelay();
    }

    private void UpdateSliderLockDelay()
    {
        switch (_gameplayController.lockDelay)
        {
            case 0:
                lockDelayValue.text = "0s";
                break;
            case 1:
                lockDelayValue.text = "0.1s";
                break;
            case 2:
                lockDelayValue.text = "0.2s";
                break;
            case 3:
                lockDelayValue.text = "0.3s";
                break;
            case 4:
                lockDelayValue.text = "0.4s";
                break;
            case 5:
                lockDelayValue.text = "0.5s";
                break;
        }
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
        _gameplayController.canHold = !_gameplayController.canHold;
        holdPieceUI.SetActive(!holdPieceUI.activeSelf);
        PlayerPrefsManager.ToggleBoolPlayerPref(PlayerPrefsManager.holdPieceKEY);
    }

    public void ToggleHardDropping()
    {
        _gameplayController.canHardDrop = !_gameplayController.canHardDrop;
        PlayerPrefsManager.ToggleBoolPlayerPref(PlayerPrefsManager.hardDropKEY);
    }

    public void ToggleGhostPiece()
    {
        _gameplayController.ToggleCanGhost();
        PlayerPrefsManager.ToggleBoolPlayerPref(PlayerPrefsManager.ghostPieceKEY);
    }
}