using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsVisual : MonoBehaviour
{
    [Header("Grid")]
    [SerializeField] GameObject grid;
    [SerializeField] Toggle gridToggle;

    [Header("Audio Visualiser")]
    [SerializeField] GameObject visualiser;
    [SerializeField] Toggle visualiserToggle;

    [Header("Screenshake")]
    [SerializeField] Screenshake screenshake;
    [SerializeField] Toggle shakeToggle;
    [SerializeField] GameObject shakeIntensity;
    private Slider shakeSlider;
    private TextMeshProUGUI shakeValue;

    private void Awake()
    {
        // gets components
        shakeSlider = shakeIntensity.GetComponentInChildren<Slider>();
        shakeValue = shakeIntensity.GetComponentInChildren<TextMeshProUGUI>();

        if (PlayerPrefs.GetInt("game_grid") == 0)
        {
            grid.SetActive(false);
            gridToggle.SetIsOnWithoutNotify(false);
        }

        if (PlayerPrefs.GetInt("visualiser") == 0)
        {
            visualiser.SetActive(false);
            visualiserToggle.SetIsOnWithoutNotify(false);
        }

        if (PlayerPrefs.GetInt("screenshake") == 0)
        {
            screenshake.enabled = false;
            shakeToggle.SetIsOnWithoutNotify(false);
            shakeIntensity.SetActive(false);
        }
    }

    private void Start()
    {
        shakeSlider.value = screenshake.shakeMagnitude * 10;
    }

    private void Update()
    {
        // screenshake
        screenshake.UpdateShakeMagnitude(shakeSlider.value / 10);
        shakeValue.text = shakeSlider.value.ToString();
    }

    public void ToggleGrid()
    {
        grid.SetActive(!grid.activeSelf);
        SettingsManager.ToggleBoolPlayerPrefs("game_grid");
    }

    public void ToggleAudioVisualiser()
    {
        visualiser.SetActive(!visualiser.activeSelf);
        SettingsManager.ToggleBoolPlayerPrefs("visualiser");
    }

    public void ToggleScreenshake()
    {
        screenshake.enabled = !screenshake.enabled;
        shakeIntensity.SetActive(!shakeIntensity.activeSelf);
        SettingsManager.ToggleBoolPlayerPrefs("screenshake");
    }
}