using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Settings : MonoBehaviour
{
    [Header("Audio Settings")]
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;

    [Header("Game Settings")]


    [Header("Visual Settings")]
    [SerializeField] Toggle gridToggle;
    [SerializeField] Toggle nextPieceToggle;
}
