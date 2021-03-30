/* File:        ReferenceTracks.cs
 * Author:      Étienne Ménard
 * Description: Persistent reference array of the tracks in the game.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ReferenceTracks : MonoBehaviour
{
    private static ReferenceTracks _instance;   // instance of the Loaded Tracks object
    public TrackSO[] refTracks;                 // array of the tracks made for Coloris

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject); // makes the array persistent across scenes
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Returns the TrackSO array
    public TrackSO[] GetRefArray()
    {
        return refTracks;
    }

    // Returns the TrackSO object
    public TrackSO GetTrackSO(int ind)
    {
        return refTracks[ind];
    }

    public int GetArraySize()
    {
        return refTracks.Length;
    }

    // Returns the selected TrackSO's track number
    public int GetTrackNb(int ind)
    {
        return refTracks[ind].trackNb;
    }

    // Returns the selected TrackSO' volume number
    public int GetVolumeNb(int ind)
    {
        return refTracks[ind].volNb;
    }

    // Returns the selected TrackSO's track name
    public string GetTrackName(int ind)
    {
        return refTracks[ind].trackName;
    }

    // Returns the selected TrackSO artist's name
    public string GetArtistName(int ind)
    {
        return refTracks[ind].authorName;
    }
}