using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Group : MonoBehaviour
{
    public int pieceId;
    public bool beenSwapped;

    private bool paused;
    private bool canHold;
    private bool canHardDrop;

    private float lastFall = 0;
    private bool isMoveable = true;
    private bool hasMoved = false;

    private float localDifficulty;

    private void Start()
    {
        localDifficulty = FindObjectOfType<Score>().globalDifficulty;

        // Default position not valid? Then it's game over
        if (!IsValidGridPos())
        {
            TriggerGameOver();
        }
    }

    private void Update()
    {
        paused = FindObjectOfType<PauseMenu>().gamePaused;
        canHold = FindObjectOfType<CanDo>().canHold;
        canHardDrop = FindObjectOfType<CanDo>().canHardDrop;

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
                    if (hasMoved)
                    {
                        // It's not valid. revert.
                        transform.position += new Vector3(0, 1, 0);

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

                lastFall = Time.time;
            }
        }   
    }

    public void OnMove(int direction)
    {
        if (isMoveable && !paused)  // if the piece can move and the game isn't paused
        {   
            transform.position += new Vector3(direction, 0, 0);    // move the piece left

            if (IsValidGridPos())   // checks if the new position is valid
            {
                UpdateGrid();                                   // updates the grid with the moved piece
                FindObjectOfType<SFXManager>().MoveSideSFX();   // plays an SFX
            }
            else
            {
                transform.position += new Vector3(-direction, 0, 0);     // reverts to its initial position
            }
        }
    }

    public void OnRotateRight()
    {
        if (gameObject.tag == "SquarePiece") return;

        transform.Rotate(0, 0, -90);

        if (IsValidGridPos())
        {
            FindObjectOfType<SFXManager>().RotateSFX();
            UpdateGrid();
        }
        else
            transform.Rotate(0, 0, 90);
    }

    public void OnRotateLeft()
    {
        if (gameObject.tag == "SquarePiece") return;

        transform.Rotate(0, 0, 90);

        if (IsValidGridPos())
        {
            FindObjectOfType<SFXManager>().RotateSFX();
            UpdateGrid();
        }
        else
            transform.Rotate(0, 0, -90);
    }

    public void OnSoftDrop()
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
            if (hasMoved)
            {
                // It's not valid. revert.
                transform.position += new Vector3(0, 1, 0);

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

        lastFall = Time.time;
    }

    public void OnHardDrop()
    {
        if (canHardDrop)
        {
            isMoveable = false;

            FindObjectOfType<SFXManager>().HardDropSFX();

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

            enabled = false;
        }
    }

    public void OnHold()
    {
        if (!beenSwapped && canHold) // hold piece
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
        Debug.Log("GAME OVER");
        Destroy(gameObject);

        FindObjectOfType<GameOver>().GameOverRoutine();
    }
}
