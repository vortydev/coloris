using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanDo : MonoBehaviour
{
    public bool canHold;
    public bool canHardDrop;

    public bool flushed;

    private void Awake()
    {
        flushed = PlayerPrefsManager.GetBoolPlayerPref(PlayerPrefsManager.flushedKEY);
    }
}
