using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flushed : MonoBehaviour
{
    private void Start()
    {
        gameObject.SetActive(FindObjectOfType<CanDo>().flushed);
    }
}
