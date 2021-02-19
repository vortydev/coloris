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
        DeletePieces();

        if (score.linesCleared > highscore)
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

        radio.trackName.text = "Tracks played:";
        radio.trackAuthor.text = tracksManager.tracksPlayed.ToString();

        // disable game elements
        tracksManager.StopMusic();
        tracksManager.enabled = false;
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

    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitToMainMenu()
    {
        SceneManager.LoadScene(0);
        Debug.Log("Quit to main menu");
    }
}
