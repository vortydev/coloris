using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextPiece : MonoBehaviour
{
    [SerializeField] Spawner spawner;
    public Image[] nextPiece;
    private int ind;

    private void Start()
    {
        ResetNextPiece();
    }

    public void DisplayNextPiece()
    {
        nextPiece[ind].gameObject.SetActive(false);

        ind = spawner.ReturnPieceInd();

        nextPiece[ind].gameObject.SetActive(true);
    }

    public void ResetNextPiece()
    {
        for (int i = 0; i < 7; i++)
        {
            nextPiece[i].gameObject.SetActive(false);
        }
    }
}
