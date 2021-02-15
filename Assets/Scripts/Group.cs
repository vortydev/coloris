using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Group : MonoBehaviour
{
    public int pieceId;
    public bool beenSwapped;

    private bool paused;
    private bool canHold;

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
        canHold = FindObjectOfType<CanHold>().canHold;

        if (isMoveable && !paused)
        {
            // Move Left
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                transform.position += new Vector3(-1, 0, 0);

                if (IsValidGridPos())
                    UpdateGrid();
                else
                    transform.position += new Vector3(1, 0, 0);
            }
            // Move Right
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                transform.position += new Vector3(1, 0, 0);

                if (IsValidGridPos())
                    UpdateGrid();
                else
                    transform.position += new Vector3(-1, 0, 0);
            }
            // Rotate right
            else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown("x"))
            {
                if (gameObject.tag == "SquarePiece") return;

                transform.Rotate(0, 0, -90);

                if (IsValidGridPos())
                    UpdateGrid();
                else
                    transform.Rotate(0, 0, 90);
            }
            // Rotate left
            else if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown("z"))
            {
                if (gameObject.tag == "SquarePiece") return;

                transform.Rotate(0, 0, 90);

                if (IsValidGridPos())
                    UpdateGrid();
                else
                    transform.Rotate(0, 0, -90);
            }
            // Move Downwards and Fall
            else if (Input.GetKeyDown(KeyCode.DownArrow) || Time.time - lastFall >= 1 - (localDifficulty / 10))
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
            else if ((Input.GetKeyDown("c") || Input.GetKeyDown(KeyCode.LeftShift)) && !beenSwapped && canHold) // hold piece
            {
                FindObjectOfType<HoldPiece>().HoldCurrentPiece(gameObject);
            }
        }   

        // Hard drop
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isMoveable = false;

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
            //FindObjectOfType<Screenshake>().TriggerScreenshake();
            FindObjectOfType<Spawner>().SpawnNext();

            enabled = false;
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
