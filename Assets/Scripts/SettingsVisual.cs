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

    [Header("Screenshake")]
    [SerializeField] Screenshake screenshake;
    [SerializeField] Toggle shakeToggle;
    [SerializeField] GameObject shakeIntensity;
    private Slider shakeSlider;
    private TextMeshProUGUI shakeValue;

    [Header("Type Writer")]
    [SerializeField] TypeWriter typeWriter;
    [SerializeField] Toggle dynamicText;
    [SerializeField] GameObject textSpeed;
    private Slider textSpeedSlider;
    private TextMeshProUGUI textSpeedValue;

    private void Awake()
    {
        // gets components
        shakeSlider = shakeIntensity.GetComponentInChildren<Slider>();
        shakeValue = shakeIntensity.GetComponentInChildren<TextMeshProUGUI>();

        textSpeedSlider = textSpeed.GetComponentInChildren<Slider>();
        textSpeedValue = textSpeed.GetComponentInChildren<TextMeshProUGUI>();

        if (PlayerPrefsManager.GetIntPlayerPref(PlayerPrefsManager.gridKEY, 1) == 0)
        {
            grid.SetActive(false);
            gridToggle.SetIsOnWithoutNotify(false);
        }

        if (PlayerPrefsManager.GetIntPlayerPref(PlayerPrefsManager.screenshakeKEY, 1) == 0)
        {
            screenshake.enabled = false;
            shakeToggle.SetIsOnWithoutNotify(false);
            shakeIntensity.SetActive(false);
        }

        if (PlayerPrefsManager.GetIntPlayerPref(PlayerPrefsManager.textSpeedKEY, 1) == 0)
        {
            dynamicText.SetIsOnWithoutNotify(false);
            textSpeed.SetActive(false);
        }
    }

    private void Start()
    {
        shakeSlider.value = screenshake.shakeMagnitude * 10;
        textSpeedSlider.value = typeWriter.textSpeed;
    }

    private void Update()
    {
        // screenshake
        screenshake.UpdateShakeMagnitude(shakeSlider.value / 10);
        shakeValue.text = shakeSlider.value.ToString();

        // text speed
        UpdateTextSpeed((int)textSpeedSlider.value);
    }

    public void ToggleGrid()
    {
        grid.SetActive(!grid.activeSelf);
        PlayerPrefsManager.ToggleBoolPlayerPref(PlayerPrefsManager.gridKEY);
    }

    public void ToggleScreenshake()
    {
        screenshake.enabled = !screenshake.enabled;
        shakeIntensity.SetActive(!shakeIntensity.activeSelf);
        PlayerPrefsManager.ToggleBoolPlayerPref(PlayerPrefsManager.screenshakeKEY);
    }

    public void ToggleDynamicText()
    {
        textSpeed.SetActive(!textSpeed.activeSelf);
        typeWriter.dynamicText = !typeWriter.dynamicText;
        PlayerPrefsManager.ToggleBoolPlayerPref(PlayerPrefsManager.textSpeedKEY);
    }

    private void UpdateTextSpeed(int s)
    {
        if (typeWriter.textSpeed != s)
        {
            typeWriter.textSpeed = s;
            typeWriter.UpdateTypingDelay(s);
            PlayerPrefsManager.SaveIntPlayerPref(PlayerPrefsManager.textSpeedKEY, s);
        }

        switch (typeWriter.textSpeed)
        {
            case 1:
                textSpeedValue.text = "Slow";
                break;
            case 2:
                textSpeedValue.text = "Default";
                break;
            case 3:
                textSpeedValue.text = "Fast";
                break;
        }
    }

}