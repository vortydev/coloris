using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TracksManager : MonoBehaviour
{
    [Header("Components")]
    public AudioSource audioSource;            // component that plays the track
    private AudioController audioController;    // script that sets the audio's volume
    [SerializeField] RadioUI radio;             // script that handles the crediting UI
    [SerializeField] TrackUI trackUI;

    [Header("Tracks")]
    [SerializeField] TrackSO[] tracks;          // array of TrackSO holding track data
    private bool[] playlist;                    // array of bools matching the TrackSO array
    private int prevTrack = -1;                 // int that holds the previous track's ind in the array
    private TrackSO curTrack;
    public bool isPaused = false;
    public int tracksPlayed = 0;

    private void Awake()
    {
        // load components
        audioSource = GetComponent<AudioSource>();
        audioController = GetComponent<AudioController>();
        
        // resize the bool array and set all to false
        playlist = new bool[tracks.Length];
        for (int i = 0; i < tracks.Length; i++)
        {
            playlist[i] = false;
        }
    }

    private void OnApplicationFocus()
    {
        TogglePause();
    }

    private void Update()
    {
        // starts a new track when the previous is done
        if (!audioSource.isPlaying && !isPaused)
            NextTrack();

        // adjusts the sound of the music
        audioSource.volume = audioController.music / 10;
    }

    // randomly chooses a track that hasn't been played yet
    public void NextTrack()
    {
        CheckPlaylist();
        int rng;
        do
            rng = Random.Range(0, tracks.Length);
        while (playlist[rng] || prevTrack == rng);

        playlist[rng] = true;
        prevTrack = rng;

        curTrack = tracks[rng];
        audioSource.clip = curTrack.track;
        audioSource.Play();

        radio.DisplayTrackInfo(curTrack.trackName, curTrack.authorName);
        trackUI.GetTotalTrackTime(audioSource.clip.length);
        tracksPlayed++;

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
        audioSource.Stop();
        audioSource.Play();

        isPaused = false;
        tracksPlayed++;
    }

    public void TogglePause()
    {
        if (isPaused)
        {
            audioSource.UnPause();
            isPaused = false;
        }
        else
        {
            audioSource.Pause();
            isPaused = true;
        }
    }

    public void StopMusic()
    {
        isPaused = false;
        audioSource.Stop();
    }
}
