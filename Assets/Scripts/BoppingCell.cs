using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoppingCell : MonoBehaviour
{
    private Vector3 start, shrink;
    public float shrinkVal;

    private void OnEnable()
    {
        start = new Vector3(transform.localScale.x, transform.localScale.y);
        shrink = new Vector3(transform.localScale.x - shrinkVal, transform.localScale.y - shrinkVal);

        BopManager.BopBlocks += BopManagerOnBop;
    }

    private void OnDisable()
    {
        BopManager.BopBlocks -= BopManagerOnBop;
    }

    private void BopManagerOnBop(float val)
    {
        var target = Vector3.Lerp(start, shrink, val);
        transform.localScale = target;
    }
}
