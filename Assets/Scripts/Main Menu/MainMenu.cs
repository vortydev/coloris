/*
 * File:        MainMenu.cs
 * Author:      Étienne Ménard
 * Description: Handles the base of the main menu.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private MyControls _actions;

    [SerializeField] GameObject mainButtons;
    [SerializeField] GameObject popupBox;
    [SerializeField] GameObject firstPlay;
    [SerializeField] GameObject[] menuPages;

    private void Awake()
    {
        _actions = new MyControls();
        FindObjectOfType<UISFX>().LoadUIElements();
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
        firstPlay.SetActive(false);

        for (int i = 1; i < menuPages.Length; i++)
        {
            menuPages[i].SetActive(false);
        }

        if (FindObjectOfType<MenuSoundtrack>().toggled)
        {
            FindObjectOfType<MenuSoundtrack>().StartSoundtrack(true);
        }

        MainMenuRichPresence();     // updates Discord Rich Presence

        mainButtons.GetComponentInChildren<Button>().Select();
    }

    public void MainMenuRichPresence()
    {
        if (FindObjectOfType<DiscordController>() != null)
            FindObjectOfType<DiscordController>().UpdateRichPresence("Staring at the stars", "", "Main Menu", PlayerPrefsManager.GetIntPlayerPref(PlayerPrefsManager.cellFaceKEY, 0));
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
        FindObjectOfType<MenuSoundtrack>().StopSoundtrack();
        SceneManager.LoadScene(2); // Loads Coloris
    }

    public void OpenChillZone()
    {
        FindObjectOfType<MenuSoundtrack>().StopSoundtrack();
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

    public void OnClickCredits()
    {
        FindObjectOfType<UISFX>().TetrisSFX();
    }

    public void QuitGame()
    {
        FindObjectOfType<UISFX>().HardDropSFX();
        Application.Quit();
    }

    public void ClosePopup()
    {
        mainButtons.SetActive(true);
        popupBox.SetActive(false);
        
        for (int i = 0; i < menuPages.Length - 1; i++)
        {
            if (menuPages[i].activeSelf)
            {
                menuPages[i].SetActive(false);
            }
        }

        FindObjectOfType<UISFX>().OnButtonClick();
    }

    public void BackButton()
    {
        ClosePopup();
    }

    public void Donate()
    {
        Application.OpenURL("https://paypal.me/etiennemenard");
    }

    public void DiscordServerInvite()
    {
        Application.OpenURL("https://discord.gg/pENaNRFk5p");
    }
}
