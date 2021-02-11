using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoicelinesManager : MonoBehaviour
{
    [Header("Components")]
    private AudioSource audioSource;
    [SerializeField] TypeWriter radio;

    [Header("Voicelines")]
    [SerializeField] VoicelineSO[] introductionVoicelines;
    [SerializeField] VoicelineSO[] controlsVoicelines;
    [SerializeField] VoicelineSO[] customizabilityVoicelines;

    [Header("Settings")]
    [SerializeField] bool muted;
    [Range(1,2)] [SerializeField] int selectedVoice;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();   
    }

    public void PlayControlsVoiceline(int ind)
    {
        // play audio
        radio.TypeControlsText(controlsVoicelines[ind - 1 ].transcript);
    }
}
