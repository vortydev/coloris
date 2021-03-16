/*
 * File:        CanDo.cs
 * Author:      Étienne Ménard
 * Description: Simple little container for some global bools I don't want to bother reading from disk all the time.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanDo : MonoBehaviour
{
    public bool canHold;        // indicates if Piece Holding is enabled
    public bool canHardDrop;    // indicates if Hard Dropping is enables
    public int lockDelay;

    public bool flushed;        // indicates if the Flushed gimmick is enabled

    private void Awake()
    {
        lockDelay = PlayerPrefsManager.GetIntPlayerPref(PlayerPrefsManager.lockDelayKEY, 5);
        flushed = PlayerPrefsManager.GetBoolPlayerPref(PlayerPrefsManager.flushedKEY);
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
}
