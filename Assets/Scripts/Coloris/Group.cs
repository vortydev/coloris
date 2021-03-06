﻿/*
 * File:        Group.cs
 * Author:      Étienne Ménard
 * Description: Takes care of everything related to the game pieces.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Group : MonoBehaviour
{
    public int pieceId;         // id of the piece
    public bool beenSwapped;    // has the piece been already swapped
    private bool _paused;       // is the game paused

    private float _localDifficulty;  // value that affects the time it takes for the piece to fall
    private float _lockDelay;        // value that affects the time it takes for a piece to lock

    private float _lastFall = 0;            // time since the last drop
    private bool _isMoveable = true;        // can the piece be moved
    private bool _hasMoved = false;         // has the piece moved  
    private bool _allowedHardDrop = false;  // little buffer thing to prevent spam hard drop

    private CanDo _perms;           // file the piece reads its permissions from
    private bool _canHold;          // can the piece be held
    private bool _canHardDrop;      // can the piece be hard dropped
    private bool _canGhost;         // is the ghost piece mechanic enabled

    [SerializeField] GameObject ghost;
    private IEnumerator _lockingRoutine;

    private void Awake()
    {
        _lockingRoutine = LockingTimer();
        _perms = FindObjectOfType<CanDo>();
    }

    private void Start()
    {
        _localDifficulty = FindObjectOfType<Score>().globalDifficulty;  // loads the current difficulty
        _lockDelay = _perms.lockDelay / 10f;                             // loads locking delay

        // Default position not valid? Then it's game over
        if (!IsValidGridPos())
        {
            TriggerGameOver();
        }

        SpawnGhost();
    }

    private void Update()
    {
        _paused = FindObjectOfType<PauseMenu>().gamePaused;
        _canHold = _perms.canHold;

        if (_isMoveable && !_paused)
        {
            if (Time.time - _lastFall >= 1 - (_localDifficulty / 10))
            {
                // Modify position
                transform.position += new Vector3(0, -1, 0);

                // See if valid
                if (IsValidGridPos())
                {
                    // It's valid. Update grid.
                    UpdateGrid();
                    HasMoved();
                }
                else
                {
                    // It's not valid. revert.
                    transform.position += new Vector3(0, 1, 0);

                    StartCoroutine(_lockingRoutine);
                }

                _lastFall = Time.time;
            }
        }   
    }

    // move the piece
    public void OnMove(int direction)
    {
        if (_isMoveable && !_paused)  // if the piece can move and the game isn't paused
        {   
            transform.position += new Vector3(direction, 0, 0);    // move the piece left

            if (IsValidGridPos())   // checks if the new position is valid
            {
                UpdateGrid();                               // updates the grid with the moved piece
                FindObjectOfType<GameSFX>().MoveSideSFX();  // plays an SFX

                UpdateGhostPosition();
            }
            else
            {
                transform.position += new Vector3(-direction, 0, 0);     // reverts to its initial position
            }
        }
    }

    public void OnRotateRight()
    {
        if (_isMoveable && !_paused)
        {
            if (gameObject.tag == "SquarePiece") return;    // pointless to rotate the O piece

            transform.Rotate(0, 0, -90);

            if (IsValidGridPos())
            {
                FindObjectOfType<GameSFX>().RotateSFX();
                UpdateGrid();

                UpdateGhostRotation();
            }
            else
                transform.Rotate(0, 0, 90);
        }
    }

    public void OnRotateLeft()
    {
        if (_isMoveable && !_paused)
        {
            if (gameObject.tag == "SquarePiece") return;

            transform.Rotate(0, 0, 90);

            if (IsValidGridPos())
            {
                FindObjectOfType<GameSFX>().RotateSFX();
                UpdateGrid();

                UpdateGhostRotation();
            }
            else
                transform.Rotate(0, 0, -90);
        }
    }

    public void OnSoftDrop()
    {
        if (_isMoveable && !_paused)
        {
            // Modify position
            transform.position += new Vector3(0, -1, 0);

            // See if valid
            if (IsValidGridPos())
            {
                // It's valid. Update grid.
                UpdateGrid();
                HasMoved();
            }
            else
            {
                // It's not valid. revert.
                transform.position += new Vector3(0, 1, 0);

                StartCoroutine(_lockingRoutine);
            }

            _lastFall = Time.time;
        }
    }

    public void OnHardDrop()
    {
        _canHardDrop = _perms.canHardDrop;

        if (_canHardDrop && _allowedHardDrop && _isMoveable && !_paused && CanStillFall())
        {
            _isMoveable = false; // locks the piece's lateral movement

            FindObjectOfType<GameSFX>().HardDropSFX();  // plays sfx

            for (int i = 0; i < Playfield.h; i++)   // goes down the entire grid
            {
                transform.position += new Vector3(0, -1, 0);

                if (IsValidGridPos())   // if it still can move
                {
                    UpdateGrid();   // move down
                }
                else
                {
                    transform.position += new Vector3(0, 1, 0); 
                }
            }

            DestroyGhost();

            Playfield.DeleteFullRows();
            FindObjectOfType<Spawner>().SpawnNext();

            enabled = false;
        }
    }

    public void OnHold()
    {
        if (!beenSwapped && _canHold && _isMoveable && !_paused) // hold piece
        {
            FindObjectOfType<HoldPiece>().HoldCurrentPiece(gameObject);
        }
    }

    // Checks if the group is within border
    public bool IsValidGridPos()
    {
        foreach (Transform child in transform)
        {
            Vector2 v = Playfield.RoundVec2(child.position);

            // Not inside Border?
            if (!Playfield.InsideBorder(v))
                return false;

            // Block in grid cell (and not part of same group)?
            if (Playfield.grid[(int)v.x, (int)v.y] != null &&
                Playfield.grid[(int)v.x, (int)v.y].parent != transform)
                return false;
        }

        if (CanStillFall())
        {
            StopCoroutine(_lockingRoutine); // interrupt the locking routine
        }

        return true;
    }

    private bool CanStillFall()
    {
        transform.position += new Vector3(0, -1, 0);    // drops the piece down once more
        bool fall = IsValidGridPos();                   // checks if that space still valid
        transform.position += new Vector3(0, 1, 0);     // puts the piece back up
        return fall;                                    // returns if the piece can still fall
    }

    // Updates the grid with the group
    public void UpdateGrid()
    {
        // Remove old children from grid
        for (int y = 0; y < Playfield.h; ++y)
            for (int x = 0; x < Playfield.w; ++x)
                if (Playfield.grid[x, y] != null)
                    if (Playfield.grid[x, y].parent == transform)
                        Playfield.grid[x, y] = null;

        // Add new children to grid
        foreach (Transform child in transform)
        {
            Vector2 v = Playfield.RoundVec2(child.position);
            Playfield.grid[(int)v.x, (int)v.y] = child;
        }
    }

    public void ResetFallCounter()
    {
        _lastFall = Time.time;
    }

    private void HasMoved()
    {
        if (!_hasMoved)
        {
            _hasMoved = true;
            _allowedHardDrop = true;
        }
    }

    private void TriggerGameOver()
    {
        Destroy(gameObject);

        FindObjectOfType<GameOver>().GameOverRoutine();
    }

    private IEnumerator LockingTimer()
    {
        yield return new WaitForSeconds(_lockDelay);

        DestroyGhost();

        if (_hasMoved)
        {
            FindObjectOfType<GameSFX>().LockSFX();

            // Clear filled horizontal lines
            Playfield.DeleteFullRows();

            // Spawn next Group
            FindObjectOfType<Spawner>().SpawnNext();

            // Disable script
            enabled = false;
        }
        else
        {
            TriggerGameOver();
        }
    }

    public void SpawnGhost()
    {
        _canGhost = _perms.canGhost;

        if (_canGhost)
        {
            ghost = Instantiate(FindObjectOfType<Spawner>().ghostPieces[pieceId - 1], transform.position, Quaternion.identity);
            ghost.name = "Ghost";

            UpdateGhostRotation();
        }
    }

    public void DestroyGhost()
    {
        if (ghost != null)
        {
            Destroy(ghost);
        }
    }

    // Checks if the group is within border
    public bool IsValidGhostPos()
    {
        foreach (Transform child in ghost.transform)
        {
            Vector2 v = Playfield.RoundVec2(child.position);

            if (!Playfield.InsideBorder(v))
                return false;

            if (Playfield.grid[(int)v.x, (int)v.y] != null &&
                Playfield.grid[(int)v.x, (int)v.y].parent != ghost.transform &&
                !(Playfield.grid[(int)v.x, (int)v.y].parent == transform))
                return false;
        }
        return true;
    }

    public void UpdateGhostPosition()
    {
        if (ghost != null)
        {
            ghost.transform.position = transform.position;  // puts the ghost back at the piece's position

            while (IsValidGhostPos())
            {
                ghost.transform.position += new Vector3(0, -1, 0);
            }

            ghost.transform.position += new Vector3(0, 1, 0);
        }
    }

    public void UpdateGhostRotation()
    {
        if (ghost != null)
        {
            ghost.transform.rotation = transform.rotation;
            UpdateGhostPosition();
        }
    }
}
