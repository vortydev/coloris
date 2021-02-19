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

    public void Awake()
    {
        background.SetActive(false);
        pausePage.SetActive(false);
        optionsPage.SetActive(false);
    }

    public void Update()
    {
        // manages opening and closing the pause menu (press Esc)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnClickPause();
        }
    }

    private void OnPauseMenu()
    {
        gamePaused = true;

        background.SetActive(true);
        nextPiece.SetActive(false);
        holdPiece.SetActive(false);

        pausePage.SetActive(true);
        optionsPage.SetActive(false);
    }

    private void OnUnpauseMenu()
    {
        gamePaused = false;

        background.SetActive(false);
        nextPiece.SetActive(true);
        holdPiece.SetActive(true);

        pausePage.SetActive(false);
        optionsPage.SetActive(false);

        FindObjectOfType<Group>().ResetFallCounter();
    }

    public void OnClickPause()
    {
        if (gamePaused)
            OnUnpauseMenu();
        else
            OnPauseMenu();
    }

    // Loads scene #0, which is the main menu
    public void QuitToMainMenu()
    {
        SceneManager.LoadScene(0);
        Debug.Log("Quit to main menu");
    }
}
