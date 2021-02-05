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
