using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsSFX : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] SFXManager sfxManager;

    [Header("Movement")]
    [SerializeField] Toggle moveToggle;
    [SerializeField] Toggle rotateToggle;
    [SerializeField] Toggle hardDropToggle;

    [Header("UI")]
    [SerializeField] Toggle holdPieceToggle;

    private void Awake()
    {
        // MOVEMENT SFX
        if (PlayerPrefsManager.GetIntPlayerPref(PlayerPrefsManager.moveSfxKEY, 1) == 0)
        {
            moveToggle.SetIsOnWithoutNotify(false);
            sfxManager.canMove = false;
        }

        if (PlayerPrefsManager.GetIntPlayerPref(PlayerPrefsManager.rotateSfxKEY, 1) == 0)
        {
            rotateToggle.SetIsOnWithoutNotify(false);
            sfxManager.canRotate = false;
        }

        if (PlayerPrefsManager.GetIntPlayerPref(PlayerPrefsManager.hardDropSfxKEY, 1) == 0)
        {
            hardDropToggle.SetIsOnWithoutNotify(false);
            sfxManager.canHardDrop = false;
        }

        // UI SFX
        if (PlayerPrefsManager.GetIntPlayerPref(PlayerPrefsManager.holdPieceSfxKEY, 1) == 0)
        {
            holdPieceToggle.SetIsOnWithoutNotify(false);
            sfxManager.canHoldPiece = false;
        }
    }

    public void ToggleMoveSFX()
    {
        PlayerPrefsManager.ToggleBoolPlayerPref(PlayerPrefsManager.moveSfxKEY);
        sfxManager.canMove = !sfxManager.canMove;
    }

    public void ToggleRotateSFX()
    {
        PlayerPrefsManager.ToggleBoolPlayerPref(PlayerPrefsManager.rotateSfxKEY);
        sfxManager.canRotate = !sfxManager.canRotate;
    }

    public void ToggleHardDropSFX()
    {
        PlayerPrefsManager.ToggleBoolPlayerPref(PlayerPrefsManager.hardDropSfxKEY);
        sfxManager.canHardDrop = !sfxManager.canHardDrop;
    }

    public void ToggleHoldPieceSFX()
    {
        PlayerPrefsManager.ToggleBoolPlayerPref(PlayerPrefsManager.holdPieceSfxKEY);
        sfxManager.canHoldPiece = !sfxManager.canHoldPiece;
    }
}
