using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    // audio settings
    public float music = 10;
    public float sfx = 10;

    void Awake()
    {
        PlayerPrefs.SetFloat("music", 10f);
        PlayerPrefs.SetFloat("sfx", 10f);

        // loads controller's variable from PlayerPrefs
        music = PlayerPrefs.GetFloat("music");
        sfx = PlayerPrefs.GetFloat("sfx");
    }

    public void UpdateMusic(float m)
    {
        if (music != m)
        {
            music = m;
        }
    }

    public void UpdateSfx(float s)
    {
        if (sfx != s)
        {
            sfx = s;
        }
    }

    public void SavePlayerPrefs()
    {
        PlayerPrefs.SetFloat("music", music);
        PlayerPrefs.SetFloat("sfx", sfx);
        PlayerPrefs.Save();
    }
}
