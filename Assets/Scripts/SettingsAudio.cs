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

    private void Update()
    {
        audioController.UpdateMusic(musicSlider.value);
        audioController.UpdateSfx(sfxSlider.value);

        // update sliders' values
        musicVal.text = musicSlider.value.ToString();
        sfxVal.text = sfxSlider.value.ToString();
    }
}