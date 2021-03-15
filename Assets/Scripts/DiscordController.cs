/*
 * File:        DiscordController.cs
 * Author:      Étienne Ménard
 * Description: Takes care of displaying Discord Rich Presence on the player's Discord profile.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Discord;

public class DiscordController : MonoBehaviour
{
    private static DiscordController instance;
    public Discord.Discord discord;

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
    }

    private void Start()
	{
        // param: 811777552208887810 is the application ID for Coloris
        // param: CreateFlags.NoRequireDiscord means the game won't crash without Discord open lol
        discord = new Discord.Discord(811777552208887810, (System.UInt64)Discord.CreateFlags.NoRequireDiscord);
		var activityManager = discord.GetActivityManager();
		var activity = new Discord.Activity
		{
            Assets =
            {
                LargeImage = "game_thumbnail",
                LargeText = "Coloris",
            }
        };

		activityManager.UpdateActivity(activity, (res) =>
		{
			if (res == Discord.Result.Ok)
			{
				//Debug.Log("Initial Discord Rich Presence update successful.");    // commented because it was annoying
			}   
        });
	}

	private void Update()
	{
		discord.RunCallbacks();
	}

    // Updates the rich presence's details and state (might expand on it later)
    public void UpdateRichPresence(string detailsString, string stateString, bool flushed = false)
    {
        string largeImage = "game_thumbnail";
        if (flushed)
        {
            largeImage = "thumbnail_flushed";
        }

        var activityManager = discord.GetActivityManager();
        var activity = new Discord.Activity
        {
            State = stateString,
            Details = detailsString,
            Assets =
            {
                LargeImage = largeImage,
                LargeText = "Coloris",
            },
        };

        activityManager.UpdateActivity(activity, (res) =>
        {
            if (res == Discord.Result.Ok)
            {
                //Debug.Log("Discord Rich Presence successfully updated.");
            }
            else Debug.LogError("Discord Rich Presence update failed.");
        });
    }
}