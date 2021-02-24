using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypeWriter : MonoBehaviour
{
    [Header("Text Settings")]
    public int textSpeed;                       // speed level from PlayerPrefs
    public float typingDelay = 0.05f;           // speed at which the text is typed
    public float eraseDelay = 0.025f;           // speed at which the text is being erased
    public bool dynamicText;                    // if the text is dynamic or not

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
            case 1:
                typingDelay = 0.075f;
                break;
            case 2:
                typingDelay = 0.05f;
                break;
            case 3:
                typingDelay = 0.025f;
                break;
        }
    }

    private void SetText(TextMeshProUGUI t, string s)
    {
        t.text = s;
    }

    public void TypeText(TextMeshProUGUI t, string s)
    {
        if (dynamicText)
        {
            StartCoroutine(ShowText(t, s));
        }
        else
        {
            SetText(t, s);
        }
    }

    private IEnumerator ShowText(TextMeshProUGUI t, string s)
    {
        string curtext;

        for (int i = 0; i < s.Length + 1; i++)
        {
            curtext = s.Substring(0, i);
            t.text = curtext;

            yield return new WaitForSeconds(typingDelay);
        }
    }

    public void TypeIntroText(TextMeshProUGUI t, string s)
    {
        if (dynamicText)
        {
            StartCoroutine(ShowIntroText(t, s));
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

        for (int i = 0; i < s.Length + 1; i++)
        {
            curtext = s.Substring(0, i);
            t.text = curtext;

            yield return new WaitForSeconds(typingDelay);

            if (i > 0 && (s[i - 1] == '.' || s[i - 1] == '!'))
            {
                yield return new WaitForSeconds(0.5f);
            }
        }

        yield return new WaitForSeconds(1);
        FindObjectOfType<TutorialIntroduction>().ToggleBackButton();
    }

    private IEnumerator EraseIntroText(TextMeshProUGUI t)
    {
        string s = t.text, curText;

        for (int i = s.Length; i > -1; i--)
        {
            curText = s.Substring(0, i);
            t.text = curText;

            yield return new WaitForSeconds(eraseDelay / 2);
        }

        FindObjectOfType<TutorialsManager>().BackToMain();
    }

    public void TypeControlsText(TextMeshProUGUI t, string s)
    {
        if (dynamicText)
        {
            StartCoroutine(ShowControlsText(t, s));
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

        for (int i = 0; i < s.Length + 1; i++)
        {
            curtext = s.Substring(0, i);
            t.text = curtext;

            yield return new WaitForSeconds(typingDelay);
        }

        FindObjectOfType<TutorialControls>().UpdateNavButtons();
    }
}
