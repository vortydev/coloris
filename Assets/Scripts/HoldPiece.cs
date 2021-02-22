using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoldPiece : MonoBehaviour
{
    public Image[] heldPieceImage;
    public GameObject heldPiece;
    private Vector3 spawnPos = new Vector3(5, 15, 0);

    private void Start()
    {
        ResetHeldPiece();
    }

    public void HoldCurrentPiece(GameObject currentPiece)
    {
        if (heldPiece != null && CheckSamePiece(currentPiece.GetComponent<Group>().pieceId))
            return;

        FindObjectOfType<SFXManager>().HoldPieceSFX();

        if (PieceHeld())
        {
            SwapPieces(currentPiece);
        }
        else
        {
            HoldFirstPiece(currentPiece);
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

    private bool PieceHeld()
    {
        return heldPiece != null;
    }

    private bool CheckSamePiece(int id)
    {
        return heldPiece.GetComponent<Group>().pieceId == id;
    }

    private void TogglePieceImage(int id)
    {
        if (heldPiece != null)
            heldPieceImage[id].gameObject.SetActive(!heldPieceImage[id].gameObject.activeSelf);
    }

    private void DeleteFromGrid(GameObject go)
    {
        foreach (Transform child in go.transform)
        {
            Vector2 v = Playfield.RoundVec2(child.position);
            Playfield.grid[(int)v.x, (int)v.y] = null;
        }
    }

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
