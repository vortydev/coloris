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
    private void Awake()
    {
        if (PlayerPrefs.GetInt("game_grid") == 0)
        {
            grid.SetActive(false);
            gridToggle.SetIsOnWithoutNotify(false);
        }

        if (PlayerPrefs.GetInt("next_piece") == 0)
        {
            nextPieceUI.gameObject.SetActive(false);
            nextPieceToggle.SetIsOnWithoutNotify(false);
        }
    }

    public void ToggleGrid()
    {
        grid.SetActive(!grid.activeSelf);
        SettingsManager.ToggleBoolPlayerPrefs("game_grid");
    }

    public void ToggleNextPiece()
    {
        nextPieceUI.gameObject.SetActive(!nextPieceUI.gameObject.activeSelf);
        SettingsManager.ToggleBoolPlayerPrefs("next_piece");
    }
}