using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playfield : MonoBehaviour
{
    public static int w = 10;
    public static int h = 20;
    public static Transform[,] grid = new Transform[w, h];

    // Round coordinates of groups to keep them clean
    public static Vector2 RoundVec2(Vector2 v)
    {
        return new Vector2(Mathf.Round(v.x),
                           Mathf.Round(v.y));
    }

    //Checks if the group is inside the border
    public static bool InsideBorder(Vector2 pos)
    {
        return ((int)pos.x >= 0 &&(int)pos.x < w && (int)pos.y >= 0);
    }

    // Deletes a row of blocks
    public static void DeleteRow(int y)
    {
        for (int x = 0; x < w; x++)
        {
            Destroy(grid[x, y].gameObject);
            grid[x, y] = null;
        }
    }

    // Move a row down
    public static void DecreaseRow(int y)
    {
        for (int x = 0; x < w; x++)
        {
            if (grid[x, y] != null)
            {
                // Move one towards bottom
                grid[x, y - 1] = grid[x, y];
                grid[x, y] = null;

                // Update Block position
                grid[x, y - 1].position += new Vector3(0, -1, 0);
            }
        }
    }

    // Move all the rows down
    public static void DecreaseRowsAbove(int y)
    {
        for (int i = y; i < h; i++)
            DecreaseRow(i);
    }

    // Scan if a row is full
    public static bool IsRowFull(int y)
    {
        for (int x = 0; x < w; ++x)
            if (grid[x, y] == null)
                return false;
        return true;
    }

    // Does the pesky row removal B)
    public static void DeleteFullRows()
    {
        for (int y = 0; y < h; y++)
        {
            if (IsRowFull(y))
            {
                DeleteRow(y);
                DecreaseRowsAbove(y + 1);
                y--;
            }
        }
    }
}
