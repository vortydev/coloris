/*
 * File:        CanDo.cs
 * Author:      Étienne Ménard
 * Description: Simple little container for some global bools, I don't want to bother reading from disk all the time.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayController : MonoBehaviour
{
    private static GameplayController instance;
    public static int gameMode;

    public bool canHold;        // pieces can be held
    public bool canHardDrop;    // pieces can be hard dropped
    public bool canGhost;       // pieces can spawn a ghost

    public int lockDelay;       // delay before the piece locks in place
    public int cellFace;        // sets the blocks' face sprite

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // makes the array persistent across scenes
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        gameMode = SceneManager.GetActiveScene().buildIndex switch
        {
            // classic
            2 => 0,
            // speed clear
            4 => 1,
            // in case, classic set up
            _ => 0,
        };

        canHold = PlayerPrefsManager.GetBoolPlayerPref(PlayerPrefsManager.holdPieceKEY);
        canHardDrop = PlayerPrefsManager.GetBoolPlayerPref(PlayerPrefsManager.hardDropKEY);
        canGhost = PlayerPrefsManager.GetBoolPlayerPref(PlayerPrefsManager.ghostPieceKEY);

        lockDelay = PlayerPrefsManager.GetIntPlayerPref(PlayerPrefsManager.lockDelayKEY, 5);
        cellFace = PlayerPrefsManager.GetIntPlayerPref(PlayerPrefsManager.cellFaceKEY, 0);
    }

    public static int GetGameMode()
    {
        return gameMode;
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
