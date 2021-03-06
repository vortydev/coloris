/*
 * File:        TutorialIntroduction.cs
 * Author:      �tienne M�nard
 * Description: Small script for some useful methods for the Introduction panel in the tutorials.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialIntroduction : MonoBehaviour
{
    [SerializeField] Button backButton;

    private void Start()
    {
        backButton.gameObject.SetActive(false);
    }

    public void ToggleBackButton()
    {
        backButton.gameObject.SetActive(!backButton.gameObject.activeSelf);
    }

    public void ClickBack()
    {
        FindObjectOfType<TutorialsManager>().BackToMain();
    }
}
