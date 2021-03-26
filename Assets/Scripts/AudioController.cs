/*
 * File:        AudioController.cs
 * Author:      Étienne Ménard
 * Description: Handles the volume levels of music and sfx.
 */

using System.Collections;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [Header("Controller")]
    private static AudioController _instance;   // instance of the object
    public float music, sfx;                    // audio settings
    public AudioSource musicSource, sfxSource;  // audio sources
    private IEnumerator _musicRoutine;

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

        musicSource.volume = music / 10;
        sfxSource.volume = sfx / 10;
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

        StopCoroutine(_musicRoutine);
        _musicRoutine = null;
    }

    public void KillSfxSource()
    {
        sfxSource.Stop();
        sfxSource.clip = null;
    }

    public void KillAudio(bool music = true, bool sfx = true)
    {
        if (music)
        {
            KillMusicSource();
        }

        if (sfx)
        {
            KillSfxSource();
        }  
    }

    public void FadeInMusic(float initial = 0, float target = 0, float duration = 3f)
    {
        if (target == 0)
            target = music;

        _musicRoutine = FadeIn(musicSource, initial, target, duration);
        StartCoroutine(_musicRoutine);
    }

    private IEnumerator FadeIn(AudioSource source, float initial, float target, float duration)
    {
        initial /= 10;
        target /= 10;

        float curTime = 0;
        source.volume = initial;

        while (source.volume < target)
        {
            curTime += Time.deltaTime;
            source.volume = Mathf.Lerp(initial, target, curTime / duration);
            yield return null;
        }
    }

    public void FadeOutMusic(float initial = 0, float target = 0, float duration = 3f)
    {
        _musicRoutine = FadeOut(musicSource, initial, target, duration);
        StartCoroutine(_musicRoutine);
    }

    private IEnumerator FadeOut(AudioSource source, float initial, float target, float duration)
    {
        initial /= 10;
        target /= 10;

        float curTime = 0;
        source.volume = initial;

        while (source.volume > target)
        {
            curTime += Time.deltaTime;
            source.volume = Mathf.Lerp(initial, target, curTime / duration);
            yield return null;
        }
    }
}
