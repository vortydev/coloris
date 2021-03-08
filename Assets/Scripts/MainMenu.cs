/*
 * File:        MainMenu.cs
 * Author:      Étienne Ménard
 * Description: Handles the base of the main menu.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private MyControls _actions;

    [SerializeField] GameObject mainButtons;
    [SerializeField] GameObject popupBox;
    [SerializeField] GameObject options;
    [SerializeField] GameObject credits;
    [SerializeField] GameObject firstPlay;

    private void Awake()
    {
        _actions = new MyControls();
    }

    private void OnEnable()
    {
        // enable the input
        _actions.Enable();

        // pause
        _actions.Coloris.Pause.performed += Pause;
    }

    private void OnDisable()
    {
        // enable the input
        _actions.Disable();

        // pause
        _actions.Coloris.Pause.performed -= Pause;
    }

    private void Start()
    {
        popupBox.SetActive(false);
        //options.SetActive(false);
        credits.SetActive(false);
        firstPlay.SetActive(false);

        MainMenuRichPresence();     // updates Discord Rich Presence
    }

    public void MainMenuRichPresence()
    {
        if (FindObjectOfType<DiscordController>() != null)
            FindObjectOfType<DiscordController>().UpdateRichPresence("Staring at the stars", "In Main Menu", PlayerPrefsManager.GetBoolPlayerPref(PlayerPrefsManager.flushedKEY));
    }

    private void Pause(InputAction.CallbackContext obj)
    {
        if (!mainButtons.activeSelf)
        {
            ClosePopup();
        }
    }

    public void OpenTutorial()
    {
        SceneManager.LoadScene(1);  // loads tutorial
    }

    public void PlayColoris()
    {
        SceneManager.LoadScene(2); // Loads Coloris
    }

    public void OpenChillZone()
    {
        SceneManager.LoadScene(3);  // loads the Chill Zone
    }

    public void CheckFirstPlay()
    {
        if (PlayerPrefsManager.GetIntPlayerPref(PlayerPrefsManager.firstPlayKEY, 1) == 0)
        {
            PlayerPrefsManager.SaveIntPlayerPref(PlayerPrefsManager.firstPlayKEY, 0);
            PlayColoris();
        }
        else
        {
            mainButtons.SetActive(false);
            firstPlay.SetActive(true);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ClosePopup()
    {
        mainButtons.SetActive(true);
        popupBox.SetActive(false);
        options.SetActive(false);
        credits.SetActive(false);

        FindObjectOfType<UISFX>().ClickButton();
    }

    public void BackButton()
    {
        ClosePopup();
    }

    public void Donate()
    {
        Application.OpenURL("https://paypal.me/etiennemenard");
    }
}
