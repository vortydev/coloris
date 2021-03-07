/*
 * File:        Flushed.cs
 * Author:      Étienne Ménard
 * Description: Gimmick implemented for the memes that adds a little flushed faced to blocks in the game.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flushed : MonoBehaviour
{
    private void Start()
    {
        gameObject.SetActive(FindObjectOfType<CanDo>().flushed);
    }
}
