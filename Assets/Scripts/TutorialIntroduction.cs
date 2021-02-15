using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialIntroduction : MonoBehaviour
{
    [SerializeField] VoicelinesManager voicelinesManager;
    [SerializeField] Button backButton;

    private void Awake()
    {
        backButton.gameObject.SetActive(false);
    }

    public void ToggleBackButton()
    {
        backButton.gameObject.SetActive(!backButton.gameObject.activeSelf);
    }

    public void ClickBack()
    {
        FindObjectOfType<TypeWriter>().EraseIntro(voicelinesManager.bodyText);
    }
}
