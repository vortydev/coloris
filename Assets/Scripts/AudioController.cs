/*
 * File:        AudioController.cs
 * Author:      Étienne Ménard
 * Description: Handles the volume levels of music and sfx.
 */

using UnityEngine;

public class AudioController : MonoBehaviour
{
    [Header("Controller")]
    private static AudioController _instance;   // instance of the object
    public float music, sfx;                    // audio settings
    public AudioSource musicSource, sfxSource;  // audio sources

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);  // makes the array persistent across scenes
        }
        else if (_instance != this)
        {
            Destroy(gameObject);            // otherwise yeet it out of existence
        }

        music = PlayerPrefsManager.GetFloatPlayerPref(PlayerPrefsManager.musicKEY, 10f);    // gets the initial values from the PlayerPrefs
        sfx = PlayerPrefsManager.GetFloatPlayerPref(PlayerPrefsManager.sfxKEY, 10f);
    }

    // Updates the music volume
    public float UpdateMusic(float m)
    {
        if (music != m) // if the new value is different from the currently stored one
        {
            music = m;                      // update the music value
            musicSource.volume = m / 10;    // update the music source's volume

            PlayerPrefsManager.SaveFloatPlayerPref(PlayerPrefsManager.musicKEY, m); // saves the updated value to the PlayerPrefs
        }

        return m;
    }

    // Updates the sfx volume
    public float UpdateSfx(float s)
    {
        if (sfx != s)
        {
            sfx = s;                    // update the sfx value
            sfxSource.volume = s / 10;  // update the sfx source's volume

            PlayerPrefsManager.SaveFloatPlayerPref(PlayerPrefsManager.sfxKEY, s);
        }

        return s;
    }

    public void KillMusicSource()
    {
        musicSource.Stop();
        musicSource.clip = null;
    }

    public void KillSfxSource()
    {
        sfxSource.Stop();
        sfxSource.clip = null;
    }

    public void KillAudio(bool music = true, bool sfx = true)
    {
        if (music)
            KillMusicSource();

        if (sfx)
            KillSfxSource();
    }
}
