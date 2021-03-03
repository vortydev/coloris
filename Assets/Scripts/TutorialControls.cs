using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialControls : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] Button backButton;
    [SerializeField] Button nextButton;
    [SerializeField] Button backToMain;
    [SerializeField] Button closeButton;
    public int controlsNav = 1;

    [Header("Piece")]
    [SerializeField] GameObject piece;
    public float movementOffset;
    public float resetDelay = 0.25f;
    public int piecePosX = 0;
    public int piecePosY = 0;

    [Header("Holding Piece")]
    [SerializeField] GameObject holdPieceUI;
    [SerializeField] GameObject heldPiece;

    [Header("Piece SFX")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip move;
    [SerializeField] AudioClip rotate;
    [SerializeField] AudioClip hold;

    private void Awake()
    {
        DisableNavButtons();

        heldPiece.SetActive(false);
        holdPieceUI.SetActive(false);
    }

    private void Update()
    {
        if (controlsNav == 1 && piecePosX < 2 && Input.GetKeyDown(KeyCode.RightArrow))
        {
            MovePieceRight();
            piecePosX++;
        }
        else if (controlsNav == 1 && piecePosX > -2 && Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MovePieceLeft();
            piecePosX--;
        }
        else if (controlsNav == 2 && (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown("x"))) {
            RotatePieceRight();
        }
        else if (controlsNav == 2 && (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown("z"))) {
            RotatePieceLeft();
        }
        else if (controlsNav == 3 && Input.GetKeyDown(KeyCode.DownArrow))
        {
            piecePosY++;
            SoftDrop();
        }
        //else if (controlsNav == 3 && Input.GetKeyDown(KeyCode.Space))
        //{
        //    HardDrop();
        //}
        else if (controlsNav == 4 && (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown("c")))
        {
            TogglePieceHeld();
        }
    }

    private void PlayPieceSfx(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }

    private void MovePieceRight()
    {
        Vector2 newPos = piece.transform.position;
        newPos.x += movementOffset;
        piece.transform.position = newPos;

        PlayPieceSfx(move);
    }

    private void MovePieceLeft()
    {
        Vector2 newPos = piece.transform.position;
        newPos.x -= movementOffset;
        piece.transform.position = newPos;

        PlayPieceSfx(move);
    }

    private IEnumerator ResetPieceToMiddle()
    {
        while (piecePosX != 0)
        {
            yield return new WaitForSeconds(resetDelay);

            if (piecePosX > 0)
            {
                MovePieceLeft();
                piecePosX--;
            }
            
            if (piecePosX < 0) {
                MovePieceRight();
                piecePosX++;
            }
        }
    }

    private void RotatePieceRight()
    {
        piece.transform.Rotate(0, 0, -90);

        PlayPieceSfx(rotate);
    }

    private void RotatePieceLeft()
    {
        piece.transform.Rotate(0, 0, 90);

        PlayPieceSfx(rotate);
    }

    private IEnumerator ResetPieceUp()
    {
        while (piece.transform.rotation.z != 0)
        {
            yield return new WaitForSeconds(resetDelay);

            RotatePieceRight();
        }
    }

    private void SoftDrop()
    {
        Vector2 newPos = piece.transform.position;
        newPos.y -= movementOffset;
        piece.transform.position = newPos;

        if (piecePosY > 2)
        {
            StartCoroutine(ResetPieceBackUp());
        }
    }

    private void HardDrop()
    {
        for (int i = 0; i < 3; i++)
        {
            SoftDrop();
            piecePosY++;
        }

        StartCoroutine(ResetPieceBackUp());
    }

    private IEnumerator ResetPieceBackUp()
    {
        while (piecePosY != 0)
        {
            yield return new WaitForSeconds(resetDelay);

            if (piecePosY > 0)
            {
                Vector2 newPos = piece.transform.position;
                newPos.y += movementOffset;
                piece.transform.position = newPos;

                piecePosY--;
            }
        }
    }

    private void TogglePieceHeld()
    {
        piece.SetActive(!piece.activeSelf);
        heldPiece.SetActive(!heldPiece.activeSelf);

        PlayPieceSfx(hold);
    }

    public void OnNextClick()
    {
        controlsNav++;
        UpdateControlsPage();
    }

    public void OnBackClick()
    {
        controlsNav--;
        UpdateControlsPage();
    }

    public void ResetControlsNav()
    {
        controlsNav = 1;
        UpdateControlsPage();
    }

    public void DisableNavButtons()
    {
        backButton.gameObject.SetActive(false);
        nextButton.gameObject.SetActive(false);
        backToMain.gameObject.SetActive(false);
        closeButton.gameObject.SetActive(false);
    }

    public void UpdateControlsPage()
    {
        DisableNavButtons();

        switch (controlsNav)
        {
            case 1:
                FindObjectOfType<TutorialsManager>().PlayControls(controlsNav);
                break;
            case 2:
                FindObjectOfType<TutorialsManager>().PlayControls(controlsNav);
                StartCoroutine(ResetPieceToMiddle());
                break;
            case 3:
                FindObjectOfType<TutorialsManager>().PlayControls(controlsNav);
                StartCoroutine(ResetPieceUp());
                break;
            case 4:
                FindObjectOfType<TutorialsManager>().PlayControls(controlsNav);
                break;
        }

        if (controlsNav == 4)
        {
            holdPieceUI.SetActive(true);
        }
        else
        {
            holdPieceUI.SetActive(false);
        }

        if (controlsNav != 4)
        {
            heldPiece.SetActive(false);
            piece.SetActive(true);
        }
    }

    public void UpdateNavButtons()
    {
        switch (controlsNav)
        {
            case 1:
                nextButton.gameObject.SetActive(true);
                backToMain.gameObject.SetActive(true);
                break;
            case 2:
            case 3:
                backButton.gameObject.SetActive(true);
                nextButton.gameObject.SetActive(true);
                break;
            case 4:
                backButton.gameObject.SetActive(true);
                closeButton.gameObject.SetActive(true);
                break;
        }
    }
}
