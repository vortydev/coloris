/*
 * File:        CZ_Catalog.cs
 * Author:      �tienne M�nard
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
    private int _sort = 0, _filter = 0;    // 0: default (#), 1: name, 2: artist, 3: volume
    [SerializeField] TMP_Dropdown sortDropdown;
    [SerializeField] TMP_Dropdown filterDropdown;
    [SerializeField] private List<string> _artistsFilter;
    [SerializeField] private List<string> _volumeFilter;

    private void Start()
    {
        CreateCatalogDefault();

        GenerateArtistFilter();
        GenerateVolumeFilter();

        sortDropdown.Select();
    }

    public void LoadCatalog()
    {
        switch (_sort)
        {
            case 0:
                CreateCatalogDefault();
                break;
            case 1:
                CreateCatalogByName();
                break;
            case 2:
                CreateCatalogByArtist();
                break;
            case 3:
                CreateCatalogByVolume();
                break;
        }
    }

    private void CreateCatalogDefault()
    {
        for (int i = 0; i < manager.tracks.GetArraySize(); i++)
        {
            GameObject newTrack = Instantiate(catalogTrackPrefab, catalogScroll.transform, false);
            newTrack.GetComponentInChildren<TextMeshProUGUI>().text = manager.tracks.GetTrackNb(i).ToString() + ". " + manager.tracks.GetTrackName(i) + " - " + manager.tracks.GetArtistName(i);
            newTrack.gameObject.name = manager.tracks.GetTrackName(i);
            newTrack.transform.position = catalogScroll.transform.position;

            newTrack.GetComponent<CZ_CatalogTrack>().trackNb = manager.tracks.GetTrackNb(i);

            catalog.Add(newTrack);
        }
    }

    private void CreateCatalogByName()
    {
        TrackSO[] tracksByName = manager.tracks.GetRefArray().OrderBy(go => go.trackName).ToArray();

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

    private void CreateCatalogByArtist()
    {
        TrackSO[] tracksByArtist = manager.tracks.GetRefArray().OrderBy(go => go.authorName).ToArray();

        if (_filter > 0)
        {
            for (int i = 0; i < tracksByArtist.Length; i++)
            {
                if (tracksByArtist[i].authorName == _artistsFilter[_filter - 1])
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
        else
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
    }

    public void CreateCatalogByVolume()
    {
        TrackSO[] tracksByVolume = manager.tracks.GetRefArray().OrderBy(go => go.volNb).ToArray();

        if (_filter > 0)
        {
            for (int i = 0; i < tracksByVolume.Length; i++)
            {
                if ("Volume " + tracksByVolume[i].volNb.ToString() == _volumeFilter[_filter - 1])
                {
                    GameObject newTrack = Instantiate(catalogTrackPrefab, catalogScroll.transform, false);
                    newTrack.GetComponentInChildren<TextMeshProUGUI>().text = tracksByVolume[i].trackNb.ToString() + ". " + tracksByVolume[i].trackName + " - " + tracksByVolume[i].authorName;
                    newTrack.gameObject.name = tracksByVolume[i].trackName;
                    newTrack.transform.position = catalogScroll.transform.position;

                    newTrack.GetComponent<CZ_CatalogTrack>().trackNb = tracksByVolume[i].trackNb;

                    catalog.Add(newTrack);
                }
            }
        }
        else
        {
            for (int i = 0; i < tracksByVolume.Length; i++)
            {

                GameObject newTrack = Instantiate(catalogTrackPrefab, catalogScroll.transform, false);
                newTrack.GetComponentInChildren<TextMeshProUGUI>().text = tracksByVolume[i].trackNb.ToString() + ". " + tracksByVolume[i].trackName + " - " + tracksByVolume[i].authorName;
                newTrack.gameObject.name = tracksByVolume[i].trackName;
                newTrack.transform.position = catalogScroll.transform.position;

                newTrack.GetComponent<CZ_CatalogTrack>().trackNb = tracksByVolume[i].trackNb;

                catalog.Add(newTrack);
            }
        }
    }

    public void UpdateCatalogSorting()
    {
        if (_sort != sortDropdown.value)
        {
            _sort = sortDropdown.value;
            UpdateCatalogFilter();
        }
        else
        {
            _filter = filterDropdown.value;
        }

        foreach (GameObject go in catalog)
        {
            Destroy(go);
        }

        catalog.Clear();
        LoadCatalog();
    }

    public void UpdateCatalogFilter()
    {
        switch (sortDropdown.value)
        {
            case 2:
                filterDropdown.interactable = true;
                LoadFilter(_artistsFilter);
                break;
            case 3:
                filterDropdown.interactable = true;
                LoadFilter(_volumeFilter);
                break;
            default:
                filterDropdown.interactable = false;
                LoadFilter();
                break;
        }

        filterDropdown.value = _filter;
        filterDropdown.RefreshShownValue();
    }

    private void LoadFilter(List<string> filterList = null)
    {
        filterDropdown.ClearOptions();
        filterDropdown.AddOptions(new List<string>() { "None" });
        _filter = 0;

        if (filterList != null)
        {
            filterDropdown.AddOptions(filterList);
        }
    }

    private void GenerateArtistFilter()
    {
        TrackSO[] tracksByArtist = manager.tracks.GetRefArray().OrderBy(go => go.authorName).ToArray();
        List<string> artists = new List<string>();

        for (int i = 0; i < tracksByArtist.Length; i++)
        {
            if (!artists.Contains(tracksByArtist[i].authorName))
            {
                artists.Add(tracksByArtist[i].authorName);
            }
        }

        _artistsFilter = artists;
    }

    private void GenerateVolumeFilter()
    {
        TrackSO[] tracksByVolume = manager.tracks.GetRefArray().OrderBy(go => go.volNb).ToArray();
        tracksByVolume.OrderBy(go => go.trackNb).ToArray();
        List<string> volFilter = new List<string>();    

        for (int i = 0; i < tracksByVolume.Length; i++)
        {
            if (!volFilter.Contains("Volume " + tracksByVolume[i].volNb))
            {
                volFilter.Add("Volume " + tracksByVolume[i].volNb);
            }
        }

        filterDropdown.ClearOptions();

        filterDropdown.AddOptions(new List<string>() { "None" });
        filterDropdown.AddOptions(volFilter);

        _volumeFilter = volFilter;
    }
}
