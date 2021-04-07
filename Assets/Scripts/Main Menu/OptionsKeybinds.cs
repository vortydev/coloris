/*
 * File:        OptionsKeybinds.cs
 * Author:      Étienne Ménard
 * Description: Handles and displays the selected loadout to play Coloris. Don't judge this terribly janky code pwease, it works and that's what matters aye?
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class OptionsKeybinds : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] TMP_Dropdown keyboardDropdown;
    [SerializeField] TMP_Dropdown gamepadDropdown;
    [SerializeField] Button viewKeybButton;
    [SerializeField] Button viewGamepadButton;

    [Header("Display")]
    [SerializeField] GameObject bindView;
    [SerializeField] TextMeshProUGUI moveLeft;
    [SerializeField] TextMeshProUGUI moveRight;
    [SerializeField] TextMeshProUGUI rotateLeft;
    [SerializeField] TextMeshProUGUI rotateRight;
    [SerializeField] TextMeshProUGUI softDrop;
    [SerializeField] TextMeshProUGUI hardDrop;
    [SerializeField] TextMeshProUGUI hold;
    [SerializeField] TextMeshProUGUI pause;

    private void Start()
    {
        keyboardDropdown.SetValueWithoutNotify(PlayerPrefsManager.GetIntPlayerPref(PlayerPrefsManager.keyboardLoadoutKEY, 0));
        gamepadDropdown.SetValueWithoutNotify(PlayerPrefsManager.GetIntPlayerPref(PlayerPrefsManager.gamepadLoadoutKEY, 1));

        if (Gamepad.all.Count <= 0)
        {
            gamepadDropdown.interactable = false;
            viewGamepadButton.interactable = false;
            gamepadDropdown.SetValueWithoutNotify(0);
        }

        bindView.SetActive(false);
    }

    public void UpdateKeyboardLoadout()
    {
        PlayerPrefsManager.SaveIntPlayerPref(PlayerPrefsManager.keyboardLoadoutKEY, keyboardDropdown.value);
        DisplayBinds(1);
    }

    public void UpdateGamepadLoadout()
    {
        PlayerPrefsManager.SaveIntPlayerPref(PlayerPrefsManager.gamepadLoadoutKEY, gamepadDropdown.value);
        DisplayBinds(2);
    }

    public void DisplayBinds(int view)
    {
        if (!bindView.activeSelf)
            bindView.SetActive(true);

        switch (view)
        {
            case 0:     // disable view
                SetButtonState(viewKeybButton, false);
                SetButtonState(viewGamepadButton, false);
                bindView.SetActive(false);
                break;

            case 1:     // keyboard loadout
                SetButtonState(viewKeybButton, false);
                SetButtonState(viewGamepadButton, true);

                switch (keyboardDropdown.value)
                {
                    case 0: // default
                        moveLeft.text = "Left arrow";
                        moveRight.text = "Right arrow";
                        rotateLeft.text = "Z, Left Ctrl";
                        rotateRight.text = "Up arrow, X";
                        softDrop.text = "Down arrow";
                        hardDrop.text = "Space";
                        hold.text = "C, Left Shift";
                        pause.text = "Esc";
                        break;

                    case 1: // vim
                        moveLeft.text = "H";
                        moveRight.text = "L, X";
                        rotateLeft.text = "Z";
                        rotateRight.text = "K";
                        softDrop.text = "J";
                        hardDrop.text = "Space";
                        hold.text = "C";
                        pause.text = "Esc";
                        break;

                    case 2: // gamer
                        moveLeft.text = "A";
                        moveRight.text = "D";
                        rotateLeft.text = "n/a";
                        rotateRight.text = "W";
                        softDrop.text = "S";
                        hardDrop.text = "Space";
                        hold.text = "E";
                        pause.text = "Esc";
                        break;
                }

                break;

            case 2:     // gamepad loadout
                SetButtonState(viewKeybButton, true);
                SetButtonState(viewGamepadButton, false);

                switch (gamepadDropdown.value)
                {
                    case 0: // disabled
                        DisplayBinds(1);
                        break;

                    case 1: // default
                        moveLeft.text = "D-Pad: Left";
                        moveRight.text = "D-Pad: Right";
                        rotateLeft.text = "A";
                        rotateRight.text = "B";
                        softDrop.text = "D-Pad: Down";
                        hardDrop.text = "D-Pad: Up";
                        hold.text = "X, RB";
                        pause.text = "Start";
                        break;
                }

                break;
        }
    }

    private void SetButtonState(Button b, bool state)
    {
        if (state)
        {
            b.GetComponentInChildren<TextMeshProUGUI>().text = "View";
        }
        else
        {
            b.GetComponentInChildren<TextMeshProUGUI>().text = "Viewing";
        }
    }
}
