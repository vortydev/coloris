/*
 * File:        OptionsSFX.cs
 * Author:      Étienne Ménard
 * Description: Sound effects for the SFX options.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsSFX : MonoBehaviour
{
    private AudioSource _sfxSource;
    [SerializeField] AudioClip move;
    [SerializeField] AudioClip rotate;
    [SerializeField] AudioClip hardDrop;
    [SerializeField] AudioClip hold;
    [SerializeField] AudioClip locking;

    private void Awake()
    {
        _sfxSource = FindObjectOfType<AudioController>().sfxSource;
    }

    private void PlaySFX(AudioClip clip)
    {
        _sfxSource.clip = clip;
        _sfxSource.Play();
    }

    public void PlaySfxMove()
    {
        PlaySFX(move);
    }

    public void PlaySfxRotate()
    {
        PlaySFX(rotate);
    }

    public void PlaySfxHardDrop()
    {
        PlaySFX(hardDrop);
    }

    public void PlaySfxHold()
    {
        PlaySFX(hold);
    }

    public void PlaySfxLocking()
    {
        PlaySFX(locking);
    }
}
