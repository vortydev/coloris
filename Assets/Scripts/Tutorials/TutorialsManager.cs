/*
 * File:        TutorialsManager.cs
 * Author:      Étienne Ménard
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
    private int loadout;

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

        loadout = PlayerPrefsManager.GetIntPlayerPref(PlayerPrefsManager.keyboardLoadoutKEY, 0);
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
        pageTitle.text = "What is Coloris?";
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
        string s = ControlsString(ind);

        radio.TypeControlsText(radioText, s);
    }

    private string ControlsString(int ind)
    {
        // excuse this hard-coded mess but I just want this to look nice aight ._.
        switch (ind)
        {
            case 1: // lateral movement
                switch (loadout)
                {
                    case 1:     // vim
                        return "Use H and L to move the piece laterally.";
                    case 2:     // gamer
                        return "Use A and D to move the piece laterally.";
                    default:    // default
                        return "Use the right and left arrows to move the piece laterally.";
                }

            case 2: // rotations
                switch (loadout)
                {
                    case 1:     // vim
                        return "K or X: clockwise rotation. Z: counter-clockwise rotation.";
                    case 2:     // gamer
                        return "W: clockwise rotation. Real gamers don't counter-clockwise.";
                    default:    // default
                        return "Up arrow or X: clockwise rotation. Ctrl or Z: counter-clockwise rotation.";
                }

            case 3: // dropping
                switch (loadout)
                {
                    case 1:     // vim
                        return "Use J to soft drop. Use Space to hard drop.";
                    case 2:     // gamer
                        return "Use S to soft drop. Use Space to hard drop.";
                    default:    // default
                        return "Use the down arrow to soft drop. Use Space to hard drop.";
                }

            case 4: // hold piece
                switch (loadout)
                {
                    case 1:     // vim
                        return "Use C to hold the current piece.";
                    case 2:     // gamer
                        return "Use E to hold the current piece.";
                    default:    // default
                        return "Use Shift or C to hold the current piece.";
                }
        }
        return null;
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
