/*
 * File:        TypeWriter.cs
 * Author:      Étienne Ménard
 * Description: Script for a type writer effect with text.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypeWriter : MonoBehaviour
{
    [Header("Text Settings")]
    public int textSpeed;                       // speed level from PlayerPrefs
    public float typingDelay = 0.05f;           // speed at which the text is typed
    public bool dynamicText;                    // if the text is dynamic or not

    private IEnumerator routine;
    private TextMeshProUGUI currentBody;

    private struct typedString
    {
        private TextMeshProUGUI t;
        private string s;
    }
    private List<typedString> queue;

    private void Awake()
    {
        dynamicText = PlayerPrefsManager.GetBoolPlayerPref(PlayerPrefsManager.dynamicTextKEY);
        textSpeed = PlayerPrefsManager.GetIntPlayerPref(PlayerPrefsManager.textSpeedKEY, 2);
        UpdateTypingDelay(textSpeed);
    }

    public void UpdateTypingDelay(int speed)
    {
        switch (speed)
        {
            case 0:
                typingDelay = 0.075f;
                break;
            case 1:
                typingDelay = 0.05f;
                break;
            case 2:
                typingDelay = 0.025f;
                break;
        }
    }

    private void SetText(TextMeshProUGUI t, string s)
    {
        t.text = s;
        routine = null;
    }

    private void AddToQueue(TextMeshProUGUI t, string s)
    {

    }

    public void TypeText(TextMeshProUGUI t, string s)
    {
        if (dynamicText)
        {
            if (routine != null && t == currentBody)
            {
                StopCoroutine(routine);
                t.text = "";
            }

            routine = ShowText(t, s);
            StartCoroutine(routine);

        }
        else
        {
            SetText(t, s);
        }
    }

    private IEnumerator ShowText(TextMeshProUGUI t, string s = "")
    {
        string curtext;
        currentBody = t;

        for (int i = 0; i < s.Length + 1; i++)
        {
            curtext = s.Substring(0, i);
            t.text = curtext;

            yield return new WaitForSeconds(typingDelay);
        }

        routine = null;
        currentBody = null;
    }

    public void TypeIntroText(TextMeshProUGUI t, string s)
    {
        if (dynamicText)
        {
            if (routine != null && t == currentBody)
            {
                StopCoroutine(routine);
                t.text = "";
            }

            routine = ShowIntroText(t, s);
            StartCoroutine(routine);
        }
        else
        {
            SetText(t, s);
            FindObjectOfType<TutorialIntroduction>().ToggleBackButton();
        }
    }

    private IEnumerator ShowIntroText(TextMeshProUGUI t, string s)
    {
        string curtext;
        currentBody = t;

        for (int i = 0; i < s.Length + 1; i++)
        {
            curtext = s.Substring(0, i);
            t.text = curtext;

            yield return new WaitForSeconds(typingDelay / 1.25f);

            if (i > 0 && (s[i - 1] == '.' || s[i - 1] == '!'))
            {
                yield return new WaitForSeconds(0.5f);
            }
        }

        yield return new WaitForSeconds(1);
        FindObjectOfType<TutorialIntroduction>().ToggleBackButton();

        routine = null;
        currentBody = null;
    }

    public void TypeControlsText(TextMeshProUGUI t, string s)
    {
        if (dynamicText)
        {
            if (routine != null && t == currentBody)
            {
                StopCoroutine(routine);
                t.text = "";
            }

            routine = ShowControlsText(t, s);
            StartCoroutine(routine);
        }
        else
        {
            SetText(t, s);
            FindObjectOfType<TutorialControls>().UpdateNavButtons();
        }
    }

    private IEnumerator ShowControlsText(TextMeshProUGUI t, string s)
    {
        string curtext;
        currentBody = t;

        for (int i = 0; i < s.Length + 1; i++)
        {
            curtext = s.Substring(0, i);
            t.text = curtext;

            yield return new WaitForSeconds(typingDelay);
        }

        FindObjectOfType<TutorialControls>().UpdateNavButtons();

        routine = null;
        currentBody = null;
    }
}
