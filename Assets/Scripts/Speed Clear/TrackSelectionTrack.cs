using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackSelectionTrack : MonoBehaviour
{
    public int trackNb;

    public void SelectCatalogTrack()
    {
        FindObjectOfType<TrackSelection>().SelectTrack(trackNb);
    }
}
