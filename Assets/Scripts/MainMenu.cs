using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject mainButtons;
    [SerializeField] GameObject popupBox;
    [SerializeField] GameObject options;
    [SerializeField] GameObject credits;
    [SerializeField] GameObject firstPlay;

    private void Awake()
    {
        popupBox.SetActive(false);
        //options.SetActive(false);
        credits.SetActive(false);
        firstPlay.SetActive(false);
    }

    private void Start()
    {
        if (FindObjectOfType<DiscordController>() != null)
            FindObjectOfType<DiscordController>().UpdateRichPresence("Staring at the stars", "In Main Menu");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !mainButtons.activeSelf)
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
