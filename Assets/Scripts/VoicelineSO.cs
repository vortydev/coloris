using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Voiceline", fileName = "New Voiceline")]
public class VoicelineSO : ScriptableObject
{
    public AudioClip voiceline;
    public string voice;
    public string clipName;
    public string transcript;
}
