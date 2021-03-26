/*
 * File:        CZ_MediaControls.cs
 * Author:      Étienne Ménard
 * Description: Handles media controls of the Chill Zone.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CZ_MediaControls : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] ChillZoneManager manager;

    [Header("Sliders")]
    [SerializeField] Slider musicSlider;
    [SerializeField] TextMeshProUGUI musicVal;
    [SerializeField] Slider sfxSlider;
    [SerializeField] TextMeshProUGUI sfxVal;

    [Header("Buttons")]
    [SerializeField] Button restartButton;
    [SerializeField] Button pauseButton;
    private TextMeshProUGUI pauseButtonText;
    [SerializeField] Button skipButton;
    public TextMeshProUGUI skipButtonText;

    [Header("Time")]
    public TextMeshProUGUI time;
    private string min, sec, totalTime;
    private float trackTime = 0;

    private void Start()
    {
        musicSlider.value = manager.audioController.music;
        sfxSlider.value = manager.audioController.sfx;

        pauseButtonText = pauseButton.GetComponentInChildren<TextMeshProUGUI>();
        skipButtonText = skipButton.GetComponentInChildren<TextMeshProUGUI>();

        time.text = "00:00 / 00:00";
    }

    private void Update()
    {
        if (manager.musicSource.isPlaying)
        {
            UpdateTrackTime();
        }
    }

    public void UpdateSliderMusic()
    {
        manager.audioController.UpdateMusic(musicSlider.value);
        musicVal.text = musicSlider.value.ToString();
    }

    public void UpdateSliderSfx()
    {
        manager.audioController.UpdateSfx(sfxSlider.value);
        sfxVal.text = sfxSlider.value.ToString();
    }

    public void ToggleMediaControls(bool state)
    {
        restartButton.interactable = state;
        pauseButton.interactable = state;
        skipButton.interactable = state;
    }

    public void PauseTrack()
    {
        if (manager.paused)
        {
            manager.TogglePause();
            pauseButtonText.text = "Pause";
        }
        else
        {
            manager.TogglePause();
            pauseButtonText.text = "Play";
        }
    }

    public void ResetPauseButton()
    {
        pauseButtonText.text = "Pause";
    }

    private void UpdateTrackTime()
    {
        int curTime = (int)manager.musicSource.time;
        min = (curTime / 60).ToString();
        sec = (curTime % 60).ToString();

        if (curTime / 60 < 10)
            min = "0" + min;

        if (curTime % 60 < 10)
            sec = "0" + sec;

        time.text = min + ":" + sec + " / " + totalTime;
    }

    public void GetTotalTrackTime(float time)
    {
        string min, sec;
        trackTime = time;

        min = ((int)trackTime / 60).ToString();
        if (trackTime / 60 < 10)
            min = "0" + min;

        sec = ((int)manager.musicSource.clip.length % 60).ToString();
        if (trackTime % 60 < 10)
            sec = "0" + sec;

        totalTime = min + ":" + sec;
    }

    public void UpdateSkipButtonText(int playlistSize)
    {
        if (playlistSize > 0)
        {
            skipButtonText.text = "Skip";
        }
        else
        {
            skipButtonText.text = "Stop";
        }
    }
}
