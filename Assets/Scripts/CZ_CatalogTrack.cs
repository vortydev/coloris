/*
 * File:        CZ_CatalogTrack.cs
 * Author:      Étienne Ménard
 * Description: Small script attached to the track prefab to communicate with the manager.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
