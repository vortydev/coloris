/*
 * File:        SFXManager.cs
 * Author:      Étienne Ménard
 * Description: Handles game SFX.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSFX : MonoBehaviour
{
    [Header("Components")]
    private AudioController audioController;    // script that sets the audio's volume
    private AudioSource _sfxSource;             // component that plays the sfx

    [Header("Movement")]
    [SerializeField] AudioClip hardDrop;
    [SerializeField] AudioClip rotate;
    [SerializeField] AudioClip move;

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
        audioController = FindObjectOfType<AudioController>();
    }

    private void Start()
    {
        _sfxSource = audioController.sfxSource;
    }

    private void UpdateVolume()
    {
        _sfxSource.volume = audioController.sfx / 10;
    }

    private void UpdateVolumePause()
    {
        _sfxSource.volume = (audioController.sfx / 2) / 10;
    }

    private void PlayClip(AudioClip clip, bool enabled = true)
    {
        if (enabled)
        {
            UpdateVolume();
            _sfxSource.clip = clip;
            _sfxSource.Play();
        }
    }

    // MOVEMENT SFX
    public void RotateSFX()
    {
        PlayClip(rotate, canRotate);
    }

    public void MoveSideSFX()
    {
        PlayClip(move, canMove);
    }

    public void HardDropSFX()
    {
        PlayClip(hardDrop, canHardDrop);
    }

    // EFFECTS SFX
    public void ClearSFX()
    {
        PlayClip(clear);
    }

    public void TetrisSFX()
    {
        PlayClip(tetris);
    }

    public void LockSFX()
    {
        PlayClip(locking);
    }

    // UI SFX
    public void HoldPieceSFX()
    {
        PlayClip(holdPiece, canHoldPiece);
    }

    public void PauseSFX()
    {
        UpdateVolumePause();

        _sfxSource.clip = pause;
        _sfxSource.Play();

        UpdateVolume();
    }

    public void UnpauseSFX()
    {
        UpdateVolumePause();

        _sfxSource.clip = unpause;
        _sfxSource.Play();

        UpdateVolume();
    }
}
