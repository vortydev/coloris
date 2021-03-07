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
    [SerializeField] Toggle dynamicTextToggle;
    [SerializeField] GameObject textSpeed;
    private Slider textSpeedSlider;
    private TextMeshProUGUI textSpeedValue;
    public float typeSpeed;

    [Header("Game Options")]
    [SerializeField] Slider difficultyLevelSlider;
    [SerializeField] TextMeshProUGUI difficultyLevelText;
    public int level;
    [SerializeField] Toggle hardDropToggle;
    [SerializeField] Toggle scoreToggle;
    [SerializeField] Toggle nextPieceToggle;
    [SerializeField] Toggle holdPieceToggle;
    //[SerializeField] Toggle ghostPieceToggle;

    [Header("SFX Options")]
    [SerializeField] Toggle moveSfxToggle;
    [SerializeField] Toggle rotateSfxToggle;
    [SerializeField] Toggle hardDropSfxToggle;
    [SerializeField] Toggle holdPieceSfxToggle;

    [Header("Extras Options")]
    [SerializeField] Toggle flushedToggle;

    private void Start()
    {
        // load audio options
        musicSlider.value = audioController.music;
        sfxSlider.value = audioController.sfx;

        if (PlayerPrefsManager.GetIntPlayerPref(PlayerPrefsManager.visualiserKEY, 1) == 0)
        {
            visualiserToggle.SetIsOnWithoutNotify(false);
        }

        // get visual options
        shakeSlider = shakeIntensity.GetComponentInChildren<Slider>();
        shakeValue = shakeIntensity.GetComponentInChildren<TextMeshProUGUI>();
        shakeSlider.value = screenshake.shakeMagnitude * 10;

        textSpeedSlider = textSpeed.GetComponentInChildren<Slider>();
        textSpeedValue = textSpeed.GetComponentInChildren<TextMeshProUGUI>();
        textSpeedSlider.value = typeSpeed = PlayerPrefsManager.GetIntPlayerPref(PlayerPrefsManager.textSpeedKEY, 2);

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

        if (PlayerPrefsManager.GetIntPlayerPref(PlayerPrefsManager.dynamicTextKEY, 1) == 0)
        {
            dynamicTextToggle.SetIsOnWithoutNotify(false);
            textSpeed.SetActive(false);
        }

        // load game options
        difficultyLevelSlider.value = level = PlayerPrefsManager.GetIntPlayerPref(PlayerPrefsManager.difficultyLevelKEY, 1);

        if (PlayerPrefsManager.GetIntPlayerPref(PlayerPrefsManager.hardDropKEY, 1) == 0)
        {
            hardDropToggle.SetIsOnWithoutNotify(false);
        }

        if (PlayerPrefsManager.GetIntPlayerPref(PlayerPrefsManager.scoreKEY, 1) == 0)
        {
            scoreToggle.SetIsOnWithoutNotify(false);
        }

        if (PlayerPrefsManager.GetIntPlayerPref(PlayerPrefsManager.nextPieceKEY, 1) == 0)
        {
            nextPieceToggle.SetIsOnWithoutNotify(false);
        }

        if (PlayerPrefsManager.GetIntPlayerPref(PlayerPrefsManager.holdPieceKEY, 1) == 0)
        {
            holdPieceToggle.SetIsOnWithoutNotify(false);
        }

        // load sfx options
        if (PlayerPrefsManager.GetIntPlayerPref(PlayerPrefsManager.moveSfxKEY, 1) == 0)
        {
            moveSfxToggle.SetIsOnWithoutNotify(false);
        }
        if (PlayerPrefsManager.GetIntPlayerPref(PlayerPrefsManager.rotateSfxKEY, 1) == 0)
        {
            rotateSfxToggle.SetIsOnWithoutNotify(false);
        }
        if (PlayerPrefsManager.GetIntPlayerPref(PlayerPrefsManager.hardDropSfxKEY, 1) == 0)
        {
            hardDropSfxToggle.SetIsOnWithoutNotify(false);
        }
        if (PlayerPrefsManager.GetIntPlayerPref(PlayerPrefsManager.holdPieceSfxKEY, 1) == 0)
        {
            holdPieceSfxToggle.SetIsOnWithoutNotify(false);
        }

        // load extras options
        if (PlayerPrefsManager.GetIntPlayerPref(PlayerPrefsManager.flushedKEY, 0) == 0)
        {
            flushedToggle.SetIsOnWithoutNotify(false);
        }

        gameObject.SetActive(false); // closes the page
    }

    public void UpdateSliderMusic()
    {
        audioController.UpdateMusic(musicSlider.value);
        musicVal.text = musicSlider.value.ToString();
    }

    public void UpdateSliderSfx()
    {
        audioController.UpdateSfx(sfxSlider.value);
        sfxVal.text = sfxSlider.value.ToString();
    }

    public void UpdateSliderScreenshakeMagnitude()
    {
        screenshake.UpdateShakeMagnitude(shakeSlider.value / 10);
        shakeValue.text = shakeSlider.value.ToString();
    }

    public void UpdateSliderTextSpeed()
    {
        UpdateTextSpeed((int)textSpeedSlider.value);
    }

    public void UpdateSliderDifficulty()
    {
        UpdateDifficultyLevel((int)difficultyLevelSlider.value);
    }

    public void ToggleVisualizer()
    {
        PlayerPrefsManager.ToggleBoolPlayerPref(PlayerPrefsManager.visualiserKEY);
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

    public void ToggleDynamicText()
    {
        PlayerPrefsManager.ToggleBoolPlayerPref(PlayerPrefsManager.dynamicTextKEY);
        textSpeed.SetActive(!textSpeed.activeSelf);
    }

    private void UpdateTextSpeed(int s) 
    {
        if (typeSpeed != s)
        {
            typeSpeed = s;
            PlayerPrefsManager.SaveIntPlayerPref(PlayerPrefsManager.textSpeedKEY, s);
        }

        switch (typeSpeed)
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

    private void UpdateDifficultyLevel(int d)
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

    public void ToggleHardDrop()
    {
        PlayerPrefsManager.ToggleBoolPlayerPref(PlayerPrefsManager.hardDropKEY);
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

    public void ToggleMoveSFX()
    {
        PlayerPrefsManager.ToggleBoolPlayerPref(PlayerPrefsManager.moveSfxKEY);
    }

    public void ToggleRotateSFX()
    {
        PlayerPrefsManager.ToggleBoolPlayerPref(PlayerPrefsManager.rotateSfxKEY);
    }

    public void ToggleHardDropSFX()
    {
        PlayerPrefsManager.ToggleBoolPlayerPref(PlayerPrefsManager.hardDropSfxKEY);
    }

    public void ToggleHoldPieceSFX()
    {
        PlayerPrefsManager.ToggleBoolPlayerPref(PlayerPrefsManager.holdPieceSfxKEY);
    }

    public void ToggleFlushed()
    {
        PlayerPrefsManager.ToggleBoolPlayerPref(PlayerPrefsManager.flushedKEY);
        FindObjectOfType<MainMenu>().MainMenuRichPresence();
    }
}
