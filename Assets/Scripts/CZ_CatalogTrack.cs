using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CZ_CatalogTrack : MonoBehaviour
{
    public int trackNb;

    public void SelectCatalogTrack()
    {
        FindObjectOfType<ChillZoneManager>().GetSelectedCatalogTrack(trackNb);
    }

    public void SelectQueueTrack()
    {
        FindObjectOfType<ChillZoneManager>().GetSelectedQueueTrack(trackNb);
    }
}
