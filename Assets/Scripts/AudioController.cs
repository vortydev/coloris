/*
 * File:        AudioController.cs
 * Author:      Étienne Ménard
 * Description: Handles the volume levels of music and sfx.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    // audio settings
    public float music, sfx;

    private void Awake()
    {
        music = PlayerPrefsManager.GetFloatPlayerPref(PlayerPrefsManager.musicKEY, 10f);    // gets the initial values from the PlayerPrefs
        sfx = PlayerPrefsManager.GetFloatPlayerPref(PlayerPrefsManager.sfxKEY, 10f);
    }

    // Updates the music setting
    public float UpdateMusic(float m)
    {
        if (music != m) // if the new value is different from the currently stored one
        {
            music = m;  // update the music value
            PlayerPrefsManager.SaveFloatPlayerPref(PlayerPrefsManager.musicKEY, m); // saves the updated value to the PlayerPrefs
        }

        return m;
    }

    // Updates the sfx setting
    public float UpdateSfx(float s)
    {
        if (sfx != s)
        {
            sfx = s;
            PlayerPrefsManager.SaveFloatPlayerPref(PlayerPrefsManager.sfxKEY, s);
        }

        return s;
    }
}
