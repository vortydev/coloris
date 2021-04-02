/*
 * File:        SettingsVisual.cs
 * Author:      Étienne Ménard
 * Description: In-game visual options.
 */

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
    [SerializeField] TMP_Dropdown textSpeedDropdown;
    [SerializeField] GameObject textSpeedObject;

    private void Awake()
    {
        // gets components
        shakeSlider = shakeIntensity.GetComponentInChildren<Slider>();
        shakeValue = shakeIntensity.GetComponentInChildren<TextMeshProUGUI>();

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

        if (PlayerPrefsManager.GetIntPlayerPref(PlayerPrefsManager.dynamicTextKEY, 1) == 0)
        {
            dynamicText.SetIsOnWithoutNotify(false);
            textSpeedObject.SetActive(false);
        }
    }

    private void Start()
    {
        shakeSlider.value = screenshake.shakeMagnitude * 10;
        textSpeedDropdown.SetValueWithoutNotify(typeWriter.textSpeed);
    }

    public void UpdateSliderShakeMagnitude()
    {
        screenshake.UpdateShakeMagnitude(shakeSlider.value / 10);
        shakeValue.text = shakeSlider.value.ToString();
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
        textSpeedObject.SetActive(!textSpeedObject.activeSelf);
        typeWriter.dynamicText = !typeWriter.dynamicText;
        PlayerPrefsManager.ToggleBoolPlayerPref(PlayerPrefsManager.dynamicTextKEY);
    }

    public void UpdateDropdownTextSpeed()
    {
        typeWriter.UpdateTypingDelay(textSpeedDropdown.value);
        PlayerPrefsManager.SaveIntPlayerPref(PlayerPrefsManager.textSpeedKEY, textSpeedDropdown.value);
    }

}