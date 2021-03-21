/*
 * File:        TrackUI.cs
 * Author:      Étienne Ménard
 * Description: Controls and displays track duration in-game on the pause menu.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TrackUI : MonoBehaviour
{
    [SerializeField] TracksManager tracksManager;

    [SerializeField] Button pauseTrackButton;
    private TextMeshProUGUI pauseButtonText;

    [SerializeField] TextMeshProUGUI time;
    private string min, sec, totalTime;
    private float trackTime;

    void Start()
    {
        pauseButtonText = pauseTrackButton.GetComponentInChildren<TextMeshProUGUI>();
        time.text = "00:00";
    }

    private void Update()
    {
        UpdateTrackTime();   
    }

    public void PauseTrack()
    {
        if (tracksManager.isPaused)
        {
            tracksManager.TogglePause();
            pauseButtonText.text = "Pause";
        }
        else
        {
            tracksManager.TogglePause();
            pauseButtonText.text = "Play";
        }
    }

    public void ResetPauseButton()
    {
        pauseButtonText.text = "Pause";
    }

    private void UpdateTrackTime()
    {
        int curTime = (int)tracksManager.musicSource.time;
        min = (curTime / 60).ToString();
        sec = (curTime % 60).ToString();

        if (curTime / 60 < 10)
            min = "0" + min;

        if (curTime % 60 < 10)
            sec = "0" + sec;

        time.text = min + ":" + sec + " / " + totalTime;
    }

    public void GetTotalTrackTime(float time)
    {
        string min, sec;
        trackTime = time;

        min = ((int)trackTime / 60).ToString();
        if (trackTime / 60 < 10)
            min = "0" + min;

        sec = ((int)tracksManager.musicSource.clip.length % 60).ToString();
        if (trackTime % 60 < 10)
            sec = "0" + sec;

        totalTime = min + ":" + sec;
    }
}
