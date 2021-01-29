using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
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

    private void Awake()
    {
        // if the playerprefs dont exist yet, set them
        if (!PlayerPrefs.HasKey("visualiser"))
        {
            // visual settings
            PlayerPrefs.SetInt("visualiser", 1);
            PlayerPrefs.SetInt("screenshake", 1);

            // game settings
            PlayerPrefs.SetInt("game_grid", 1);
            PlayerPrefs.SetInt("score", 1);
            PlayerPrefs.SetInt("next_piece", 1);

            PlayerPrefs.Save();
        }
    }

    private void Start()
    {
        visualPanel.SetActive(false);
        gamePanel.SetActive(false);

        audioButton.interactable = false;
    }

    public void OnAudioClick()
    {
        audioPanel.SetActive(true);
        visualPanel.SetActive(false);
        gamePanel.SetActive(false);

        audioButton.interactable = false;
        visualButton.interactable = true;
        gameButton.interactable = true;
    }

    public void OnVisualClick()
    {
        audioPanel.SetActive(false);
        visualPanel.SetActive(true);
        gamePanel.SetActive(false);

        audioButton.interactable = true;
        visualButton.interactable = false;
        gameButton.interactable = true;
    }

    public void OnGameClick()
    {
        audioPanel.SetActive(false);
        visualPanel.SetActive(false);
        gamePanel.SetActive(true);

        audioButton.interactable = true;
        visualButton.interactable = true;
        gameButton.interactable = false;
    }

    public static void ToggleBoolPlayerPrefs(string key)
    {
        if (PlayerPrefs.GetInt(key) == 0)
            PlayerPrefs.SetInt(key, 1);
        else
            PlayerPrefs.SetInt(key, 0);

        PlayerPrefs.Save();
    }

    public static void SaveFloatPlayerPrefs(string key, float val)
    {
        PlayerPrefs.SetFloat(key, val);
        PlayerPrefs.Save();
    }
}