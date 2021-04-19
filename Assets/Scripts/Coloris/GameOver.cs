/*
 * File:        GameOver.cs
 * Author:      Étienne Ménard
 * Description: Handles the Game Over state of the game.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOver : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] GameObject background;
    [SerializeField] GameObject gameOverPage;
    [SerializeField] GameObject scoreUI;
    [SerializeField] GameObject nextPieceUI;
    [SerializeField] GameObject holdPieceUI;
    [SerializeField] GameObject pauseButton;

    [Header("Scripts")]
    [SerializeField] TracksManager tracksManager;
    [SerializeField] Spawner spawner;
    [SerializeField] Score score;
    [SerializeField] PauseMenu pauseMenu;
    [SerializeField] StartGame startGame;
    [SerializeField] GameplayController gameplayController;

    [Header("Game Over")]
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI detailsText;
    [SerializeField] GameObject detailsScrollview;
    [SerializeField] RadioUI radio;
    private int _highscore;

    private void Awake()
    {
        _highscore = PlayerPrefsManager.GetIntPlayerPref(PlayerPrefsManager.highscoreKEY, 0);
    }

    private void Start()
    {
        // game over popup
        gameOverPage.SetActive(false);
        detailsScrollview.SetActive(false);
    }

    public void GameOverRoutine()
    {
        DeletePieces(); // clears the remaining pieces

        if (score.linesCleared > _highscore) // if the current score is higher than the highscore, updates and saves it
        {
            _highscore = score.linesCleared;
            PlayerPrefsManager.SaveIntPlayerPref(PlayerPrefsManager.highscoreKEY, _highscore);
        }

        // load texts
        scoreText.text = "Score: " + score.linesCleared
                        + "\nHighscore: " + _highscore;

        detailsText.text = "Difficulty: " + score.GetDifficultyString()
                        + "\nLock Delay: " + GetLockDelayString()
                        + "\nHard Dropping: " + PlayerPrefsManager.GetBoolStringPlayerPref(PlayerPrefsManager.hardDropKEY)
                        + "\nNext Piece: " + PlayerPrefsManager.GetBoolStringPlayerPref(PlayerPrefsManager.nextPieceKEY)
                        + "\nHold Piece: " + PlayerPrefsManager.GetBoolStringPlayerPref(PlayerPrefsManager.holdPieceKEY)
                        + "\nGhost Piece: " + PlayerPrefsManager.GetBoolStringPlayerPref(PlayerPrefsManager.ghostPieceKEY);

        tracksManager.gameStarted = false;
        tracksManager.audioController.FadeOutMusic(tracksManager.audioController.music, tracksManager.audioController.music / 10f, 3f);

        // disable game elements
        spawner.enabled = false;
        pauseMenu.enabled = false;

        // disable game UI
        pauseButton.SetActive(false);
        scoreUI.SetActive(false);
        nextPieceUI.SetActive(false);
        holdPieceUI.SetActive(false);

        // enable game over UI
        background.SetActive(true);
        gameOverPage.SetActive(true);
    }

    // goes through all the remaining pieces and deletes them
    private void DeletePieces()
    {
        GameObject[] oldPieces;
        oldPieces = GameObject.FindGameObjectsWithTag("Piece");

        GameObject[] oldSquarePieces;
        oldSquarePieces = GameObject.FindGameObjectsWithTag("SquarePiece");

        foreach (GameObject oldPiece in oldPieces)
        {
            Destroy(oldPiece);
        }

        foreach (GameObject oldSquare in oldSquarePieces)
        {
            Destroy(oldSquare);
        }
    }

    public void ToggleDetails()
    {
        detailsScrollview.SetActive(!detailsScrollview.activeSelf);
    }

    // re-enables all the stuff to start a new game
    public void Replay()
    {
        startGame.countdownText.enabled = true;
        startGame.GameOverRestartMusicFade();

        spawner.enabled = true;
        spawner.RegenBags();

        score.ResetScore();
        if (PlayerPrefsManager.GetBoolPlayerPref(PlayerPrefsManager.scoreKEY))
            scoreUI.SetActive(true);

        nextPieceUI.GetComponent<NextPiece>().ResetNextPiece();
        if (PlayerPrefsManager.GetBoolPlayerPref(PlayerPrefsManager.nextPieceKEY))
            nextPieceUI.SetActive(true);

        holdPieceUI.GetComponent<HoldPiece>().ResetHeldPiece();
        if (PlayerPrefsManager.GetBoolPlayerPref(PlayerPrefsManager.holdPieceKEY))
            holdPieceUI.SetActive(true);

        background.SetActive(false);
        gameOverPage.SetActive(false);
    }

    public string GetLockDelayString()
    {
        switch (gameplayController.lockDelay)
        {
            case 0:
                return "0s";
            case 1:
                return "0.1s";
            case 2:
                return "0.2s";
            case 3:
                return "0.3s";
            case 4:
                return "0.4s";
            case 5:
                return "0.5s";
        }

        return null;
    }

    public void QuitToMainMenu()
    {
        SceneManager.LoadScene(0);
        Debug.Log("Quit to main menu");
    }
}
