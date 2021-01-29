using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    // audio settings
    public float music, sfx;

    private void Awake()
    {
        music = PlayerPrefsManager.GetFloatPlayerPrefs(PlayerPrefsManager.musicKEY, 10f);
        sfx = PlayerPrefsManager.GetFloatPlayerPrefs(PlayerPrefsManager.sfxKEY, 10f);
    }

    public void UpdateMusic(float m)
    {
        if (music != m)
        {
            music = m;
            PlayerPrefsManager.SaveFloatPlayerPrefs(PlayerPrefsManager.musicKEY, m);
        }
    }

    public void UpdateSfx(float s)
    {
        if (sfx != s)
        {
            sfx = s;
            PlayerPrefsManager.SaveFloatPlayerPrefs(PlayerPrefsManager.sfxKEY, s);
        }
    }
}
