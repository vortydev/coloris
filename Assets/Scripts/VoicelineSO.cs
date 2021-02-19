using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Voiceline", fileName = "New Voiceline")]
public class VoicelineSO : ScriptableObject
{
    public AudioClip ethelVoiceline;
    public AudioClip varianVoiceline;
    public string clipName;
    public string transcript;
}
