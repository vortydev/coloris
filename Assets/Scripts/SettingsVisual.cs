using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsVisual : MonoBehaviour
{
    [Header("Block pulsing")]
    [SerializeField] TMP_Dropdown pulseDropdown;    // off, current piece, all

    [Header("Screenshake")]
    [SerializeField] Screenshake screenshake;
    [SerializeField] TMP_Dropdown shakeDropdown;  // off, clear lined, all the time
    [SerializeField] Slider shakeSlider;
}