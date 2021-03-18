/*
 * File:        MainMenuOptionsPanel.cs
 * Author:      Étienne Ménard
 * Description: Handles the options panels of the main menu.
 */

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

    [Header("SFX")]
    [SerializeField] GameObject sfxPanel;
    [SerializeField] Button sfxButton;

    [Header("Keybinds")]
    [SerializeField] GameObject keybindsPanel;
    [SerializeField] Button keybindsButton;

    [Header("Extras")]
    [SerializeField] GameObject extrasPanel;
    [SerializeField] Button extrasButton;

    private void Awake()
    {
        visualPanel.SetActive(false);
        gamePanel.SetActive(false);
        sfxPanel.SetActive(false);
        keybindsPanel.SetActive(false);
        extrasPanel.SetActive(false);

        audioButton.interactable = false;
    }

    public void OnAudioClick()
    {
        audioPanel.SetActive(true);
        audioButton.interactable = false;

        visualPanel.SetActive(false);
        visualButton.interactable = true;

        gamePanel.SetActive(false);
        gameButton.interactable = true;

        sfxPanel.SetActive(false);
        sfxButton.interactable = true;

        keybindsPanel.SetActive(false);
        keybindsButton.interactable = true;

        extrasPanel.SetActive(false);
        extrasButton.interactable = true;
    }

    public void OnVisualClick()
    {
        audioPanel.SetActive(false);
        audioButton.interactable = true;

        visualPanel.SetActive(true);
        visualButton.interactable = false;

        gamePanel.SetActive(false);
        gameButton.interactable = true;

        sfxPanel.SetActive(false);
        sfxButton.interactable = true;

        keybindsPanel.SetActive(false);
        keybindsButton.interactable = true;

        extrasPanel.SetActive(false);
        extrasButton.interactable = true;
    }

    public void OnGameClick()
    {
        audioPanel.SetActive(false);
        audioButton.interactable = true;

        visualPanel.SetActive(false);
        visualButton.interactable = true;

        gamePanel.SetActive(true);
        gameButton.interactable = false;

        sfxPanel.SetActive(false);
        sfxButton.interactable = true;

        keybindsPanel.SetActive(false);
        keybindsButton.interactable = true;

        extrasPanel.SetActive(false);
        extrasButton.interactable = true;
    }

    public void OnSfxClick()
    {
        audioPanel.SetActive(false);
        audioButton.interactable = true;

        visualPanel.SetActive(false);
        visualButton.interactable = true;

        gamePanel.SetActive(false);
        gameButton.interactable = true;

        sfxPanel.SetActive(true);
        sfxButton.interactable = false;

        keybindsPanel.SetActive(false);
        keybindsButton.interactable = true;

        extrasPanel.SetActive(false);
        extrasButton.interactable = true;
    }

    public void OnKeybindsClick()
    {
        audioPanel.SetActive(false);
        audioButton.interactable = true;

        visualPanel.SetActive(false);
        visualButton.interactable = true;

        gamePanel.SetActive(false);
        gameButton.interactable = true;

        sfxPanel.SetActive(false);
        sfxButton.interactable = true;

        keybindsPanel.SetActive(true);
        keybindsButton.interactable = false;

        extrasPanel.SetActive(false);
        extrasButton.interactable = true;
    }

    public void OnExtrasClick()
    {
        audioPanel.SetActive(false);
        audioButton.interactable = true;

        visualPanel.SetActive(false);
        visualButton.interactable = true;

        gamePanel.SetActive(false);
        gameButton.interactable = true;

        sfxPanel.SetActive(false);
        sfxButton.interactable = true;

        keybindsPanel.SetActive(false);
        keybindsButton.interactable = true;

        extrasPanel.SetActive(true);
        extrasButton.interactable = false;
    }
}
