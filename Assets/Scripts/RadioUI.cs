using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RadioUI : MonoBehaviour
{
    [Header("Text Elements")]
    public TextMeshProUGUI trackName;
    public TextMeshProUGUI trackAuthor;

    public void DisplayTrackInfo(string name, string author)
    {
        trackName.text = name;
        trackAuthor.text = author;
    }
}
