/*
 * File:        CanDo.cs
 * Author:      �tienne M�nard
 * Description: Simple little container for some global bools, I don't want to bother reading from disk all the time.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanDo : MonoBehaviour
{
    public bool canHold;        // pieces can be held
    public bool canHardDrop;    // pieces can be hard dropped
    public bool canGhost;       // pieces can spawn a ghost

    public int lockDelay;       // delay before the piece locks in place

    public int cellFace;        // sets the blocks' face sprite

    private void Awake()
    {
        lockDelay = PlayerPrefsManager.GetIntPlayerPref(PlayerPrefsManager.lockDelayKEY, 5);
        cellFace = PlayerPrefsManager.GetIntPlayerPref(PlayerPrefsManager.cellFaceKEY, 0);
    }

    public string GetLockDelayString()
    {
        switch (lockDelay)
        {
            case 0:
                return "0s";
            case 1:
                return "0.1s";
            case 2:
                return "0.2s";
            case 3:
                return "0.3s";
            case 4:
                return "0.4s";
            case 5:
                return "0.5s";
        }

        return null;
    }

    public void ToggleCanGhost()
    {
        canGhost = !canGhost;

        if (canGhost)
        {
            FindObjectOfType<Group>().SpawnGhost();
        }
        else
        {
            FindObjectOfType<Group>().DestroyGhost();
        }
    }
}