using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    // audio settings
    public float music;
    public float sfx;

    void Awake()
    {
        if (!PlayerPrefs.HasKey("music"))
        {
            PlayerPrefs.SetFloat("music", 10f);
            PlayerPrefs.SetFloat("sfx", 10f);

            PlayerPrefs.Save();
        }

        // loads controller's variable from PlayerPrefs
        music = PlayerPrefs.GetFloat("music");
        sfx = PlayerPrefs.GetFloat("sfx");
    }

    public void UpdateMusic(float m)
    {
        if (music != m)
        {
            music = m;
            SettingsManager.SaveFloatPlayerPrefs("music", m);
        }
    }

    public void UpdateSfx(float s)
    {
        if (sfx != s)
        {
            sfx = s;
            SettingsManager.SaveFloatPlayerPrefs("sfx", s);
        }
    }
}
