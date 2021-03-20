/*
 * File:        InputsGame.cs
 * Author:      Étienne Ménard
 * Description: Handles the Input Actions in-game.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputsGame : MonoBehaviour
{
    private MyControls _actions;

    private void Awake()
    {
        // create a new instance of the input actions before everything else
        _actions = new MyControls();
        LoadLoadouts(PlayerPrefsManager.GetIntPlayerPref(PlayerPrefsManager.keyboardLoadoutKEY, 0), PlayerPrefsManager.GetIntPlayerPref(PlayerPrefsManager.gamepadLoadoutKEY, 1));
    }

    private void LoadLoadouts(int keybLoadout, int gamepadLoadout)
    {
        string keybControls = "Default";
        string gamepadControls = "Gamepad";

        switch (keybLoadout)
        {
            case 0:
                keybControls = "Default";
                break;
            case 1:
                keybControls = "Vim";
                break;
            case 2:
                keybControls = "Gamer";
                break;
            case 3:
                keybControls = "Numpad";
                break;
        }

        switch (gamepadLoadout)
        {
            case 1:
                gamepadControls = "Gamepad";
                break;
        }

        if (Gamepad.all.Count > 0 && gamepadLoadout >= 1)
        {
            _actions.bindingMask = InputBinding.MaskByGroups(keybControls, gamepadControls);
        }
        else
        {
            _actions.bindingMask = InputBinding.MaskByGroup(keybControls);
        }
    }

    private void OnEnable()
    {
        // enable the input
        _actions.Enable();

        // piece movement
        _actions.Coloris.Move.performed += Move;
        _actions.Coloris.RotateRight.performed += RotateRight;
        _actions.Coloris.RotateLeft.performed += RotateLeft;
        _actions.Coloris.SoftDrop.performed += SoftDrop;
        _actions.Coloris.HardDrop.performed += HardDrop;
        _actions.Coloris.Hold.performed += Hold;

        // pause
        _actions.Coloris.Pause.performed += Pause;
    }

    private void OnDisable()
    {
        // disable the input
        _actions.Disable();

        _actions.Coloris.Move.performed -= Move;
        _actions.Coloris.RotateRight.performed -= RotateRight;
        _actions.Coloris.RotateLeft.performed -= RotateLeft;
        _actions.Coloris.SoftDrop.performed -= SoftDrop;
        _actions.Coloris.HardDrop.performed -= HardDrop;
        _actions.Coloris.Hold.performed -= Hold;

        _actions.Coloris.Pause.performed -= Pause;
    }

    private void Move(InputAction.CallbackContext obj)
    {
        var g = FindObjectOfType<Group>();

        if (g)
            g.OnMove((int)obj.ReadValue<float>());
    }

    private void RotateRight(InputAction.CallbackContext obj)
    {
        var g = FindObjectOfType<Group>();

        if (g)
            g.OnRotateRight();
    }

    private void RotateLeft(InputAction.CallbackContext obj)
    {
        var g = FindObjectOfType<Group>();

        if (g)
            g.OnRotateLeft();
    }

    private void SoftDrop(InputAction.CallbackContext obj)
    {
        var g = FindObjectOfType<Group>();

        if (g)
            g.OnSoftDrop();
    }

    private void HardDrop(InputAction.CallbackContext obj)
    {
        var g = FindObjectOfType<Group>();

        if (g)
            g.OnHardDrop();
    }

    private void Hold(InputAction.CallbackContext obj)
    {
        var g = FindObjectOfType<Group>();

        if (g)
            g.OnHold();
    }

    private void Pause(InputAction.CallbackContext obj)
    {
        var p = FindObjectOfType<PauseMenu>();

        if (p)
            p.OnClickPause();
    }
}
