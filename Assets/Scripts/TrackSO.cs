using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Track", fileName = "New Track")]
public class TrackSO : ScriptableObject
{
    public AudioClip track;     // audio clip with the actual song
    public int trackNb;         // number of the track
    public int volNb;           // volume number
    public string trackName;    // name of the track
    public string authorName;   // author of the track
}
