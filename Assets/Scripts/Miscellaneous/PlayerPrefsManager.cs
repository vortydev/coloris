/*
 * File:        PlayerPrefsManager.cs
 * Author:      Étienne Ménard
 * Description: Cheeky script that that defines PlayerPrefs keys and useful methods to interat with it.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsManager : MonoBehaviour
{
    // audio
    public static string musicKEY = "music";                        // float that controls the volume of the music
    public static string sfxKEY = "sfx";                            // float that controls the volume of the sfx
    public static string menuSoundtrackKEY = "menuSoundtrack";      // "bool" int that enables music in the menus

    // sfx
    public static string moveSfxKEY = "moveSFX";                    // "bool" int that enables the SFX triggered by moving
    public static string rotateSfxKEY = "rotateSFX";                // "bool" int that enables the SFX triggered by rotating
    public static string hardDropSfxKEY = "hardDropSFX";            // "bool" int that enables the SFX triggered by hard dropping
    public static string holdPieceSfxKEY = "holdPieceSFX";          // "bool" int that enables the SFX triggered by holding a piece
    public static string lockSfxKEY = "lockSFX";                    // "bool" int that enables the SFX triggered by locking 

    // visual
    public static string gridKEY = "gameGrid";                      // "bool" int that toggles the game grid
    public static string visualiserKEY = "visualiser";              // "bool" int that toggles the audio visualiser
    public static string dynamicTextKEY = "dynamicText";            // "bool" int that tells the type writer if the text is dynamic or not
    public static string textSpeedKEY = "textSpeed";                // int holding the typing speed (0: slow, 1: default, 2: fast)

    // screenshake
    public static string screenshakeKEY = "screenshake";            // "bool" int that toggles the Screenshake.cs script
    public static string shakeMagnitudeKEY = "shakeMagnitude";      // float that controls the intensity of the screenshake

    // game
    public static string firstPlayKEY = "firstPlay";                // "bool" int that tells if it's the player's first playthrough
    public static string scoreKEY = "score";                        // "bool" int that toggles the score UI
    public static string highscoreKEY = "highscore";                // int of the player's highscore

    public static string difficultyLevelKEY = "difficultyLevel";    // int of the difficulty level (0: easy, 1: normal, 2: hard, 3: insane)
    public static string lockDelayKEY = "lockDelay";                // int that controls the lock delay for pieces
    public static string hardDropKEY = "hardDrop";                  // "bool" int that allows hard dropping
    public static string nextPieceKEY = "nextPiece";                // "bool" int that toggles the next piece UI
    public static string holdPieceKEY = "holdPiece";                // "bool" int that toggles the piece holding mechanic
    public static string ghostPieceKEY = "ghostPiece";              // "bool" int that toggles the ghost piece mechanic

    public static string keyboardLoadoutKEY = "keyboardLoadout";    // int for the selected keybind loadout (0: default, 1: vim, 2: gamer)
    public static string gamepadLoadoutKEY = "gamepadLoadout";      // int for the selected gamepad loadout (0: off, 1: default)

    // extras
    public static string cellFaceKEY = "cellFace";                  // int that toggles emoji faces on cells (0: disabled, 1: flushed, 2: weary, 3: pensive)
    public static string gameVersionKEY = "gameVersion";            // "bool" int that toggles the game version on the main menu

    // speed clear track highscores                                 // track highscores
    public static string SP_HS_1_KEY = "sphs1";                     // 1. Stellaris 
    public static string SP_HS_2_KEY = "sphs2";                     // 2. Encounter!
    public static string SP_HS_3_KEY = "sphs3";                     // 3. Flight
    public static string SP_HS_4_KEY = "sphs4";                     // 4. Endless
    public static string SP_HS_5_KEY = "sphs5";                     // 5. Center
    public static string SP_HS_6_KEY = "sphs6";                     // 6. Gumby
    public static string SP_HS_7_KEY = "sphs7";                     // 7. Racing
    public static string SP_HS_8_KEY = "sphs8";                     // 8. 1999
    public static string SP_HS_9_KEY = "sphs9";                     // 9. Lucid Dreams
    public static string SP_HS_10_KEY = "sphs10";                   // 10. NIGHTS
    public static string SP_HS_11_KEY = "sphs11";                   // 11. Astral
    public static string SP_HS_12_KEY = "sphs12";                   // 12. TKN


    // Returns a float PlayerPref and creates it if it doesn't exist
    public static float GetFloatPlayerPref(string KEY, float defaultVal)
    {
        if (!PlayerPrefs.HasKey(KEY))
        {
            PlayerPrefs.SetFloat(KEY, defaultVal);
            PlayerPrefs.Save();
        }

        return PlayerPrefs.GetFloat(KEY);
    }

    // Saves a float PlayerPref
    public static void SaveFloatPlayerPref(string KEY, float savedVal)
    {
        PlayerPrefs.SetFloat(KEY, savedVal);
        PlayerPrefs.Save();
    }

    // Returns an int PlayerPref and creates it if it doesn't exist
    public static int GetIntPlayerPref(string KEY, int defaultVal)
    {
        if (!PlayerPrefs.HasKey(KEY))
        {
            PlayerPrefs.SetInt(KEY, defaultVal);
            PlayerPrefs.Save();
        }

        return PlayerPrefs.GetInt(KEY);
    }

    // Saves an int PlayerPref 
    public static void SaveIntPlayerPref(string KEY, int savedVal)
    {
        PlayerPrefs.SetInt(KEY, savedVal);
        PlayerPrefs.Save();
    }

    // Toggles "bool" int PlayerPrefs between 0 and 1
    public static void ToggleBoolPlayerPref(string KEY)
    {
        if (PlayerPrefs.GetInt(KEY) == 0)
            SaveIntPlayerPref(KEY, 1);
        else
            SaveIntPlayerPref(KEY, 0);
    }

    public static bool GetBoolPlayerPref(string KEY)
    {
        return PlayerPrefs.GetInt(KEY) == 1;
    }

    public static bool GetBoolPlayerPref(string KEY, bool defaultVal)
    {
        int val;
        if (defaultVal)
        {
            val = 1;
        }
        else
        {
            val = 0;
        }

        return GetIntPlayerPref(KEY, val) == 1;
    }

    public static string GetBoolStringPlayerPref(string KEY)
    {
        if (PlayerPrefs.GetInt(KEY) == 0)
        {
            return "Off";
        }
        else
        {
            return "On";
        }
    }
}
