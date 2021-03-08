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
    [SerializeField] AudioSource sfxSource;
    [SerializeField] AudioClip move;
    [SerializeField] AudioClip rotate;
    [SerializeField] AudioClip hardDrop;
    [SerializeField] AudioClip hold;

    private void PlaySFX(AudioClip clip)
    {
        sfxSource.clip = clip;
        sfxSource.Play();
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
}
