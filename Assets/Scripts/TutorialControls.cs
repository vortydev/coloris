using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class TutorialControls : MonoBehaviour
{
    private MyControls _actions;

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
    private bool canDrop = true;

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
        _actions = new MyControls();

        DisableNavButtons();

        heldPiece.SetActive(false);
        holdPieceUI.SetActive(false);
    }

    private void OnEnable()
    {
        // enable the input
        _actions.Enable();

        // piece movement
        _actions.Coloris.Move.performed += Move;
        _actions.Coloris.RotateRight.performed += RotateRight;
        _actions.Coloris.RotateLeft.performed += RotateLeft;
        _actions.Coloris.SoftDrop.performed += SoftDrop;
        _actions.Coloris.HardDrop.performed += HardDrop;
        _actions.Coloris.Hold.performed += Hold;
    }

    private void OnDisable()
    {
        // disable the input
        _actions.Disable();

        _actions.Coloris.Move.performed -= Move;
        _actions.Coloris.RotateRight.performed -= RotateRight;
        _actions.Coloris.RotateLeft.performed -= RotateLeft;
        _actions.Coloris.SoftDrop.performed -= SoftDrop;
        _actions.Coloris.HardDrop.performed -= HardDrop;
        _actions.Coloris.Hold.performed -= Hold;
    }

    private void Move(InputAction.CallbackContext obj)
    {
        if (controlsNav == 1)
        {
            if (obj.ReadValue<float>() > 0 && piecePosX < 2)
            {
                MovePieceRight();
                piecePosX++;
            }
            else if (obj.ReadValue<float>() < 0 && piecePosX > -2)
            {
                MovePieceLeft();
                piecePosX--;
            }
        }
    }

    private void RotateRight(InputAction.CallbackContext obj)
    {
        if (controlsNav == 2)
        {
            OnRotatePieceRight();
        }
    }

    private void RotateLeft(InputAction.CallbackContext obj)
    {
        if (controlsNav == 2)
        {
            OnRotatePieceLeft();
        }
    }

    private void SoftDrop(InputAction.CallbackContext obj)
    {
        if (controlsNav == 3 && canDrop)
        {
            OnSoftDrop();
        }
    }

    private void HardDrop(InputAction.CallbackContext obj)
    {
        if (controlsNav == 3 && canDrop) 
        {
            OnHardDrop();
        }
    }

    private void Hold(InputAction.CallbackContext obj)
    {
        if (controlsNav == 4)
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

    private void OnRotatePieceRight()
    {
        piece.transform.Rotate(0, 0, -90);

        PlayPieceSfx(rotate);
    }

    private void OnRotatePieceLeft()
    {
        piece.transform.Rotate(0, 0, 90);

        PlayPieceSfx(rotate);
    }

    private IEnumerator ResetPieceUp()
    {
        while (piece.transform.rotation.z != 0)
        {
            yield return new WaitForSeconds(resetDelay);

            OnRotatePieceRight();
        }
    }

    private void OnSoftDrop()
    {
        Vector2 newPos = piece.transform.position;
        newPos.y -= movementOffset;
        piece.transform.position = newPos;

        piecePosY++;

        if (piecePosY > 2)
        {
            canDrop = false;
            StartCoroutine(ResetPieceBackUp());
        }
    }

    private void OnHardDrop()
    {
        canDrop = false;

        for (int i = 0; i < 3; i++)
        {
            OnSoftDrop();
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

        canDrop = true;
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
