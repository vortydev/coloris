/*
 * File:        CZ_Catalog.cs
 * Author:      Étienne Ménard
 * Description: Handles the Chill Zone catalog of tracks.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class CZ_Catalog : MonoBehaviour
{
    [SerializeField] ChillZoneManager manager;

    [Header("Catalog")]
    [SerializeField] GameObject catalogTrackPrefab;
    [SerializeField] GameObject catalogScroll;
    public List<GameObject> catalog;

    [Header("Sort & Filter")]
    private int _sort = 0;    // 0: default (#), 1: name, 2: artist
    private int _filter = 0;  // 0: none, 1: by artist
    [SerializeField] TMP_Dropdown sortDropdown;
    [SerializeField] TMP_Dropdown filterDropdown;
    private string[] _artistsFilter;

    private void Start()
    {
        CreateCatalogDefault();
        GenerateArtistFilter();
    }

    private void LoadCatalog(int sort = 0)
    {
        switch (sort)
        {
            case 0:
                CreateCatalogDefault();
                break;
            case 1:
                CreateCatalogByName();
                break;
            case 2:
                CreateCatalogByArtist(_filter);
                break;
        }
    }

    private void CreateCatalogDefault()
    {
        for (int i = 0; i < manager.tracks.Length; i++)
        {
            GameObject newTrack = Instantiate(catalogTrackPrefab, catalogScroll.transform, false);
            newTrack.GetComponentInChildren<TextMeshProUGUI>().text = manager.tracks[i].trackNb.ToString() + ". " + manager.tracks[i].trackName + " - " + manager.tracks[i].authorName;
            newTrack.gameObject.name = manager.tracks[i].trackName;
            newTrack.transform.position = catalogScroll.transform.position;

            newTrack.GetComponent<CZ_CatalogTrack>().trackNb = manager.tracks[i].trackNb;

            catalog.Add(newTrack);
        }
    }

    private void CreateCatalogByName()
    {
        TrackSO[] tracksByName = manager.tracks.OrderBy(go => go.trackName).ToArray();

        for (int i = 0; i < tracksByName.Length; i++)
        {
            GameObject newTrack = Instantiate(catalogTrackPrefab, catalogScroll.transform, false);
            newTrack.GetComponentInChildren<TextMeshProUGUI>().text = tracksByName[i].trackNb.ToString() + ". " + tracksByName[i].trackName + " - " + tracksByName[i].authorName;
            newTrack.gameObject.name = tracksByName[i].trackName;
            newTrack.transform.position = catalogScroll.transform.position;

            newTrack.GetComponent<CZ_CatalogTrack>().trackNb = tracksByName[i].trackNb;

            catalog.Add(newTrack);
        }
    }

    private void CreateCatalogByArtist(int filter = 0)
    {
        TrackSO[] tracksByArtist = manager.tracks.OrderBy(go => go.authorName).ToArray();

        if (filter == 0)
        {
            for (int i = 0; i < tracksByArtist.Length; i++)
            {
                GameObject newTrack = Instantiate(catalogTrackPrefab, catalogScroll.transform, false);
                newTrack.GetComponentInChildren<TextMeshProUGUI>().text = tracksByArtist[i].trackNb.ToString() + ". " + tracksByArtist[i].trackName + " - " + tracksByArtist[i].authorName;
                newTrack.gameObject.name = tracksByArtist[i].trackName;
                newTrack.transform.position = catalogScroll.transform.position;

                newTrack.GetComponent<CZ_CatalogTrack>().trackNb = tracksByArtist[i].trackNb;

                catalog.Add(newTrack);
            }
        }
        else
        {
            for (int i = 0; i < tracksByArtist.Length; i++)
            {
                if (_artistsFilter[filter - 1] == tracksByArtist[i].authorName)
                {
                    GameObject newTrack = Instantiate(catalogTrackPrefab, catalogScroll.transform, false);
                    newTrack.GetComponentInChildren<TextMeshProUGUI>().text = tracksByArtist[i].trackNb.ToString() + ". " + tracksByArtist[i].trackName + " - " + tracksByArtist[i].authorName;
                    newTrack.gameObject.name = tracksByArtist[i].trackName;
                    newTrack.transform.position = catalogScroll.transform.position;

                    newTrack.GetComponent<CZ_CatalogTrack>().trackNb = tracksByArtist[i].trackNb;

                    catalog.Add(newTrack);
                }
            }
        }
    }

    public void UpdateCatalogSorting()
    {
        _sort = sortDropdown.value;

        if (_sort == 2)
        {
            filterDropdown.interactable = true;
        }
        else
        {
            filterDropdown.interactable = false;
            filterDropdown.value = 0;
            filterDropdown.RefreshShownValue();
        }

        foreach (GameObject go in catalog)
        {
            Destroy(go);
        }

        catalog.Clear();
        LoadCatalog(_sort);
    }

    public void UpdateCatalogFilter()
    {
        _filter = filterDropdown.value;

        foreach (GameObject go in catalog)
        {
            Destroy(go);
        }

        catalog.Clear();
        CreateCatalogByArtist(_filter);
    }

    private void GenerateArtistFilter()
    {
        TrackSO[] tracksByArtist = manager.tracks.OrderBy(go => go.authorName).ToArray();
        List<string> artists = new List<string>();

        for (int i = 0; i < tracksByArtist.Length; i++)
        {
            if (!artists.Contains(tracksByArtist[i].authorName))
            {
                artists.Add(tracksByArtist[i].authorName);
            }
        }

        filterDropdown.AddOptions(artists);

        _artistsFilter = new string[artists.Count];
        _artistsFilter = artists.ToArray();
    }
}
