using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoicelinesManager : MonoBehaviour
{
    [SerializeField] TypeWriter radio;
    [SerializeField] VoicelineSO[] controlsVoicelines;

    public void PlayControlsVoiceline(int ind)
    {
        // play audio
        radio.TypeControlsText(controlsVoicelines[ind - 1 ].transcript);
    }
}
