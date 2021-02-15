using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TutorialsManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] VoicelinesManager voicelinesManager;
    [SerializeField] GameObject mainButtons;
    [SerializeField] TextMeshProUGUI pageTitle;
    [SerializeField] GameObject holdPieceUI;

    [Header("Page")]
    [SerializeField] GameObject introPage;
    [SerializeField] GameObject controlsPage;

    private void Start()
    {
        introPage.SetActive(false);
        controlsPage.SetActive(false);

        voicelinesManager.TypeMainMessage();
    }

    public void BackToMain()
    {
        pageTitle.text = "Tutorials";
        pageTitle.fontStyle = FontStyles.Underline;

        mainButtons.SetActive(true);
        introPage.SetActive(false);
        controlsPage.SetActive(false);

        holdPieceUI.SetActive(false);
        holdPieceUI.GetComponentInChildren<Transform>().gameObject.SetActive(false);

        voicelinesManager.TypeMainMessage();
    }

    public void OpenIntroduction()
    {
        pageTitle.text = "Introduction";
        pageTitle.fontStyle = FontStyles.Normal;

        introPage.SetActive(true);
        mainButtons.SetActive(false);
    }

    public void OpenControls()
    {
        pageTitle.text = "Controls";
        pageTitle.fontStyle = FontStyles.Normal;

        mainButtons.SetActive(false);
        controlsPage.SetActive(true);

        FindObjectOfType<TutorialControls>().UpdateControlsPage();
    }

    public void MainMenu()
    {
        PlayerPrefsManager.SaveIntPlayerPref(PlayerPrefsManager.firstPlayKEY, 0);
        SceneManager.LoadScene(0);
    }
}
