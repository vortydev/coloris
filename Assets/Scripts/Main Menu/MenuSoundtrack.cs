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
    private bool _playing;

    private void Awake()
    {
        _audioController = GetComponent<AudioController>();
        _musicSource = _audioController.musicSource;
        toggled = PlayerPrefsManager.GetBoolPlayerPref(PlayerPrefsManager.menuSoundtrackKEY, true);
    }

    public void StartSoundtrack(bool fade = false)
    {
        if (toggled && !_playing)
        {
            _playing = true;

            _musicSource.clip = soundtrack;
            _musicSource.loop = true;
            _musicSource.Play();

            if (fade)
                _audioController.FadeInMusic(0, _audioController.music, fadeDuration);
        }
    }

    public void StopSoundtrack()
    {
        _playing = false;

        _musicSource.loop = false;
        _musicSource.clip = null;
        _musicSource.Stop();
    }

    public void ToggleSoundtrack()
    {
        toggled = !toggled;

        if (toggled)
        {
            StartSoundtrack(true);
        }
        else
        {
            StopSoundtrack();
        }
    }
}
