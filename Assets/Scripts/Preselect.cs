using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Preselect : MonoBehaviour
{
    private void OnEnable()
    {
        GetComponent<Selectable>().Select();
    }
}
