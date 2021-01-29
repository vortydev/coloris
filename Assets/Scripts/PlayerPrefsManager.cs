using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsManager : MonoBehaviour
{
    [Header("Audio")]
    public static string musicKEY = "music";
    public static string sfxKEY = "sfx";

    [Header("Visual")]
    public static string gridKEY = "gameGrid";
    public static string visualiserKEY = "visualiser";

    [Header("Game")]
    public static string scoreKEY = "score";
    public static string nextPieceKEY = "nextPiece";

    [Header("Screenshake")]
    public static string screenshakeKEY = "screenshake";
    public static string shakeMagnitudeKEY = "shakeMagnitude";

    [Header("Difficulty")]
    public static string difficultyKEY = "cappedDifficulty";
    public static string maxDifficultyKEY = "maxDifficulty";

    [Header("Tutorial")]
    public static string firstPlayKEY = "firstPlay";

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

    // Toggles "bool" PlayerPrefs between 0 and 1
    public static void ToggleBoolPlayerPrefs(string KEY)
    {
        if (PlayerPrefs.GetInt(KEY) == 0)
            SaveIntPlayerPrefs(KEY, 1);
        else
            SaveIntPlayerPrefs(KEY, 0);
    }
}
