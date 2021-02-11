using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialsManager : MonoBehaviour
{
    public string mainMessage;

    [Header("Components")]
    [SerializeField] VoicelinesManager voicelinesManager;
    [SerializeField] GameObject mainButtons;
    [SerializeField] TextMeshProUGUI pageTitle;
    [SerializeField] GameObject holdPieceUI;

    [Header("Controls")]
    [SerializeField] GameObject controlsPage;

    private void Start()
    {
        controlsPage.SetActive(false);
    }

    public void BackToMain()
    {
        mainButtons.SetActive(true);
        controlsPage.SetActive(false);
        holdPieceUI.SetActive(false);
        holdPieceUI.GetComponentInChildren<Transform>().gameObject.SetActive(false);

        pageTitle.text = "Tutorials";
        pageTitle.fontStyle = FontStyles.Underline;

        FindObjectOfType<TypeWriter>().TypeTranscript(mainMessage);
    }

    public void OpenControls()
    {
        pageTitle.text = "Controls";
        pageTitle.fontStyle = FontStyles.Normal;

        mainButtons.SetActive(false);
        controlsPage.SetActive(true);

        FindObjectOfType<TutorialControls>().UpdateControlsPage();
    }
}
