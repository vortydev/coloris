using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    [Header("Components")]
    public AudioSource audioSource;             // component that plays the track
    private AudioController audioController;    // script that sets the audio's volume

    [Header("Movement")]
    [SerializeField] AudioClip hardDrop;
    [SerializeField] AudioClip rotate;
    [SerializeField] AudioClip moveSide;

    [Header("Effects")]
    [SerializeField] AudioClip clear;
    [SerializeField] AudioClip tetris;
    [SerializeField] AudioClip locking;

    [Header("UI")]
    [SerializeField] AudioClip holdPiece;
    [SerializeField] AudioClip pause;
    [SerializeField] AudioClip unpause;

    [Header("Settings")]
    public bool canMove = true;
    public bool canRotate = true;
    public bool canHardDrop = true;
    public bool canHoldPiece = true;

    private void Awake()
    {
        audioController = GetComponent<AudioController>();
    }

    private void UpdateVolume()
    {
        audioSource.volume = audioController.sfx / 10;
    }

    private void UpdateVolumePause()
    {
        audioSource.volume = (audioController.sfx / 2) / 10;
    }

    // MOVEMENT SFX
    public void RotateSFX()
    {
        UpdateVolume();

        if (canRotate)
        {
            audioSource.clip = rotate;
            audioSource.Play();
        }
    }

    public void MoveSideSFX()
    {
        UpdateVolume();

        if (canMove)
        {
            audioSource.clip = moveSide;
            audioSource.Play();
        }
    }

    public void HardDropSFX()
    {
        UpdateVolume();

        if (canHardDrop)
        {
            audioSource.clip = hardDrop;
            audioSource.Play();
        }
    }

    // EFFECTS SFX
    public void ClearSFX()
    {
        UpdateVolume();

        audioSource.clip = clear;
        audioSource.Play();
    }

    public void TetrisSFX()
    {
        UpdateVolume();

        audioSource.clip = tetris;
        audioSource.Play();
    }

    public void LockSFX()
    {
        UpdateVolume();

        audioSource.clip = locking;
        audioSource.Play();
    }

    // UI SFX
    public void HoldPieceSFX()
    {
        UpdateVolume();

        if (canHoldPiece)
        {
            audioSource.clip = holdPiece;
            audioSource.Play();
        }
    }

    public void PauseSFX()
    {
        UpdateVolumePause();

        audioSource.clip = pause;
        audioSource.Play();
    }

    public void UnpauseSFX()
    {
        UpdateVolumePause();

        audioSource.clip = unpause;
        audioSource.Play();
    }
}
