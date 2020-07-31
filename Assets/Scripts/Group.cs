using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Group : MonoBehaviour
{
    // Time since last gravity tick
    private float lastFall = 0;
    private bool isMoveable = true;
    private int localDifficulty = 0;

    void Start()
    {
        localDifficulty = FindObjectOfType<Score>().ReturnDifficulty();
        // Default position not valid? Then it's game over
        if (!isValidGridPos())
        {
            Debug.Log("GAME OVER");
            Destroy(gameObject);
            ReloadScene();
        }
    }

    void ReloadScene()
    {
        new WaitForSeconds(5);
        SceneManager.LoadScene(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoveable)
        {
            // Move Left
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                transform.position += new Vector3(-1, 0, 0);

                if (isValidGridPos())
                    updateGrid();
                else
                    transform.position += new Vector3(1, 0, 0);
            }
            // Move Right
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                transform.position += new Vector3(1, 0, 0);

                if (isValidGridPos())
                    updateGrid();
                else
                    transform.position += new Vector3(-1, 0, 0);
            }
            // Rotate
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (gameObject.tag == "SquarePiece") return;

                transform.Rotate(0, 0, -90);

                if (isValidGridPos())
                    updateGrid();
                else
                    transform.Rotate(0, 0, 90);
            }
            // Move Downwards and Fall
            else if (Input.GetKeyDown(KeyCode.DownArrow) || Time.time - lastFall >= 1 - (localDifficulty * 0.1f))
            {
                // Modify position
                transform.position += new Vector3(0, -1, 0);

                // See if valid
                if (isValidGridPos())
                {
                    // It's valid. Update grid.
                    updateGrid();
                }
                else
                {
                    // It's not valid. revert.
                    transform.position += new Vector3(0, 1, 0);

                    // Clear filled horizontal lines
                    Playfield.deleteFullRows();

                    // Spawn next Group
                    FindObjectOfType<Spawner>().spawnNext();

                    // Disable script
                    enabled = false;
                }

                lastFall = Time.time;
            }
        }   

        // Hard drop
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isMoveable = false;

            for (int i = 0; i < 15; i++)
            {
                transform.position += new Vector3(0, -1, 0);

                if (isValidGridPos())
                {
                    updateGrid();
                }
                else
                {
                    transform.position += new Vector3(0, 1, 0);
                }
            }

            
            Playfield.deleteFullRowsAndShake();
            FindObjectOfType<Spawner>().spawnNext();

            enabled = false;
        }
    }

    // Checks if the group is within border
    bool isValidGridPos()
    {
        foreach (Transform child in transform)
        {
            Vector2 v = Playfield.roundVec2(child.position);

            // Not inside Border?
            if (!Playfield.insideBorder(v))
                return false;

            // Block in grid cell (and not part of same group)?
            if (Playfield.grid[(int)v.x, (int)v.y] != null &&
                Playfield.grid[(int)v.x, (int)v.y].parent != transform)
                return false;
        }
        return true;
    }

    // Updates the grid with the group
    void updateGrid()
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
            Vector2 v = Playfield.roundVec2(child.position);
            Playfield.grid[(int)v.x, (int)v.y] = child;
        }
    }
}
