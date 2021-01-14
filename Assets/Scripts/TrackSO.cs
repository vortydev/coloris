using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Track", menuName = "Track")]
public class TrackSO : ScriptableObject
{
    [Header("Track Data")]
    public AudioClip track;
    public string trackName;
    public string authorName;

    [Header("Pulsing Data")]
    public int bpm;
    public int offset;
}
