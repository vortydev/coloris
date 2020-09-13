using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu]
public class AudioTrack : ScriptableObject
{
    public AudioClip Clip;
    public float BPM;
    public float Offset;
    public float LeadInTime;
    public string Author;
}
