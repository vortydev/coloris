using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Track", menuName = "Track")]
public class TrackSO : ScriptableObject
{
    [Header("Track Data")]
    public AudioClip track;     // audio clip with the actual song
    public string trackName;    // name of the track
    public string authorName;   // author of the track

    [Header("Pulsing Data")]
    public int bpm;             // frequency of the bop
    public int offset;          // bopping offset
    public float startDelay;    // time in seconds before the bopping starts
}
