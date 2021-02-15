using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VoicelinesManager : MonoBehaviour
{
    [Header("Components")]
    private AudioSource audioSource;
    [SerializeField] TypeWriter radio;
    public TextMeshProUGUI radioText;
    public TextMeshProUGUI bodyText;

    [Header("Voicelines")]
    [SerializeField] VoicelineSO[] mainVoicelines;
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

    public void TypeMainMessage()
    {
        int rng = Random.Range(0, mainVoicelines.Length);
        radio.TypeText(radioText, mainVoicelines[rng].transcript);
    }

    public void PlayIntroduction()
    {
        radio.TypeText(radioText, introductionVoicelines[0].transcript);
        radio.TypeIntroText(bodyText, introductionVoicelines[1].transcript);
        // play audio
    }

    public void PlayControlsVoiceline(int ind)
    {
        radio.TypeControlsText(radioText, controlsVoicelines[ind - 1 ].transcript);
        // play audio
    }
}
