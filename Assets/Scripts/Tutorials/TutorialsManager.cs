/*
 * File:        TutorialsManager.cs
 * Author:      �tienne M�nard
 * Description: Manages the tutorials and the more general stuff of the scene.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class TutorialsManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] GameObject mainButtons;
    [SerializeField] TextMeshProUGUI pageTitle;
    [SerializeField] GameObject holdPieceUI;
    [SerializeField] TypeWriter radio;
    public TextMeshProUGUI radioText;
    public TextMeshProUGUI bodyText;

    [Header("Page")]
    [SerializeField] GameObject introPage;
    [SerializeField] GameObject controlsPage;

    [Header("Transcripts")]
    [SerializeField] string[] mainPageTrans;
    [SerializeField] string[] introTrans;
    [SerializeField] string[] contTrans;

    [Header("Audio")]
    private AudioController _audioController;
    [SerializeField] Slider musicSlider;
    [SerializeField] TextMeshProUGUI musicVal;
    [SerializeField] Slider sfxSlider;
    [SerializeField] TextMeshProUGUI sfxVal;

    private void Awake()
    {
        _audioController = FindObjectOfType<AudioController>();
        FindObjectOfType<UISFX>().LoadUIElements();
    }

    private void Start()
    {
        introPage.SetActive(false);
        controlsPage.SetActive(false);

        radio.TypeText(radioText, mainPageTrans[Random.Range(0, mainPageTrans.Length)]);

        musicSlider.value = _audioController.music;
        sfxSlider.value = _audioController.sfx;

        if (FindObjectOfType<DiscordController>() != null)
            FindObjectOfType<DiscordController>().UpdateRichPresence("Learning the ropes", "", "Tutorials", PlayerPrefsManager.GetIntPlayerPref(PlayerPrefsManager.cellFaceKEY, 0));
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

        radio.TypeText(radioText, mainPageTrans[Random.Range(0, mainPageTrans.Length)]);
    }

    public void OpenIntroduction()
    {
        pageTitle.text = "Introduction";
        pageTitle.fontStyle = FontStyles.Normal;

        introPage.SetActive(true);
        mainButtons.SetActive(false);

        PlayIntroduction();
    }

    public void OpenControls()
    {
        pageTitle.text = "Controls";
        pageTitle.fontStyle = FontStyles.Normal;

        mainButtons.SetActive(false);
        controlsPage.SetActive(true);

        FindObjectOfType<TutorialControls>().UpdateControlsPage();
    }

    private void PlayIntroduction()
    {
        radio.TypeText(radioText, introTrans[0]);
        radio.TypeIntroText(bodyText, introTrans[1]);
    }

    public void PlayControls(int ind)
    {
        radio.TypeControlsText(radioText, contTrans[ind - 1]);
    }

    public void UpdateSliderMusic()
    {
        _audioController.UpdateMusic(musicSlider.value);
        musicVal.text = musicSlider.value.ToString();
    }

    public void UpdateSliderSfx()
    {
        _audioController.UpdateSfx(sfxSlider.value);
        sfxVal.text = sfxSlider.value.ToString();
    }

    public void MainMenu()
    {
        PlayerPrefsManager.SaveIntPlayerPref(PlayerPrefsManager.firstPlayKEY, 0);
        SceneManager.LoadScene(0);
    }
}