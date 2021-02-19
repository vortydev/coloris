using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsAudio : MonoBehaviour
{
    [SerializeField] AudioController audioController;

    [Header("Music")]
    [SerializeField] Slider musicSlider;
    [SerializeField] TextMeshProUGUI musicVal;

    [Header("SFX")]
    [SerializeField] Slider sfxSlider;
    [SerializeField] TextMeshProUGUI sfxVal;

    [Header("Speech")]
    [SerializeField] Slider speechSlider;
    [SerializeField] TextMeshProUGUI speechVal;
    [SerializeField] TMP_Dropdown voiceDropdown;

    [Header("Visualiser")]
    [SerializeField] GameObject visualiser;
    [SerializeField] Toggle visualiserToggle;

    private void Awake()
    {
        voiceDropdown.SetValueWithoutNotify(PlayerPrefsManager.GetIntPlayerPref(PlayerPrefsManager.selectedVoiceKEY, 0));

        if (PlayerPrefsManager.GetIntPlayerPref(PlayerPrefsManager.visualiserKEY, 1) == 0)
        {
            visualiser.SetActive(false);
            visualiserToggle.SetIsOnWithoutNotify(false);
        }
    }

    private void Start()
    {
        musicSlider.value = audioController.music;
        sfxSlider.value = audioController.sfx;
        speechSlider.value = audioController.speech;
    }

    private void Update()
    {
        // update audio controller
        audioController.UpdateMusic(musicSlider.value);
        audioController.UpdateSfx(sfxSlider.value);
        audioController.UpdateSpeech(speechSlider.value);

        // update sliders' values
        musicVal.text = musicSlider.value.ToString();
        sfxVal.text = sfxSlider.value.ToString();
        speechVal.text = speechSlider.value.ToString();
    }

    public void UpdateSelectedVoice()
    {
        PlayerPrefsManager.SaveIntPlayerPref(PlayerPrefsManager.selectedVoiceKEY, voiceDropdown.value);
    }

    public void ToggleAudioVisualiser()
    {
        visualiser.SetActive(!visualiser.activeSelf);
        PlayerPrefsManager.ToggleBoolPlayerPref(PlayerPrefsManager.visualiserKEY);
    }
}