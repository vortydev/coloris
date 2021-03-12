/*
 * File:        SettingsAudio.cs
 * Author:      Étienne Ménard
 * Description: In-game audio options.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsAudio : MonoBehaviour
{
    private AudioController _audioController;

    [Header("Music")]
    [SerializeField] Slider musicSlider;
    [SerializeField] TextMeshProUGUI musicVal;

    [Header("SFX")]
    [SerializeField] Slider sfxSlider;
    [SerializeField] TextMeshProUGUI sfxVal;

    [Header("Visualiser")]
    [SerializeField] GameObject visualiser;
    [SerializeField] Toggle visualiserToggle;

    private void Awake()
    {
        _audioController = FindObjectOfType<AudioController>();

        if (PlayerPrefsManager.GetIntPlayerPref(PlayerPrefsManager.visualiserKEY, 1) == 0)
        {
            visualiser.SetActive(false);
            visualiserToggle.SetIsOnWithoutNotify(false);
        }
    }

    private void Start()
    {
        musicSlider.value = _audioController.music;
        sfxSlider.value = _audioController.sfx;
    }

    public void UpdateMusicSlider()
    {
        _audioController.UpdateMusic(musicSlider.value);
        musicVal.text = musicSlider.value.ToString();
    }

    public void UpdateSfxSlider()
    {
        _audioController.UpdateSfx(sfxSlider.value);
        sfxVal.text = sfxSlider.value.ToString();
    }

    public void ToggleAudioVisualiser()
    {
        visualiser.SetActive(!visualiser.activeSelf);
        PlayerPrefsManager.ToggleBoolPlayerPref(PlayerPrefsManager.visualiserKEY);
    }
}