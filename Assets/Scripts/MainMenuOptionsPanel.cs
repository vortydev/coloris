using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuOptionsPanel : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] GameObject audioPanel;
    [SerializeField] Button audioButton;

    [Header("Visual")]
    [SerializeField] GameObject visualPanel;
    [SerializeField] Button visualButton;

    [Header("Game")]
    [SerializeField] GameObject gamePanel;
    [SerializeField] Button gameButton;

    [Header("Extras")]
    [SerializeField] GameObject extrasPanel;
    [SerializeField] Button extrasButton;

    private void Awake()
    {
        visualPanel.SetActive(false);
        gamePanel.SetActive(false);
        extrasPanel.SetActive(false);

        audioButton.interactable = false;
    }

    public void OnAudioClick()
    {
        audioPanel.SetActive(true);
        visualPanel.SetActive(false);
        gamePanel.SetActive(false);
        extrasPanel.SetActive(false);

        audioButton.interactable = false;
        visualButton.interactable = true;
        gameButton.interactable = true;
        extrasButton.interactable = true;
    }

    public void OnVisualClick()
    {
        audioPanel.SetActive(false);
        visualPanel.SetActive(true);
        gamePanel.SetActive(false);
        extrasPanel.SetActive(false);

        audioButton.interactable = true;
        visualButton.interactable = false;
        gameButton.interactable = true;
        extrasButton.interactable = true;
    }

    public void OnGameClick()
    {
        audioPanel.SetActive(false);
        visualPanel.SetActive(false);
        gamePanel.SetActive(true);
        extrasPanel.SetActive(false);

        audioButton.interactable = true;
        visualButton.interactable = true;
        gameButton.interactable = false;
        extrasButton.interactable = true;
    }

    public void OnTutorialClick()
    {
        audioPanel.SetActive(false);
        visualPanel.SetActive(false);
        gamePanel.SetActive(false);
        extrasPanel.SetActive(true);

        audioButton.interactable = true;
        visualButton.interactable = true;
        gameButton.interactable = true;
        extrasButton.interactable = false;
    }
}
