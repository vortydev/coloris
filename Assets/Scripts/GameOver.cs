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

    [Header("Game Over")]
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI detailsText;
    [SerializeField] GameObject detailsScrollview;
    [SerializeField] RadioUI radio;
    private int highscore;

    private void Awake()
    {
        highscore = PlayerPrefsManager.GetIntPlayerPref(PlayerPrefsManager.highscoreKEY, 0);

        // game over popup
        gameOverPage.SetActive(false);
        detailsScrollview.SetActive(false);
    }

    public void GameOverRoutine()
    {
        DeletePieces(); // clears the remaining pieces

        if (score.linesCleared > highscore) // if the current score is higher than the highscore, updates and saves it
        {
            highscore = score.linesCleared;
            PlayerPrefsManager.SaveIntPlayerPref(PlayerPrefsManager.highscoreKEY, highscore);
        }

        // load texts
        scoreText.text = "Score: " + score.linesCleared
                        + "\nHighscore: " + highscore;

        detailsText.text = "Difficulty: " + score.GetDifficultyString()
                        + "\nHard Dropping: " + PlayerPrefsManager.GetBoolStringPlayerPref(PlayerPrefsManager.hardDropKEY)
                        + "\nNext Piece: " + PlayerPrefsManager.GetBoolStringPlayerPref(PlayerPrefsManager.nextPieceKEY)
                        + "\nHold Piece: " + PlayerPrefsManager.GetBoolStringPlayerPref(PlayerPrefsManager.holdPieceKEY);

        tracksManager.gameStarted = false;
        tracksManager.musicSource.volume /= 10;

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
        startGame.countdown.enabled = true;
        startGame.GameOverRestart();

        spawner.enabled = true;
        spawner.RegenBags();

        scoreUI.SetActive(true);
        score.ResetScore();

        nextPieceUI.SetActive(true);
        nextPieceUI.GetComponent<NextPiece>().ResetNextPiece();

        holdPieceUI.SetActive(true);
        holdPieceUI.GetComponent<HoldPiece>().ResetHeldPiece();

        background.SetActive(false);
        gameOverPage.SetActive(false);
    }

    public void QuitToMainMenu()
    {
        SceneManager.LoadScene(0);
        Debug.Log("Quit to main menu");
    }
}
