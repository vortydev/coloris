using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISFX : MonoBehaviour
{
    [Header("Components")]
    public AudioSource audioSource;
    private AudioController audioController;

    [Header("SFX")]
    [SerializeField] AudioClip clickButton;
    [SerializeField] AudioClip changePanel;
    [SerializeField] AudioClip clickToggle;
    [SerializeField] AudioClip clickDropdown;
    [SerializeField] AudioClip clickMenuButton;

    [Header("Unique")]
    [SerializeField] AudioClip clickCredits;
    [SerializeField] AudioClip clickQuit;

    private void Awake()
    {
        audioController = GetComponent<AudioController>();
    }

    private void Update()
    {
        audioSource.volume = audioController.sfx / 10;
    }

    public void ClickButton() {
        audioSource.clip = clickButton;
        audioSource.Play();
    }

    public void ChangePanel()
    {
        audioSource.clip = changePanel;
        audioSource.Play();
    }

    public void ClickToggle()
    {
        audioSource.clip = clickToggle;
        audioSource.Play();
    }

    public void ClickDropdown()
    {
        audioSource.clip = clickDropdown;
        audioSource.Play();
    }

    public void ClickMenuButton()
    {
        audioSource.clip = clickMenuButton;
        audioSource.Play();
    }

    public void ClickCredits()
    {
        audioSource.clip = clickCredits;
        audioSource.Play();
    }

    public void ClickQuit()
    {
        audioSource.clip = clickQuit;
        audioSource.Play();
    }
}
