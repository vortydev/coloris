using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool gamePaused = false;
    [SerializeField] GameObject popup;

    [Header("Pause menu pages")]
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject settingsMenu;

    [Header("Components")]
    [SerializeField] GameObject nextPieces;
    //[SerializeField] GameObject heldPiece;

    public void Start()
    {
        popup.gameObject.SetActive(false);          // sets the menu to inactive
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

        popup.gameObject.SetActive(true);
        nextPieces.gameObject.SetActive(false);

        if (!pauseMenu.activeSelf)
        {
            settingsMenu.SetActive(false);
            pauseMenu.SetActive(true);
        }
    }

    private void OnUnpauseMenu()
    {
        gamePaused = false;

        popup.gameObject.SetActive(false);
        nextPieces.gameObject.SetActive(true);

        FindObjectOfType<Group>().ResetFallCounter();
    }

    public void OnClickPause()
    {
        if (gamePaused)
            OnUnpauseMenu();
        else
            OnPauseMenu();
    }

    public void OnClickSettings()
    {

    }

    // Loads scene #0, which is the main menu
    public void QuitToMainMenu()
    {
        SceneManager.LoadScene(0);
        Debug.Log("Quit to main menu");
    }
}
