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
    public AudioController audioController;    // script that sets the audio's volume
    public AudioSource musicSource;             // component that plays the track
    [SerializeField] RadioUI radio;             // script that handles the crediting UI
    [SerializeField] TypeWriter typeWriter;     // script for the type writing effect on the radio
    [SerializeField] TrackUI trackUI;           // script that handles tracks in pause menu
    private bool onFocus = true;

    [Header("Tracks")]
    private ReferenceTracks tracks;             // array of TrackSO holding track data
    private bool[] playlist;                    // array of bools matching the TrackSO array
    private int prevTrack = -1;                 // int that holds the previous track's ind in the array
    private TrackSO _curTrack;                   
    public bool isPaused = false;
    public bool gameStarted = false;

    private void Awake()
    {
        // load components
        audioController = FindObjectOfType<AudioController>();
        tracks = FindObjectOfType<ReferenceTracks>();
        
        // resize the bool array and set all to false
        playlist = new bool[tracks.GetArraySize()];
        for (int i = 0; i < tracks.GetArraySize(); i++)
        {
            playlist[i] = false;
        }
    }

    private void Start()
    {
        musicSource = audioController.musicSource;
    }

    private void OnApplicationFocus()
    {
        if (gameStarted)
        {
            TogglePause();
            onFocus = !onFocus;
        }
    }

    private void Update()
    {
        // starts a new track when the previous is done
        if (!musicSource.isPlaying && !isPaused && gameStarted && onFocus)
            NextTrack();

        // adjusts the sound of the music
        //if (gameStarted)
            //musicSource.volume = audioController.music / 10;
    }

    // randomly chooses a track that hasn't been played yet
    public void NextTrack()
    {
        CheckPlaylist();
        int rng;
        do
            rng = Random.Range(0, tracks.GetArraySize());
        while (playlist[rng] || prevTrack == rng);

        playlist[rng] = true;
        prevTrack = rng;

        _curTrack = tracks.GetTrackSO(rng);
        musicSource.clip = _curTrack.track;
        musicSource.Play();

        //radio.DisplayTrackInfo(curTrack.trackName, curTrack.authorName);
        typeWriter.TypeText(radio.trackName, _curTrack.trackName);
        typeWriter.TypeText(radio.trackAuthor, _curTrack.authorName);

        trackUI.GetTotalTrackTime(musicSource.clip.length);

        isPaused = false;
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
