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

    private void Awake()
    {
        popupBox.SetActive(false);
        //options.SetActive(false);
        credits.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !mainButtons.activeSelf)
        {
            ClosePopup();
        }
    }

    public void PlayColoris()
    {
        SceneManager.LoadScene(1); // Loads scene 1 with the actual game
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
}
