using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timerText;

    private void Awake()
    {
        _timerText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void DisplayTrackDuration(int trackLength)
    {
        string min = (trackLength / 60).ToString();
        string sec = (trackLength % 60).ToString();

        if (trackLength / 60 < 10)
            min = "0" + min;

        if (trackLength % 60 < 10)
            sec = "0" + sec;

        _timerText.text = "Time:\n" + min + ":" + sec;
    }
}
