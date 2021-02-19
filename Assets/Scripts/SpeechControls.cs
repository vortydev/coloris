using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpeechControls : MonoBehaviour
{
    [SerializeField] AudioController audioController;
    [SerializeField] VoicelinesManager voicelinesManager;

    [Header("Options")]
    public bool muted;
    [SerializeField] Button muteButton;
    [SerializeField] TextMeshProUGUI muteButtonText;
    [SerializeField] Slider speechSlider;
    [SerializeField] TextMeshProUGUI speechVal;
    private float preMutedVal;
    [SerializeField] TMP_Dropdown voiceDropdown;

    private void Start()
    {
        speechSlider.value = audioController.speech;
        voiceDropdown.SetValueWithoutNotify(voicelinesManager.selectedVoice);
    }

    private void Update()
    {
        if (!muted)
        {
            audioController.UpdateSpeech(speechSlider.value);
            speechVal.text = speechSlider.value.ToString();

            voicelinesManager.audioSource.volume = audioController.speech / 10;
        }
    }

    public void ToggleMute()
    {
        if (muted)
        {
            muted = false;
            muteButtonText.text = "Mute";

            speechSlider.interactable = true;
            speechVal.text = preMutedVal.ToString();
        }
        else
        {
            muted = true;
            muteButtonText.text = "Unmute";

            speechSlider.interactable = false;
            preMutedVal = speechSlider.value;
            speechVal.text = "Muted";
        }
    }

    public void UpdateSelectedVoice()
    {
        if (voiceDropdown.value != voicelinesManager.selectedVoice)
        {
            voicelinesManager.selectedVoice = voiceDropdown.value;
            PlayerPrefsManager.SaveIntPlayerPref(PlayerPrefsManager.selectedVoiceKEY, voicelinesManager.selectedVoice);
        }
    }
}
