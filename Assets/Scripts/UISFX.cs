/*
 * File:        UISFX.cs
 * Author:      Étienne Ménard
 * Description: Little script to give sfx to UI elements.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class UISFX : MonoBehaviour
{
    [Header("Components")]
    private AudioSource _sfxSource;

    [Header("SFX")]
    [SerializeField] AudioClip uiClick;
    [SerializeField] AudioClip woosh;
    [SerializeField] AudioClip locking;
    [SerializeField] AudioClip move;
    [SerializeField] AudioClip hold;
    [SerializeField] AudioClip tetris;
    [SerializeField] AudioClip hardDrop;

    [Header("UI Elements")]
    [SerializeField] Toggle[] _toggles;                             // array of all the scene's toggles
    [SerializeField] TMP_Dropdown[] _dropdowns;                     // array of all the scene's dropdowns
    [SerializeField] List<Button> _buttons, _panelButtons, _menuButtons;    // arrays of all the scene's buttons

    private void Awake()
    {
        _sfxSource = GetComponent<AudioController>().sfxSource;
    }

    public void  LoadUIElements()
    {
        EmptyArrays();
        GetUIElements();
        LoadListeners();
    }

    private void GetUIElements()
    {
        _toggles = FindObjectsOfType<Toggle>();
        _dropdowns = FindObjectsOfType<TMP_Dropdown>();
        GetButtons();
    }

    // Gets all the buttons and then filters them into their appropriate list
    private void GetButtons()
    {
        Button[] allButtons = FindObjectsOfType<Button>();  // generates an array of all the Buttons in the scene

        for (int i = 0; i < allButtons.Length; i++)         // adds the button to its appropriate list depending of its tag
        {
            if (allButtons[i].tag == "UniqueUI")
            {
                continue;
            }
            else if (allButtons[i].tag == "PanelButton")
            {
                _panelButtons.Add(allButtons[i]);
            }
            else if (allButtons[i].tag == "MenuButton")
            {
                _menuButtons.Add(allButtons[i]);
            }
            else
            {
                _buttons.Add(allButtons[i]);
            }
        } 
    }

    private void LoadListeners()
    {
        // loads listeners for toggles
        for (int i = 0; i < _toggles.Length; i++)
        {
            if (_toggles[i].tag != "UniqueUI")
            {
                _toggles[i].onValueChanged.AddListener((value) => OnToggleClick());
            }
        }
        
        // loads listeners for dropdowns
        for (int i = 0; i < _dropdowns.Length; i++)
        {
            if (_dropdowns[i].tag != "UniqueUI")
            {
                _dropdowns[i].onValueChanged.AddListener((value) => OnDropdownClick());
            }
        }

        // loads listeners for regular buttons
        for (int i = 0; i < _buttons.Count; i++)
        {
            _buttons[i].onClick.AddListener(OnButtonClick);
        }

        // loads listeners for panel buttons
        for (int i = 0; i < _panelButtons.Count; i++)
        {
            _panelButtons[i].onClick.AddListener(OnPanelButtonClick);
        }

        // loads listeners for menu buttons
        for (int i = 0; i < _menuButtons.Count; i++)
        {
            _menuButtons[i].onClick.AddListener(OnMenuButtonClick);
        }
    }

    private void EmptyArrays()
    {
        if (_toggles.Length > 0)
            _toggles = new Toggle[0];

        if (_dropdowns.Length > 0)
            _dropdowns = new TMP_Dropdown[0];

        if (_buttons.Count > 0)
            _buttons.Clear();

        if (_panelButtons.Count > 0)
            _panelButtons.Clear();

        if (_menuButtons.Count > 0)
            _menuButtons.Clear();
    }

    private void PlayClip(AudioClip clip)
    {
        _sfxSource.clip = clip;
        _sfxSource.Play();
    }

    public void OnButtonClick() {
        PlayClip(uiClick);
    }

    public void OnPanelButtonClick()
    {
        PlayClip(woosh);
    }

    public void OnToggleClick()
    {
        PlayClip(locking);
    }

    public void OnDropdownClick()
    {
        PlayClip(move);
    }

    public void OnMenuButtonClick()
    {
        PlayClip(hold);
    }

    public void TetrisSFX()
    {
        PlayClip(tetris);
    }

    public void HardDropSFX()
    {
        PlayClip(hardDrop);
    }
}
