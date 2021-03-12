using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSoundtrack : MonoBehaviour
{
    private AudioController _audioController;
    private AudioSource _musicSource;
    [SerializeField] AudioClip soundtrack;
    public bool toggled;
    [SerializeField] float fadeDuration = 1f;

    private void Awake()
    {
        _audioController = GetComponent<AudioController>();
        _musicSource = _audioController.musicSource;
        toggled = PlayerPrefsManager.GetBoolPlayerPref(PlayerPrefsManager.menuSoundtrackKEY, true);
    }

    public void StartSoundtrack()
    {
        if (toggled)
        {
            _musicSource.clip = soundtrack;
            _musicSource.loop = true;
            _musicSource.Play();

            StartCoroutine(FadeIn());
        }
    }

    private IEnumerator FadeIn()
    {
        float currentTime = 0;
        float targetVolume = _audioController.music / 10;
        _musicSource.volume = 0;

        while (_musicSource.volume < targetVolume)
        {
            currentTime += Time.deltaTime;
            _musicSource.volume = Mathf.Lerp(0, targetVolume, currentTime / fadeDuration);
            yield return null;
        }
    }

    public void StopSoundtrack()
    {
        _musicSource.loop = false;
        _musicSource.clip = null;
        _musicSource.Stop();
    }

    public void ToggleSoundtrack()
    {
        toggled = !toggled;

        if (toggled)
        {
            StartSoundtrack();
        }
        else
        {
            StopSoundtrack();
        }
    }
}
