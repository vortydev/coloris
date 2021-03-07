/*
 * File:        ChillZoneManager.cs
 * Author:      Étienne Ménard
 * Description: Handles most of the stuff for the Chill Zone.
 */

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ChillZoneManager : MonoBehaviour
{
    [Header("Components")]
    private MyControls _actions;
    public AudioSource audioSource;
    [SerializeField] AudioController audioController;
    [SerializeField] RadioUI radio;
    [SerializeField] TypeWriter typeWriter;
    [SerializeField] bool onFocus;

    [Header("References")]
    public ReferenceTracks tracks;

    [Header("Tracks")]
    public bool paused = false;
    public TrackSO selectedTrack;
    public TrackSO currentTrack;
    public List<TrackSO> playlist;

    [Header("Chill Zone")]
    [SerializeField] CZ_MediaControls mediaControls;
    [SerializeField] CZ_Catalog catalog;
    [SerializeField] CZ_Queue queue;
    [SerializeField] GameObject quitPopup;

    private void Awake()
    {
        _actions = new MyControls();
        tracks = FindObjectOfType<ReferenceTracks>();
    }

    private void OnEnable()
    {
        // enable the input
        _actions.Enable();

        // pause
        _actions.Coloris.Pause.performed += Pause;
    }

    private void OnDisable()
    {
        // enable the input
        _actions.Disable();

        // pause
        _actions.Coloris.Pause.performed -= Pause;
    }

    private void Start()
    {
        onFocus = true;
        quitPopup.SetActive(false);

        if (FindObjectOfType<DiscordController>() != null)
            FindObjectOfType<DiscordController>().UpdateRichPresence("Listening to music", "Vibing in the Chill Zone", PlayerPrefsManager.GetBoolPlayerPref(PlayerPrefsManager.flushedKEY));
    }

    // pauses the music when the game goes out of focus
    private void OnApplicationFocus()
    {
        onFocus = !onFocus;

        if (!onFocus && !paused)
        {
            audioSource.Pause();
        }
        else if (onFocus && !paused)
        {
            audioSource.UnPause();
        }
        
    }

    private void Update()
    {
        if (!audioSource.isPlaying && currentTrack != null && !paused)
        {
            if (onFocus)
            {
                NextTrackInPlaylist();  // plays the next track if the game is in focus and the track isn't paused
            }
        }
    }

    private void Pause(InputAction.CallbackContext obj)
    {
        ToggleQuitPopup();
    }

    public void GetSelectedCatalogTrack(int trackNb)
    {
        selectedTrack = tracks.GetTrackSO(trackNb - 1);    // sets the selected track from the reference array

        queue.ToggleCatalogTrackControls(true); // disables and enables relevant control buttons
        queue.EnableQueueTrackControls(false);  // disables and enables relevant control buttons
    }

    // sets the selected track from the reference array
    public void GetSelectedQueueTrack(int trackNb)
    {
        selectedTrack = tracks.GetTrackSO(trackNb - 1);

        queue.ToggleQueueTrackControls(true);
        queue.EnableQueueTrackControls(true);
    }

    private void DeselectSelectedTrack()
    {
        selectedTrack = null;   // deselects the track

        queue.ToggleCatalogTrackControls(false);    // disables control buttons
        queue.ToggleQueueTrackControls(false);
    }

    // pauses and unpauses the music
    public void TogglePause()
    {
        if (paused)
        {
            audioSource.UnPause();
            paused = false;
        }
        else
        {
            audioSource.Pause();
            paused = true;
        }
    }

    // restarts the track fromt the start
    public void RestartTrack()
    {
        audioSource.Stop();
        audioSource.Play();

        paused = false;
    }

    private void NextTrackInPlaylist()
    {
        switch (queue.loopDropdown.value)   // checks what kind of looping is on (no its not a bool because I might have different loop modes coming up)
        {
            case 0:             // no looping
                ClickSkip();    // goes to the next track if there is one
                break;

            case 1:                 // looping enabled
                audioSource.Play(); // replays the loaded track
                break;
        }
    }

    public void ClickCatalogPlay()
    {
        currentTrack = selectedTrack;   // sets the selected track as the current track
        DeselectSelectedTrack();        // deselects the track

        audioSource.clip = currentTrack.track;  // loads audio source
        audioSource.Play();                     // plays the track

        UpdateMediaControlsAndRadio();                      // update media controls
        mediaControls.UpdateSkipButtonText(playlist.Count); // updates the skip button's text

        queue.loopDropdown.interactable = true; // enables the loop dropdown
    }

    public void ClickSkip()
    {
        if (playlist.Count > 0) // if there's other tracks in the playlist
        {
            currentTrack = playlist[0]; // loads track in the AudioSource
            playlist.RemoveAt(0);       // removes the track from the playlist

            audioSource.clip = currentTrack.track;
            audioSource.Play();

            queue.CreateQueue(playlist);    // regenerates the queue

            UpdateMediaControlsAndRadio();
            mediaControls.UpdateSkipButtonText(playlist.Count);
        }
        else
        {
            audioSource.Stop();     // stops the music
            currentTrack = null;    // empties the current track

            mediaControls.ToggleMediaControls(false);
            mediaControls.time.text = "00:00 / 00:00";

            typeWriter.TypeText(radio.trackName, "");
            typeWriter.TypeText(radio.trackAuthor, "");

            queue.loopDropdown.interactable = false;
            queue.loopDropdown.SetValueWithoutNotify(0);
            queue.loopDropdown.RefreshShownValue();
        }
    }

    public void ClickQueuePlay()
    {
        currentTrack = selectedTrack;   // sets the selected track as the current track

        if (playlist[0] == selectedTrack) 
        {
            playlist.Remove(selectedTrack);
            queue.CreateQueue(playlist);
        }
        
        DeselectSelectedTrack();        // deselects the track

        audioSource.clip = currentTrack.track;  // loads audio source
        audioSource.Play();                     // plays the track

        UpdateMediaControlsAndRadio();
        mediaControls.UpdateSkipButtonText(playlist.Count);

        queue.loopDropdown.interactable = true;
    }

    private void UpdateMediaControlsAndRadio()
    {
        mediaControls.GetTotalTrackTime(currentTrack.track.length); // updates the time
        mediaControls.ToggleMediaControls(true);                    // toggles media controls on

        typeWriter.TypeText(radio.trackName, currentTrack.trackName);       // types on the radio the current track's name
        typeWriter.TypeText(radio.trackAuthor, currentTrack.authorName);    // types on the radio the current track's artist
    }

    public void ClickNext()
    {
        playlist.Insert(0, selectedTrack);
        DeselectSelectedTrack();

        queue.CreateQueue(playlist);

        mediaControls.UpdateSkipButtonText(playlist.Count);
    }

    public void ClickQueue()
    {
        playlist.Add(selectedTrack);
        DeselectSelectedTrack();

        queue.CreateQueue(playlist);

        mediaControls.UpdateSkipButtonText(playlist.Count);
    }

    public void ClickSkipTo()
    {
        currentTrack = selectedTrack;
        audioSource.clip = currentTrack.track;  // loads audio source
        audioSource.Play();                     // plays the track

        int pos = playlist.IndexOf(selectedTrack);
        for (int i = 0; i < pos + 1; i++)
        {
            playlist.RemoveAt(0);
        }

        queue.CreateQueue(playlist);

        DeselectSelectedTrack();
        UpdateMediaControlsAndRadio();
        mediaControls.UpdateSkipButtonText(playlist.Count);
    }

    public void ClickRemove()
    {
        playlist.Remove(selectedTrack);
        DeselectSelectedTrack();
        queue.CreateQueue(playlist);
        mediaControls.UpdateSkipButtonText(playlist.Count);
    }

    public void ClickShuffle()
    {
        for (int i = 0; i < playlist.Count; i++) 
        {
            TrackSO temp = playlist[i];
            int ind = Random.Range(i, playlist.Count);
            playlist[i] = playlist[ind];
            playlist[ind] = temp;
        }

        queue.CreateQueue(playlist);
    }

    public void ClickEmpty()
    {
        playlist.Clear();
        queue.EmptyQueue();

        queue.TogglePlaylistButtons(false);
        mediaControls.UpdateSkipButtonText(playlist.Count);
    }

    public void ToggleQuitPopup()
    {
        FindObjectOfType<UISFX>().ClickButton();
        quitPopup.SetActive(!quitPopup.activeSelf);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
