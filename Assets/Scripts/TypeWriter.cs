using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypeWriter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI trackName;
    [SerializeField] TextMeshProUGUI trackAuthor;
    [SerializeField] TextMeshProUGUI radioText;
    public int textSpeed;
    public float typingDelay = 0.05f;            // speed at which the text is typed
    public float eraseDelay = 0.025f;           // speed at which the text is being erased

    public bool dynamicText;
    private string fullText = "";
    private string currentText = "";

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

    public void SetTranscript(string s)
    {
        fullText = s;
        radioText.text = fullText;
    }

    public void TypeTranscript(string s)
    {
        fullText = s;

        if (dynamicText)
        {
            StartCoroutine(ShowText());
        }
        else
        {
            SetTranscript(s);
        }
    }

    private IEnumerator ShowText()
    {
        for (int i = 0; i < fullText.Length + 1; i++)
        {
            currentText = fullText.Substring(0, i);
            radioText.text = currentText;

            yield return new WaitForSeconds(typingDelay);
        }
    }

    public void SetTrack(string n, string a)
    {
        trackName.text = n;
        trackAuthor.text = a;
    }

    public void TypeTrack(string n, string a)
    {
        if (dynamicText)
        {
            StartCoroutine(ShowTrack(trackName, n));
            StartCoroutine(ShowTrack(trackAuthor, a));
        }
        else
        {
            SetTrack(n, a);
        }
    }

    private IEnumerator ShowTrack(TextMeshProUGUI t, string s)
    {
        string curText = "";

        for (int i = 0; i < s.Length + 1; i++)
        {
            curText = s.Substring(0, i);
            t.text = curText;

            yield return new WaitForSeconds(typingDelay);
        }
    }

    public void TypeControlsText(string s)
    {
        fullText = s;

        if (dynamicText)
        {
            StartCoroutine(ShowControlsText());
        }
        else
        {
            SetTranscript(s);
        }
    }

    private IEnumerator ShowControlsText()
    {
        for (int i = 0; i < fullText.Length + 1; i++)
        {
            currentText = fullText.Substring(0, i);
            radioText.text = currentText;

            yield return new WaitForSeconds(typingDelay);
        }

        FindObjectOfType<TutorialControls>().UpdateNavButtons();
    }
}
