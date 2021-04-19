/*
 * File:        TrackSO.cs
 * Author:      Étienne Ménard
 * Description: ScriptableObject that holds all the track's information.
 */

using UnityEngine;

[CreateAssetMenu(menuName = "Track", fileName = "New Track")]
public class TrackSO : ScriptableObject
{
    public AudioClip Clip;      // audio clip with the actual song
    public string TrackName;    // name of the track
    public string ArtistName;   // author of the track
    public int TrackNb;         // number of the track
    public int VolNb;           // volume number
}