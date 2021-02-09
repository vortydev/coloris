using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuOptions : MonoBehaviour
{
    [Header("Audio Options")]
    [SerializeField] AudioController audioController;
    [SerializeField] Slider musicSlider;
    [SerializeField] TextMeshProUGUI musicVal;
    [SerializeField] Slider sfxSlider;
    [SerializeField] TextMeshProUGUI sfxVal;
    [SerializeField] Toggle visualiserToggle;

    [Header("Visual Options")]
    [SerializeField] Toggle gridToggle;
    [SerializeField] Screenshake screenshake;
    [SerializeField] Toggle shakeToggle;
    [SerializeField] GameObject shakeIntensity;
    private Slider shakeSlider;
    private TextMeshProUGUI shakeValue;

    [Header("Game Options")]
    [SerializeField] Slider difficultyLevelSlider;
    [SerializeField] TextMeshProUGUI difficultyLevelText;
    public int level;
    [SerializeField] Toggle scoreToggle;
    [SerializeField] Toggle nextPieceToggle;
    [SerializeField] Toggle holdPieceToggle;
    //[SerializeField] Toggle ghostPieceToggle;

    private void Start()
    {
        // load audio options
        musicSlider.value = audioController.music;
        sfxSlider.value = audioController.sfx;

        if (PlayerPrefsManager.GetIntPlayerPref(PlayerPrefsManager.visualiserKEY, 1) == 0)
        {
            visualiserToggle.SetIsOnWithoutNotify(false);
        }

        // get visual components
        shakeSlider = shakeIntensity.GetComponentInChildren<Slider>();
        shakeValue = shakeIntensity.GetComponentInChildren<TextMeshProUGUI>();

        // load visual options
        shakeSlider.value = screenshake.shakeMagnitude * 10;

        if (PlayerPrefsManager.GetIntPlayerPref(PlayerPrefsManager.gridKEY, 1) == 0)
        {
            gridToggle.SetIsOnWithoutNotify(false);
        }

        if (PlayerPrefsManager.GetIntPlayerPref(PlayerPrefsManager.screenshakeKEY, 1) == 0)
        {
            screenshake.enabled = false;
            shakeToggle.SetIsOnWithoutNotify(false);
            shakeIntensity.SetActive(false);
        }

        // load game options
        difficultyLevelSlider.value = level = PlayerPrefsManager.GetIntPlayerPref(PlayerPrefsManager.difficultyLevelKEY, 1);

        if (PlayerPrefs.GetInt(PlayerPrefsManager.scoreKEY) == 0)
        {
            scoreToggle.SetIsOnWithoutNotify(false);
        }

        if (PlayerPrefs.GetInt(PlayerPrefsManager.nextPieceKEY) == 0)
        {
            nextPieceToggle.SetIsOnWithoutNotify(false);
        }

        if (PlayerPrefs.GetInt(PlayerPrefsManager.holdPieceKEY) == 0)
        {
            holdPieceToggle.SetIsOnWithoutNotify(false);
        }

        gameObject.SetActive(false);
    }

    private void Update()
    {
        // update audio controller
        audioController.UpdateMusic(musicSlider.value);
        audioController.UpdateSfx(sfxSlider.value);

        // update audio sliders' values
        musicVal.text = musicSlider.value.ToString();
        sfxVal.text = sfxSlider.value.ToString();

        // screenshake
        screenshake.UpdateShakeMagnitude(shakeSlider.value / 10);
        shakeValue.text = shakeSlider.value.ToString();

        // difficulty
        UpdateDifficultyLevel((int)difficultyLevelSlider.value);
    }

    public void ToggleGrid()
    {
        PlayerPrefsManager.ToggleBoolPlayerPref(PlayerPrefsManager.gridKEY);
    }

    public void ToggleScreenshake()
    {
        screenshake.enabled = !screenshake.enabled;
        shakeIntensity.SetActive(!shakeIntensity.activeSelf);
        PlayerPrefsManager.ToggleBoolPlayerPref(PlayerPrefsManager.screenshakeKEY);
    }

    public void UpdateDifficultyLevel(int d)
    {
        if (level != d)
        {
            level = d;
            PlayerPrefsManager.SaveIntPlayerPref(PlayerPrefsManager.difficultyLevelKEY, d);
        }

        switch (level)
        {
            case 0:
                difficultyLevelText.text = "Easy";
                break;
            case 1:
                difficultyLevelText.text = "Normal";
                break;
            case 2:
                difficultyLevelText.text = "Hard";
                break;
            case 3:
                difficultyLevelText.text = "Insane";
                break;
        }
    }

    public string GetDifficultyString()
    {
        switch (level)
        {
            case 0:
                return "Easy";
            case 1:
                return "Normal";
            case 2:
                return "Hard";
            case 3:
                return "Insane";
        }

        return "???";
    }

    public void ToggleScore()
    {
        PlayerPrefsManager.ToggleBoolPlayerPref(PlayerPrefsManager.scoreKEY);
    }

    public void ToggleNextPiece()
    {
        PlayerPrefsManager.ToggleBoolPlayerPref(PlayerPrefsManager.nextPieceKEY);
    }

    public void ToggleHoldPiece()
    {
        PlayerPrefsManager.ToggleBoolPlayerPref(PlayerPrefsManager.holdPieceKEY);
    }
}
