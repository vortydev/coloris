/*
 * File:        ReleaseButton.cs
 * Author:      Étienne Ménard
 * Description: Small script to load the Change Log's release page.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReleaseButton : MonoBehaviour
{
    public int index;

    public void OnReleaseButtonClick()
    {
        FindObjectOfType<ChangeLogManager>().LoadReleasePage(index);
        FindObjectOfType<UISFX>().OnMenuButtonClick();
    }
}
