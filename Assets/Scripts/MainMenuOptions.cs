/*
 * File:        MainMenuOptions.cs
 * Author:      Étienne Ménard
 * Description: Handles all the options in the main menu.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuOptions : MonoBehaviour
{
    [Header("Audio Options")]
    private AudioController _audioController;
    [SerializeField] Slider musicSlider;
    [SerializeField] TextMeshProUGUI musicVal;
    [SerializeField] Slider sfxSlider;
    [SerializeField] TextMeshProUGUI sfxVal;
    [SerializeField] Toggle menuSoundtrackToggle;
    [SerializeField] Toggle visualiserToggle;

    [Header("Visual Options")]
    [SerializeField] Toggle gridToggle;
    [SerializeField] Screenshake screenshake;
    [SerializeField] Toggle shakeToggle;
    [SerializeField] GameObject shakeIntensity;
    private Slider _shakeSlider;
    private TextMeshProUGUI _shakeValue;
    [SerializeField] Toggle dynamicTextToggle;
    [SerializeField] TMP_Dropdown textSpeedDropdown;
    public float typeSpeed;

    [Header("Game Options")]
    [SerializeField] TMP_Dropdown difficultyDropdown;
    [SerializeField] Slider lockDelaySlider;
    [SerializeField] TextMeshProUGUI lockDelayValue;
    [Range(0,5)] public int lockDelay; 
    [SerializeField] Toggle hardDropToggle;
    [SerializeField] Toggle scoreToggle;
    [SerializeField] Toggle nextPieceToggle;
    [SerializeField] Toggle holdPieceToggle;
    [SerializeField] Toggle ghostPieceToggle;

    [Header("SFX Options")]
    [SerializeField] Toggle moveSfxToggle;
    [SerializeField] Toggle rotateSfxToggle;
    [SerializeField] Toggle hardDropSfxToggle;
    [SerializeField] Toggle holdPieceSfxToggle;
    [SerializeField] Toggle pieceLockingSfxToggle;

    [Header("Extras Options")]
    [SerializeField] TMP_Dropdown cellFaceDropdown;
    [SerializeField] Toggle gameVersionToggle;
    [SerializeField] TextMeshProUGUI gameVersionText;

    private void Awake()
    {
        _audioController = FindObjectOfType<AudioController>();
    }

    private void Start()
    {
        // load audio options
        musicSlider.value = _audioController.music;
        sfxSlider.value = _audioController.sfx;

        if (!FindObjectOfType<MenuSoundtrack>().toggled)
        {
            menuSoundtrackToggle.SetIsOnWithoutNotify(false);
        }

        if (PlayerPrefsManager.GetIntPlayerPref(PlayerPrefsManager.visualiserKEY, 1) == 0)
        {
            visualiserToggle.SetIsOnWithoutNotify(false);
        }

        // get visual options
        _shakeSlider = shakeIntensity.GetComponentInChildren<Slider>();
        _shakeValue = shakeIntensity.GetComponentInChildren<TextMeshProUGUI>();
        _shakeSlider.value = screenshake.shakeMagnitude * 10;

        textSpeedDropdown.SetValueWithoutNotify(PlayerPrefsManager.GetIntPlayerPref(PlayerPrefsManager.textSpeedKEY, 1));
        typeSpeed = textSpeedDropdown.value;

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
            textSpeedDropdown.gameObject.SetActive(false);
        }

        // load game options
        difficultyDropdown.SetValueWithoutNotify(PlayerPrefsManager.GetIntPlayerPref(PlayerPrefsManager.difficultyLevelKEY, 1));

        lockDelaySlider.value = lockDelay = PlayerPrefsManager.GetIntPlayerPref(PlayerPrefsManager.lockDelayKEY, 5);
        UpdateLockDelay((int)lockDelaySlider.value);

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

        if (PlayerPrefsManager.GetIntPlayerPref(PlayerPrefsManager.ghostPieceKEY, 1) == 0)
        {
            ghostPieceToggle.SetIsOnWithoutNotify(false);
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
        if (PlayerPrefsManager.GetIntPlayerPref(PlayerPrefsManager.lockSfxKEY, 1) == 0)
        {
            pieceLockingSfxToggle.SetIsOnWithoutNotify(false);
        }

        // load extras options
        cellFaceDropdown.SetValueWithoutNotify(PlayerPrefsManager.GetIntPlayerPref(PlayerPrefsManager.cellFaceKEY, 0));
        gameVersionText.text = "v" + Application.version;

        if (!PlayerPrefsManager.GetBoolPlayerPref(PlayerPrefsManager.gameVersionKEY, true))
        {
            gameVersionToggle.SetIsOnWithoutNotify(false);
            gameVersionText.gameObject.SetActive(false);
        }

        gameObject.SetActive(false); // closes the page
    }

    public void UpdateSliderMusic()
    {
        _audioController.UpdateMusic(musicSlider.value);
        musicVal.text = musicSlider.value.ToString();
    }

    public void UpdateSliderSfx()
    {
        _audioController.UpdateSfx(sfxSlider.value);
        sfxVal.text = sfxSlider.value.ToString();
    }

    public void UpdateSliderScreenshakeMagnitude()
    {
        screenshake.UpdateShakeMagnitude(_shakeSlider.value / 10);
        _shakeValue.text = _shakeSlider.value.ToString();
    }

    public void UpdateSliderLockDelay()
    {
        UpdateLockDelay((int)lockDelaySlider.value);
    }

    public void ToggleMenuSoundtrack()
    {
        FindObjectOfType<MenuSoundtrack>().ToggleSoundtrack();
        PlayerPrefsManager.ToggleBoolPlayerPref(PlayerPrefsManager.menuSoundtrackKEY);
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
        textSpeedDropdown.gameObject.SetActive(!textSpeedDropdown.gameObject.activeSelf);
    }

    public void UpdateDropdownTextSpeed() 
    {
        PlayerPrefsManager.SaveIntPlayerPref(PlayerPrefsManager.textSpeedKEY, textSpeedDropdown.value);
    }

    public void UpdateDropdownDifficulty()
    {
        PlayerPrefsManager.SaveIntPlayerPref(PlayerPrefsManager.difficultyLevelKEY, difficultyDropdown.value);
    }

    private void UpdateLockDelay(int d)
    {
        if (lockDelay != d)
        {
            lockDelay = d;
            PlayerPrefsManager.SaveIntPlayerPref(PlayerPrefsManager.lockDelayKEY, d);
        }

        switch (lockDelay)
        {
            case 0:
                lockDelayValue.text = "0s";
                break;
            case 1:
                lockDelayValue.text = "0.1s";
                break;
            case 2:
                lockDelayValue.text = "0.2s";
                break;
            case 3:
                lockDelayValue.text = "0.3s";
                break;
            case 4:
                lockDelayValue.text = "0.4s";
                break;
            case 5:
                lockDelayValue.text = "0.5s";
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

    public void ToggleGhostPiece()
    {
        PlayerPrefsManager.ToggleBoolPlayerPref(PlayerPrefsManager.ghostPieceKEY);
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

    public void TogglePieceLockingSFX()
    {
        PlayerPrefsManager.ToggleBoolPlayerPref(PlayerPrefsManager.lockSfxKEY);
    }

    public void UpdateDropdownCellFace()
    {
        PlayerPrefsManager.SaveIntPlayerPref(PlayerPrefsManager.cellFaceKEY, cellFaceDropdown.value);
        FindObjectOfType<MainMenu>().MainMenuRichPresence();
    }

    public void ToggleGameVersion()
    {
        PlayerPrefsManager.ToggleBoolPlayerPref(PlayerPrefsManager.gameVersionKEY);
        gameVersionText.gameObject.SetActive(!gameVersionText.gameObject.activeSelf);
    }
}
