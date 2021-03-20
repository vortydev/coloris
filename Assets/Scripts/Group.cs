/*
 * File:        Group.cs
 * Author:      Étienne Ménard
 * Description: Takes care of everything related to the game pieces.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Group : MonoBehaviour
{
    public int pieceId;             // id of the piece
    public bool beenSwapped;        // has the piece been already swapped

    private bool paused;            // is the game paused

    private float lastFall = 0;     // time since the last drop
    private bool isMoveable = true; // can the piece be moved
    private bool hasMoved = false;  // has the piece moved  

    private float localDifficulty;  // value that affects the time it takes for the piece to fall
    private float lockDelay;        // value that affects the time it takes for a piece to lock

    [Header("Can Do")]
    private CanDo perms;
    private bool canHold;           // can the piece be held
    private bool canHardDrop;       // can the piece be hard dropped
    private bool canGhost;          // is the ghost pece mechanic enabled

    [SerializeField] GameObject ghost;

    private void Start()
    {
        perms = FindObjectOfType<CanDo>();

        localDifficulty = FindObjectOfType<Score>().globalDifficulty;   // loads the current difficulty
        lockDelay = perms.lockDelay / 10;                               // loads locking delay

        // Default position not valid? Then it's game over
        if (!IsValidGridPos())
        {
            TriggerGameOver();
        }

        SpawnGhost();
    }

    private void Update()
    {
        paused = FindObjectOfType<PauseMenu>().gamePaused;
        canHold = perms.canHold;
        canHardDrop = perms.canHardDrop;
        //canGhost = perms.canGhost;

        if (isMoveable && !paused)
        {
            if (Time.time - lastFall >= 1 - (localDifficulty / 10))
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

                    StartCoroutine(LockingTimer());
                }

                lastFall = Time.time;
            }
        }   
    }

    // move the piece
    public void OnMove(int direction)
    {
        if (isMoveable && !paused)  // if the piece can move and the game isn't paused
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
        if (isMoveable && !paused)
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
        if (isMoveable && !paused)
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
        if (isMoveable && !paused)
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

                StartCoroutine(LockingTimer());
            }

            lastFall = Time.time;
        }
    }

    public void OnHardDrop()
    {
        if (canHardDrop && isMoveable && !paused)
        {
            isMoveable = false; // locks the piece

            FindObjectOfType<GameSFX>().HardDropSFX();

            for (int i = 0; i < Playfield.h; i++)
            {
                transform.position += new Vector3(0, -1, 0);

                if (IsValidGridPos())
                {
                    UpdateGrid();
                }
                else
                {
                    transform.position += new Vector3(0, 1, 0);
                }
            }

            Playfield.DeleteFullRows();
            FindObjectOfType<Spawner>().SpawnNext();

            DestroyGhost();

            enabled = false;
        }
    }

    public void OnHold()
    {
        if (!beenSwapped && canHold && isMoveable && !paused) // hold piece
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
        return true;
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
        lastFall = Time.time;
    }

    private void HasMoved()
    {
        if (!hasMoved)
        {
            hasMoved = true;
        }
    }

    private void TriggerGameOver()
    {
        Destroy(gameObject);

        FindObjectOfType<GameOver>().GameOverRoutine();
    }

    private IEnumerator LockingTimer()
    {
        yield return new WaitForSeconds(lockDelay);

        DestroyGhost();

        if (hasMoved)
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
        canGhost = perms.canGhost;

        if (canGhost)
        {
            ghost = Instantiate(FindObjectOfType<Spawner>().ghostPieces[pieceId - 1], transform.position, Quaternion.identity);
            ghost.name = "Ghost";

            UpdateGhostPosition();
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
