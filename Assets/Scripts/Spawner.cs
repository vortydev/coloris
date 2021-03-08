/*
 * File:        Spawner.cs
 * Author:      Étienne Ménard
 * Description: Generates bags of 7 pieces and spawns the next piece in the game.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] NextPiece nextPiece;   // script that handles displaying the next piece
    public GameObject[] refPieces;          // array of reference pieces that are cloned into bags

    [SerializeField] GameObject[] bag1, bag2;   // 2 seperate arrays of the 7 pieces in a random order
    private GameObject _lastPiece;              // last piece of the bag kept in memory

    private int _bagInd;            // "pointer" in the bag                
    public int selectedBag = 0;     // the selected bag

    private void Awake()
    {
        RegenBags();
    }

    public void RegenBags()
    {
        bag1 = GenerateBag();
        bag2 = GenerateBag();
    }

    public void SpawnNext()
    {
        // spawns a piece at the spawner's position
        if (selectedBag == 0)
            Instantiate(bag1[_bagInd], transform.position, Quaternion.identity);
        else
            Instantiate(bag2[_bagInd], transform.position, Quaternion.identity);

        nextPiece.DisplayNextPiece();

        _bagInd++;                           // increments the bag index
        if (_bagInd > refPieces.Length - 1)  // and generates anew bag if we're at the end of the bag array
        {
            if (selectedBag == 0)
            {
                selectedBag = 1;
                bag1 = GenerateBag();
            }
            else
            {
                selectedBag = 0;
                bag2 = GenerateBag();
            }
        }
    }

    private GameObject[] GenerateBag()
    {
        GameObject[] bag = new GameObject[refPieces.Length]; // generates a fresh new array
        _bagInd = 0;                            // resets the bag index to the beginning

        int rng;
        bool[] pieceTaken = new bool[refPieces.Length];

        for (int i = 0; i < refPieces.Length; i++)
        {
            do
            {
                rng = Random.Range(0, refPieces.Length);
                bag[i] = refPieces[rng];
            } while (pieceTaken[rng] || bag[0] == _lastPiece);

            pieceTaken[rng] = true;
        }

        _lastPiece = bag[refPieces.Length - 1]; // sets the last of the bag for the next bag generation

        return bag;
    }

    public int ReturnPieceInd()
    {
        for (int i = 0; i < refPieces.Length; i++)
        {
            if (selectedBag == 0)
            {
                if (_bagInd < 6)
                {
                    if (refPieces[i] == bag1[_bagInd + 1])
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
                if (_bagInd < 6)
                {
                    if (refPieces[i] == bag2[_bagInd + 1])
                        return i;
                }
                else
                {
                    if (refPieces[i] == bag1[0])
                        return i;
                }
            }
        }

        return 0;
    }
}
