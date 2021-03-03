using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Discord;

public class DiscordController : MonoBehaviour
{
	public Discord.Discord discord;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
	{
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
				//Debug.Log("Initial Discord Rich Presence update successful.");
			}   
        });
	}

	private void Update()
	{
		discord.RunCallbacks();
	}

    public void UpdateRichPresence(string detailsString, string stateString)
    {
        var activityManager = discord.GetActivityManager();
        var activity = new Discord.Activity
        {
            State = stateString,
            Details = detailsString,
            //Timestamps =
            //{
            //    Start = 5,
            //    End = 6,
            //},
            Assets =
            {
                LargeImage = "game_thumbnail",
                LargeText = "Coloris",
            },
            //Instance = true,
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