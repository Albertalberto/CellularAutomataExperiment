using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board
{
    public int width { get; private set; }
    public int height { get; private set; }
    public Cell[,] cells { get; private set; }

    public Board(int width, int height)
    {
        this.width = width;
        this.height = height;
        cells = new Cell[width, height];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                cells[x, y] = new Cell();
            }
        }
    }

    public Cell GetCell(int x, int y)
    {
        if (x < 0 || y < 0 || x >= width || y >= height)
        {
            return null;
        }
        return cells[x, y];
    }

    public void SetCell(int x, int y, Vector3 value)
    {
        if (x < 0 || y < 0 || x >= width || y >= height)
        {
            return;
        }
        cells[x, y].value = value;
    }
}

