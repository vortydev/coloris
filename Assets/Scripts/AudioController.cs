using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    // audio settings
    public float music, sfx;

    private void Awake()
    {
        music = PlayerPrefsManager.GetFloatPlayerPref(PlayerPrefsManager.musicKEY, 10f);
        sfx = PlayerPrefsManager.GetFloatPlayerPref(PlayerPrefsManager.sfxKEY, 10f);
    }

    public void UpdateMusic(float m)
    {
        if (music != m)
        {
            music = m;
            PlayerPrefsManager.SaveFloatPlayerPref(PlayerPrefsManager.musicKEY, m);
        }
    }

    public void UpdateSfx(float s)
    {
        if (sfx != s)
        {
            sfx = s;
            PlayerPrefsManager.SaveFloatPlayerPref(PlayerPrefsManager.sfxKEY, s);
        }
    }
}