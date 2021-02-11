using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypeWriter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI radioText;
    public float typingDelay = 0.05f;            // speed at which the text is typed
    public float eraseDelay = 0.025f;           // speed at which the text is being erased

    private string fullText = "";
    private string currentText = "";

    private void Awake()
    {
        typingDelay = PlayerPrefsManager.GetFloatPlayerPref(PlayerPrefsManager.textSpeedKEY, 0.05f);
    }

    private void Start()
    {
        SetTranscript(FindObjectOfType<TutorialsManager>().mainMessage);
    }

    public void SetTranscript(string s)
    {
        fullText = s;
        radioText.text = fullText;
    }

    public void TypeTranscript(string s)
    {
        fullText = s;
        StartCoroutine(ShowText());
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

    public void TypeControlsText(string s)
    {
        fullText = s;
        StartCoroutine(ShowControlsText());
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
