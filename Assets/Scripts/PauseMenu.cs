/*
 * File:        PauseMenu.cs
 * Author:      Étienne Ménard
 * Description: Script for when the game is paused.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool gamePaused = false;

    [Header("UI")]
    [SerializeField] GameObject background;
    [SerializeField] GameObject nextPiece;
    [SerializeField] GameObject holdPiece;

    [Header("Pause menu pages")]
    [SerializeField] GameObject pausePage;
    [SerializeField] GameObject optionsPage;

    private void Awake()
    {
        FindObjectOfType<UISFX>().LoadUIElements();
    }

    public void Start()
    {
        background.SetActive(false);
        pausePage.SetActive(false);
        optionsPage.SetActive(false);
    }

    private void OnPauseMenu()
    {
        gamePaused = true;
        FindObjectOfType<GameSFX>().PauseSFX();

        background.SetActive(true);
        nextPiece.SetActive(false);
        holdPiece.SetActive(false);

        pausePage.SetActive(true);
        optionsPage.SetActive(false);
    }

    private void OnUnpauseMenu()
    {
        gamePaused = false;
        FindObjectOfType<GameSFX>().UnpauseSFX();

        background.SetActive(false);
        nextPiece.SetActive(true);
        holdPiece.SetActive(true);

        pausePage.SetActive(false);
        optionsPage.SetActive(false);

        FindObjectOfType<Group>().ResetFallCounter();
    }

    public void OnClickPause()
    {
        if (FindObjectOfType<TracksManager>().gameStarted)
        {
            if (gamePaused)
                OnUnpauseMenu();
            else
                OnPauseMenu();
        }
    }

    // Loads scene #0, which is the main menu
    public void QuitToMainMenu()
    {
        FindObjectOfType<AudioController>().KillAudio();
        SceneManager.LoadScene(0);
    }
}
