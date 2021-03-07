/*
 * File:        AudioVisualizer.cs
 * Author:      Étienne Ménard
 * Description: Little script attached to a simple sprite that gets scaled using the samples from AudioPeer.cs.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioVisualizer : MonoBehaviour
{
    public int band;
    public float initialScale, scaleMultiplier;

    void Start()
    {
        initialScale = transform.localScale.y;
    }

    void Update()
    {
        transform.localScale = new Vector2(transform.localScale.x, (AudioPeer._bandBuffer[band] * scaleMultiplier) + initialScale);
    }
}
