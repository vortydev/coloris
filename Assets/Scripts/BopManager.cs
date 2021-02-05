using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BopManager : MonoBehaviour
{
    public bool isEnabled = true;
    public static event Action<float> BopBlocks;
    private float _currentTime;

    [Header("Track Data")]
    public int _bpm;
    public int _offset;
    public float _startDelay;

    //public void GetCurTrack(TrackSO track)
    //{
    //    _bpm = track.bpm;
    //    _offset = track.offset;
    //    _startDelay = track.startDelay;
    //}

    private void Update()
    {
        if (_currentTime < _startDelay)
        {
            _currentTime += Time.deltaTime;
            return;
        }

        BopBlocks?.Invoke(Mathf.Cos((Time.time * Mathf.PI * _bpm / 60 + _offset / 60) % Mathf.PI));
    }
}
