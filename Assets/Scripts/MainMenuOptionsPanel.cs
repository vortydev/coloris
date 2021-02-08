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

    [Header("Tutorial")]
    [SerializeField] GameObject tutorialPanel;
    [SerializeField] Button tutorialButton;

    private void Awake()
    {
        visualPanel.SetActive(false);
        gamePanel.SetActive(false);
        tutorialPanel.SetActive(false);

        audioButton.interactable = false;
    }

    public void OnAudioClick()
    {
        audioPanel.SetActive(true);
        visualPanel.SetActive(false);
        gamePanel.SetActive(false);
        tutorialPanel.SetActive(false);

        audioButton.interactable = false;
        visualButton.interactable = true;
        gameButton.interactable = true;
        tutorialButton.interactable = true;
    }

    public void OnVisualClick()
    {
        audioPanel.SetActive(false);
        visualPanel.SetActive(true);
        gamePanel.SetActive(false);
        tutorialPanel.SetActive(false);

        audioButton.interactable = true;
        visualButton.interactable = false;
        gameButton.interactable = true;
        tutorialButton.interactable = true;
    }

    public void OnGameClick()
    {
        audioPanel.SetActive(false);
        visualPanel.SetActive(false);
        gamePanel.SetActive(true);
        tutorialPanel.SetActive(false);

        audioButton.interactable = true;
        visualButton.interactable = true;
        gameButton.interactable = false;
        tutorialButton.interactable = true;
    }

    public void OnTutorialClick()
    {
        audioPanel.SetActive(false);
        visualPanel.SetActive(false);
        gamePanel.SetActive(false);
        tutorialPanel.SetActive(true);

        audioButton.interactable = true;
        visualButton.interactable = true;
        gameButton.interactable = true;
        tutorialButton.interactable = false;
    }
}
