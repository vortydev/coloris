using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOver : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] GameObject background;
    [SerializeField] GameObject gameOverPage;
    [SerializeField] GameObject scoreUI;
    [SerializeField] GameObject nextPieceUI;
    [SerializeField] GameObject pauseButton;

    [Header("Scripts")]
    [SerializeField] TracksManager tracksManager;
    [SerializeField] Spawner spawner;
    [SerializeField] Score score;
    [SerializeField] PauseMenu pauseMenu;

    [Header("Game Over")]
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI detailsText;
    [SerializeField] RadioUI radio;

    private void Awake()
    {
        // game over popup
        gameOverPage.SetActive(false);
        detailsText.gameObject.SetActive(false);
    }

    public void GameOverRoutine()
    {
        // load texts
        scoreText.text = "Score: " + score.linesCleared;
        detailsText.text = "Difficulty: " + score.GetDifficultyString()
                        + "\nNext Piece: " + PlayerPrefsManager.GetBoolPlayerPref(PlayerPrefsManager.nextPieceKEY)
                        //+ "\Held Piece: " + PlayerPrefsManager.GetBoolPlayerPref(PlayerPrefsManager.heldPieceKEY)
                        + "\nHeld Piece: Coming Soon"; // temp
        radio.trackName.text = "Tracks played:";
        radio.trackAuthor.text = tracksManager.tracksPlayed.ToString();

        // disable game elements
        tracksManager.StopMusic();
        tracksManager.enabled = false;
        spawner.enabled = false;
        pauseMenu.enabled = false;

        // disable UI
        pauseButton.SetActive(false);
        scoreUI.SetActive(false);
        nextPieceUI.SetActive(false);

        // enable game over UI
        background.SetActive(true);
        gameOverPage.SetActive(true);
    }

    public void ToggleDetails()
    {
        detailsText.gameObject.SetActive(!detailsText.gameObject.activeSelf);
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