using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VoicelinesManager : MonoBehaviour
{
    [Header("Components")]
    public AudioSource audioSource;
    [SerializeField] TypeWriter radio;
    public TextMeshProUGUI radioText;
    public TextMeshProUGUI bodyText;

    [Header("Voicelines")]
    [SerializeField] VoicelineSO[] mainVoicelines;
    [SerializeField] VoicelineSO[] introductionVoicelines;
    [SerializeField] VoicelineSO[] controlsVoicelines;

    [Header("Settings")]
    public bool paused;
    public int selectedVoice;

    private void Awake()
    {
        selectedVoice = PlayerPrefsManager.GetIntPlayerPref(PlayerPrefsManager.selectedVoiceKEY, 0);
    }

    private void OnApplicationFocus()
    {
        TogglePause();
    }

    private void TogglePause()
    {
        if (paused)
        {
            audioSource.UnPause();
            paused = false;
        }
        else
        {
            audioSource.Pause();
            paused = true;
        }
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
        PlayVoicelineClip(introductionVoicelines[1]);
    }

    public void PlayControlsVoiceline(int ind)
    {
        radio.TypeControlsText(radioText, controlsVoicelines[ind - 1 ].transcript);
        PlayVoicelineClip(controlsVoicelines[ind - 1]);
    }

    private AudioClip GetSelectedVoiceAudioClip(VoicelineSO voiceline)
    {
        switch (selectedVoice)
        {
            case 0:
                return voiceline.varianVoiceline;
            case 1:
                return voiceline.ethelVoiceline;
        }
        return null;
    }

    private void PlayVoicelineClip(VoicelineSO voiceline)
    {
        if (audioSource.isPlaying)  // interrupts the previous audio clip
            audioSource.Stop();

        audioSource.clip = GetSelectedVoiceAudioClip(voiceline);
        audioSource.Play();
    }
}
