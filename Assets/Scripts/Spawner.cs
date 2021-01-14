using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] NextPiece nextPiece;
    public GameObject[] refPieces;

    public GameObject[] bag;
    public GameObject[] bag2;
    private GameObject lastPiece;

    public int bagInd;
    public int selectedBag = 0;

    private void Awake()
    {
        GenerateBag();
        GenerateBag2();

        nextPiece.DisplayNextPiece();
    }
    private void Start()
    {
        SpawnNext();
    }

    public void SpawnNext()
    {
        // spawns a piece at the spawner's position
        if (selectedBag == 0)
            Instantiate(bag[bagInd], transform.position, Quaternion.identity);
        else
            Instantiate(bag2[bagInd], transform.position, Quaternion.identity);

        nextPiece.DisplayNextPiece();

        bagInd++;                       // increments the bag index
        if (bagInd > refPieces.Length - 1)    // and generates anew bag if we're at the end of the bag array
        {
            if (selectedBag == 0)
            {
                selectedBag = 1;
                GenerateBag();
            }
            else
            {
                selectedBag = 0;
                GenerateBag2();
            }
        }
    }

    private void GenerateBag()
    {
        bag = new GameObject[refPieces.Length];   // generates a fresh new array
        bagInd = 0;                             // resets the bag index to the beginning

        int rng;
        bool[] pieceTaken = new bool[refPieces.Length];

        for (int i = 0; i < refPieces.Length; i++)
        {
            do
            {
                rng = Random.Range(0, refPieces.Length);
                bag[i] = refPieces[rng];
            } while (pieceTaken[rng] || bag[0] == lastPiece);

            pieceTaken[rng] = true;
        }

        lastPiece = bag[refPieces.Length - 1];
    }

    private void GenerateBag2()
    {
        bag2 = new GameObject[refPieces.Length];   // generates a fresh new array
        bagInd = 0;                             // resets the bag index to the beginning

        int rng;
        bool[] pieceTaken = new bool[refPieces.Length];

        for (int i = 0; i < refPieces.Length; i++)
        {
            do
            {
                rng = Random.Range(0, refPieces.Length);
                bag2[i] = refPieces[rng];
            } while (pieceTaken[rng] || bag2[0] == lastPiece);

            pieceTaken[rng] = true;
        }

        lastPiece = bag2[refPieces.Length - 1];
    }

    public int ReturnPieceInd()
    {
        for (int i = 0; i < refPieces.Length; i++)
        {
            if (selectedBag == 0)
            {
                if (bagInd < 6)
                {
                    if (refPieces[i] == bag[bagInd + 1])
                        return i;
                }
                else
                {
                    if (refPieces[i] == bag2[0])
                        return i;
                }
                    
            }
            else
            {
                if (bagInd < 6)
                {
                    if (refPieces[i] == bag2[bagInd + 1])
                        return i;
                }
                else
                {
                    if (refPieces[i] == bag[0])
                        return i;
                }
            }
        }

        return 0;
    }
}
