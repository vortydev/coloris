/*
 * File:        ChangeLogManager.cs
 * Author:      Étienne Ménard
 * Description: Handles all the stuff for the Chnage Log.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeLogManager : MonoBehaviour
{
    [SerializeField] ReleaseSO[] releases;
    [SerializeField] GameObject releaseButtonPrefab;

    [Header("Change Log Page")]
    [SerializeField] GameObject changeLogPage;
    [SerializeField] GameObject changeLogScrollView;

    [Header("Release Page")]
    [SerializeField] GameObject releasePage;
    [SerializeField] TextMeshProUGUI releaseName;
    [SerializeField] TextMeshProUGUI releaseContent;

    private void Start()
    {
        LoadReleaseButtons();
    }

    private void LoadReleaseButtons()
    {
        for (int i = releases.Length - 1; i >= 0; i--)
        {
            GameObject newRelease = Instantiate(releaseButtonPrefab, changeLogScrollView.transform, false); // instantiates a new release
            newRelease.name = "Release " + releases[i].version;                                             // names the GameObject
            newRelease.transform.position = changeLogScrollView.transform.position;                         // sets the position of the object as the one of its parent

            newRelease.GetComponentInChildren<TextMeshProUGUI>().text = releases[i].version + " - " + releases[i].name;
            newRelease.GetComponentInChildren<ReleaseButton>().index = i;
        }
    }

    public void LoadReleasePage(int ind)
    {
        releaseName.text = releases[ind].version + " - " + releases[ind].name;

        string content = releases[ind].description;

        if (releases[ind].newTracks.Length > 0)
        {
            content += "\n\nNEW TRACKS";
            for (int j = 0; j < releases[ind].newTracks.Length; j++)
            {
                content += "\n- " + releases[ind].newTracks[j];
            }
        }

        if (releases[ind].changes.Length > 0)
        {
            content += "\n\nCHANGES";
            for (int j = 0; j < releases[ind].changes.Length; j++)
            {
                content += "\n- " + releases[ind].changes[j];
            }
        }

        if (releases[ind].bugfixes.Length > 0)
        {
            content += "\n\nBUGFIXES";
            for (int j = 0; j < releases[ind].bugfixes.Length; j++)
            {
                content += "\n#" + releases[ind].bugfixes[j];
            }
        }

        releaseContent.text = content;

        TogglePages();
    }

    public void TogglePages()
    {
        changeLogPage.SetActive(!changeLogPage.activeSelf);
        releasePage.SetActive(!releasePage.activeSelf);
    }
}
