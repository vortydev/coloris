using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TracksManager : MonoBehaviour
{
    [Header("Components")]
    private AudioSource audioSource;            // component that plays the track
    private AudioController audioController;    // script that sets the audio's volume
    private BopManager bopManager;               // script that handles the cells bopping
    [SerializeField] RadioUI radio;             // script that handles the crediting UI

    [Header("Tracks")]
    [SerializeField] TrackSO[] tracks;          // array of TrackSO holding track data
    private bool[] playlist;                    // array of bools matching the TrackSO array
    private int prevTrack = -1;                 // int that holds the previous track's ind in the array
    private TrackSO curTrack;

    private void Awake()
    {
        // load components
        audioSource = GetComponent<AudioSource>();
        audioController = GetComponent<AudioController>();
        bopManager = GetComponent<BopManager>();
        
        // resize the bool array and set all to false
        playlist = new bool[tracks.Length];
        for (int i = 0; i < tracks.Length; i++)
        {
            playlist[i] = false;
        }
    }

    private void Start()
    {
        NextTrack();
    }

    private void Update()
    {
        // starts a new track when the previous is done
        if (!audioSource.isPlaying)
            NextTrack();

        // adjusts the sound of the music
        audioSource.volume = audioController.music / 10;
    }

    // randomly chooses a track that hasn't been played yet
    private void NextTrack()
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
        bopManager.GetCurTrack(curTrack);
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
}
