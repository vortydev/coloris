/*
 * File:        HoldPiece.cs
 * Author:      Étienne Ménard
 * Description: Script for the piece holding mechanic.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoldPiece : MonoBehaviour
{
    public Image[] heldPieceImage;
    public GameObject heldPiece;
    private Vector3 spawnPos;

    private void Start()
    {
        spawnPos = FindObjectOfType<Spawner>().gameObject.transform.position;
        ResetHeldPiece();
    }

    public void HoldCurrentPiece(GameObject currentPiece)
    {
        if (heldPiece != null && CheckSamePiece(currentPiece.GetComponent<Group>().pieceId))
            return;

        FindObjectOfType<GameSFX>().HoldPieceSFX();  // plays the hold piece sound

        if (PieceHeld())
        {
            SwapPieces(currentPiece);   // swaps the current piece with the held one if there's one
        }
        else
        {
            HoldFirstPiece(currentPiece);   // yoinks the piece off the grid
        }
    }

    private void HoldFirstPiece(GameObject currentPiece)
    {
        heldPiece = Instantiate(currentPiece, transform);   // create a copy of the current piece to be held
        heldPiece.name = "HeldPiece";                       // rename it (for me lol)
        heldPiece.SetActive(false);                         // disables the GameObject

        DeleteFromGrid(currentPiece);               // deletes from the grid the block of the piece
        Destroy(currentPiece);                      // deletes the GameObject
        FindObjectOfType<Spawner>().SpawnNext();    // spawns the next piece
        FindObjectOfType<Group>().beenSwapped = true;

        TogglePieceImage(heldPiece.GetComponent<Group>().pieceId - 1);  // updates UI with the held piece
    }

    private void SwapPieces(GameObject currentPiece)
    {
        TogglePieceImage(heldPiece.GetComponent<Group>().pieceId - 1);

        GameObject swappedPiece = Instantiate(heldPiece, spawnPos, Quaternion.identity);
        AddToGrid(swappedPiece);                                // add the peice to the grid
        swappedPiece.GetComponent<Group>().beenSwapped = true;  // prevents the piece from being swapped again
        swappedPiece.SetActive(true);                           // enable the piece

        Destroy(heldPiece);                                     // destroy old held piece
        heldPiece = Instantiate(currentPiece, transform);       // make a new one
        heldPiece.name = "HeldPiece";
        heldPiece.SetActive(false);

        DeleteFromGrid(currentPiece);
        Destroy(currentPiece);

        TogglePieceImage(heldPiece.GetComponent<Group>().pieceId - 1);
    }

    // returns if a piece is held
    private bool PieceHeld()
    {
        return heldPiece != null;   
    }

    // returns if the held piece is the same as the current one
    private bool CheckSamePiece(int id)
    {
        return heldPiece.GetComponent<Group>().pieceId == id;   
    }

    // toggles the image of the held piece
    private void TogglePieceImage(int id)
    {
        if (heldPiece != null)
            heldPieceImage[id].gameObject.SetActive(!heldPieceImage[id].gameObject.activeSelf);
    }

    // removes the current piece from the grid
    private void DeleteFromGrid(GameObject go)
    {
        foreach (Transform child in go.transform)
        {
            Vector2 v = Playfield.RoundVec2(child.position);
            Playfield.grid[(int)v.x, (int)v.y] = null;
        }
    }

    // adds the held piece to the grid
    private void AddToGrid(GameObject go)
    {
        foreach (Transform child in go.transform)
        {
            Vector2 v = Playfield.RoundVec2(child.position);
            Playfield.grid[(int)v.x, (int)v.y] = child;
        }
    }

    public void ResetHeldPiece()
    {
        for (int i = 0; i < 7; i++)
        {
            heldPieceImage[i].gameObject.SetActive(false);
        }

        if (heldPiece != null)
        {
            heldPiece = null;
        }
    }
}
