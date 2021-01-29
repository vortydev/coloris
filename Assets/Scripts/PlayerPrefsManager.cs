using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsManager : MonoBehaviour
{
    // audio
    public static string musicKEY = "music";                    // float that controls the volume of the music
    public static string sfxKEY = "sfx";                        // float that controls the volume of the sfx

    // visual
    public static string gridKEY = "gameGrid";                  // "bool" int that toggles the game grid
    public static string visualiserKEY = "visualiser";          // "bool" int that toggles the audio visualiser

    // game
    public static string scoreKEY = "score";                    // "bool" int that toggles the score UI
    public static string nextPieceKEY = "nextPiece";            // "bool" int that toggles the nextpiece UI

    // screenshake
    public static string screenshakeKEY = "screenshake";        // "bool" int that toggles the Screenshake.cs script
    public static string shakeMagnitudeKEY = "shakeMagnitude";  // float that controls the intensity of the screenshake

    // difficulty
    public static string difficultyLevelKEY = "difficultyLevel";    // int of the difficulty level (1

    // tutorial
    public static string firstPlayKEY = "firstPlay";            // "bool" int that tells if it's the player's first playthrough

    // Returns a float PlayerPref and creates it if it doesn't exist
    public static float GetFloatPlayerPrefs(string KEY, float val)
    {
        if (!PlayerPrefs.HasKey(KEY))
        {
            PlayerPrefs.SetFloat(KEY, val);
            PlayerPrefs.Save();
        }

        return PlayerPrefs.GetFloat(KEY);
    }

    // Saves a float PlayerPref
    public static void SaveFloatPlayerPrefs(string KEY, float val)
    {
        PlayerPrefs.SetFloat(KEY, val);
        PlayerPrefs.Save();
    }

    // Returns an int PlayerPref and creates it if it doesn't exist
    public static int GetIntPlayerPrefs(string KEY, int val)
    {
        if (!PlayerPrefs.HasKey(KEY))
        {
            PlayerPrefs.SetInt(KEY, val);
            PlayerPrefs.Save();
        }

        return PlayerPrefs.GetInt(KEY);
    }

    // Saves an int PlayerPref 
    public static void SaveIntPlayerPrefs(string KEY, int val)
    {
        PlayerPrefs.SetInt(KEY, val);
        PlayerPrefs.Save();
    }

    // Toggles "bool" int PlayerPrefs between 0 and 1
    public static void ToggleBoolPlayerPrefs(string KEY)
    {
        if (PlayerPrefs.GetInt(KEY) == 0)
            SaveIntPlayerPrefs(KEY, 1);
        else
            SaveIntPlayerPrefs(KEY, 0);
    }
}
