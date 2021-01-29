using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Track", menuName = "Track")]
public class TrackSO : ScriptableObject
{
    [Header("Track Data")]
    public AudioClip track;     // audio clip with the actual song
    public int trackNb;         // number of the track
    public string trackName;    // name of the track
    public string authorName;   // author of the track
}
