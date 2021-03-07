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

    public bool flushed;        // indicates if the Flushed gimmick is enabled

    private void Awake()
    {
        flushed = PlayerPrefsManager.GetBoolPlayerPref(PlayerPrefsManager.flushedKEY);
    }
}
