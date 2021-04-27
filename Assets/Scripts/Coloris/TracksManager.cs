/*
 * File:        TracksManager.cs
 * Author:      Étienne Ménard
 * Description: Generates a shuffled playlist of all the tracks and plays the tracks.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TracksManager : MonoBehaviour
{
    [Header("Components")]
    public AudioController audioController;     // script that sets the audio's volume
    public AudioSource musicSource;             // component that plays the track
    [SerializeField] RadioUI radio;             // script that handles the crediting UI
    [SerializeField] TypeWriter typeWriter;     // script for the type writing effect on the radio
    [SerializeField] TrackUI trackUI;           // script that handles tracks in pause menu

    [Header("Tracks")]
    private ReferenceTracks _tracks;            // array of TrackSO holding track data
    private bool[] playlist;                    // array of bools matching the TrackSO array
    private int prevTrack = -1;                 // int that holds the previous track's ind in the array
    public TrackSO curTrack;                    // the currently selected track

    [Header("Settings")]
    public bool isPaused = false;               // is the music paused
    public bool gameStarted = false;            // has the game started
    private bool _onFocus = true;               // is the game is in-focus
    public int mode;                            // the mode the tracks manager is set to (0: classic, 1: speed clear)

    private void Awake()
    {
        // load components
        audioController = FindObjectOfType<AudioController>();
        _tracks = FindObjectOfType<ReferenceTracks>();
        
        // resize the bool array and set all to false
        playlist = new bool[_tracks.GetArraySize()];
        for (int i = 0; i < _tracks.GetArraySize(); i++)
        {
            playlist[i] = false;
        }
    }

    private void Start()
    {
        musicSource = audioController.musicSource;
        mode = GameplayController.gameMode;
    }

    private void OnApplicationFocus()
    {
        if (gameStarted)
        {
            TogglePause();
            _onFocus = !_onFocus;
        }
    }

    private void Update()
    {
        // starts a new track when the previous is done
        if (!musicSource.isPlaying && !isPaused && gameStarted && _onFocus && mode == 0)
            NextTrack();
    }

    // randomly chooses a track that hasn't been played yet
    public void NextTrack()
    {
        CheckPlaylist();
        int rng;
        do
            rng = Random.Range(0, _tracks.GetArraySize());
        while (playlist[rng] || prevTrack == rng);

        playlist[rng] = true;
        prevTrack = rng;

        curTrack = _tracks.GetTrackSO(rng);
        musicSource.clip = curTrack.Clip;
        musicSource.Play();

        DisplayTrackUI();

        isPaused = false;
    }

    public void DisplayTrackUI()
    {
        typeWriter.TypeText(radio.trackName, curTrack.TrackName);
        typeWriter.TypeText(radio.trackAuthor, curTrack.ArtistName);

        trackUI.GetTotalTrackTime(musicSource.clip.length);
    }

    // checks if the playlist is done and resets it if it is
    private void CheckPlaylist()
    {
        if (IsPlaylistDone())
        {
            for (int i = 0; i < playlist.Length; i++)
            {
                playlist[i] = false;
            }
        }
    }

    // returns if the playlist is done or not
    private bool IsPlaylistDone()
    {
        for (int i = 0; i < playlist.Length; i++)
        {
            if (!playlist[i])
                return false;
        }

        return true;
    }

    public void RestartTrack()
    {
        musicSource.Stop();
        musicSource.Play();

        isPaused = false;
    }

    public void TogglePause()
    {
        if (isPaused)
        {
            musicSource.UnPause();
            isPaused = false;
        }
        else
        {
            musicSource.Pause();
            isPaused = true;
        }
    }

    public void StopMusic()
    {
        isPaused = false;
        musicSource.Stop();
    }
}
