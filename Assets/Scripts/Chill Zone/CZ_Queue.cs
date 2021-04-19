/*
 * File:        CZ_Queue.cs
 * Author:      Étienne Ménard
 * Description: Takes care of the queue with selectable tracks
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CZ_Queue : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] ChillZoneManager manager;

    [Header("Queue")]
    [SerializeField] GameObject queueTrackPrefab;
    [SerializeField] GameObject queueScroll;
    [SerializeField] TextMeshProUGUI queueText;
    public List<GameObject> queue;

    [Header("Queue Controls - Catalog")]
    [SerializeField] Button playCatalogButton;
    [SerializeField] Button nextButton;
    [SerializeField] Button queueButton;

    [Header("Queue Controls - Queue")]
    [SerializeField] Button playQueueButton;
    [SerializeField] Button skipToButton;
    [SerializeField] Button removeButton;

    [Header("Playlist Controls")]
    [SerializeField] Button shuffleButton;
    public TMP_Dropdown loopDropdown;
    public Button emptyButton;

    private void Start()
    {
        playQueueButton.gameObject.SetActive(false);
        skipToButton.gameObject.SetActive(false);
        removeButton.gameObject.SetActive(false);
    }

    // toggles catalog track controls
    public void ToggleCatalogTrackControls(bool state)
    {
        playCatalogButton.interactable = state;
        nextButton.interactable = state;
        queueButton.interactable = state;
    }

    // toggles queue track controls
    public void ToggleQueueTrackControls(bool state)
    {
        playQueueButton.interactable = state;
        skipToButton.interactable = state;
        removeButton.interactable = state;
    }

    // toggles playlist controls
    public void TogglePlaylistControls(bool state)
    {
        shuffleButton.interactable = state;
        loopDropdown.interactable = state;
        emptyButton.interactable = state;
    }

    public void TogglePlaylistButtons(bool state)
    {
        shuffleButton.interactable = state;
        emptyButton.interactable = state;
    }

    // switch between the 2 different control buttons for tracks
    public void EnableQueueTrackControls(bool state)
    {
        playQueueButton.gameObject.SetActive(state);
        skipToButton.gameObject.SetActive(state);
        removeButton.gameObject.SetActive(state);

        playCatalogButton.gameObject.SetActive(!state);
        nextButton.gameObject.SetActive(!state);
        queueButton.gameObject.SetActive(!state);
    }

    // generates a new queue with the tracks from the playlist
    public void CreateQueue(List<TrackSO> playlist)
    {
        EmptyQueue();

        int pos = 1;
        foreach (TrackSO track in playlist)
        {
            GameObject newTrack = Instantiate(queueTrackPrefab, queueScroll.transform, false);
            newTrack.GetComponentsInChildren<TextMeshProUGUI>()[0].text = track.TrackName;
            newTrack.GetComponentsInChildren<TextMeshProUGUI>()[1].text = pos.ToString() + ".";
            newTrack.gameObject.name = pos.ToString() + "-" + track.TrackName;
            newTrack.transform.position = queueScroll.transform.position;

            newTrack.GetComponent<CZ_CatalogTrack>().trackNb = track.TrackNb;
            pos++;

            queue.Add(newTrack);
        }

        if (queue.Count > 0)
        {
            queueText.text = "Queue (" + queue.Count + ")";
            emptyButton.interactable = true;
        }
        else
        {
            emptyButton.interactable = false;
        }

        if (queue.Count > 1)
        {
            shuffleButton.interactable = true;
        }
        else
        {
            shuffleButton.interactable = false;
        }
    }

    // empties the queue
    public void EmptyQueue()
    {
        foreach (GameObject go in queue)
        {
            Destroy(go);
        }

        queueText.text = "Queue";
        queue.Clear();
    }
}
