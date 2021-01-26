using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsGame : MonoBehaviour
{
    [Header("Grid")]
    [SerializeField] GameObject grid;
    [SerializeField] Toggle gridToggle;

    //[Header("Score")]
    //[SerializeField] Image scoreUI;
    //[SerializeField] Toggle scoreToggle;

    [Header("Next Piece")]
    [SerializeField] Image nextPieceUI;
    [SerializeField] Toggle nextPieceToggle;

    //[Header("Held Piece")]
    //[SerializeField] Image heldPieceUI;
    //[SerializeField] Toggle heldPieceToggle;

    //[Header("Ghost Piece")]
    //[SerializeField] Image ghostPieceUI;
    //[SerializeField] Toggle ghostPieceToggle;

    public void ToggleGrid()
    {
        grid.SetActive(!grid.activeSelf);
    }
}