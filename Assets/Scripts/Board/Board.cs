using System;
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

    public void SetBoard(Cell[,] cells)
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                this.cells[x, y].value = cells[x, y].value;
            }
        }
    }


    public Cell GetCell(int x, int y)
    {
        // Wrap around if x or y is outside the board's bounds
        x = WrapAround(x, width);
        y = WrapAround(y, height);
        return cells[x, y];
    }

    public Vector3 GetCellValue(int x, int y)
    {
        // Wrap around if x or y is outside the board's bounds
        x = WrapAround(x, width);
        y = WrapAround(y, height);
        return cells[x, y].value;
    }

    public void SetCell(int x, int y, Vector3 value)
    {
        // Wrap around if x or y is outside the board's bounds
        x = WrapAround(x, width);
        y = WrapAround(y, height);
        cells[x, y].value = value;
    }

    private int WrapAround(int value, int max)
    {
        if (value < 0)
        {
            value = (value % max + max) % max;
        }
        else
        {
            value = value % max;
        }
        return value;
    }


    internal void SetCellValue(int x, int y, Vector3 value)
    {
        x = WrapAround(x, width);
        y = WrapAround(y, height);
        cells[x, y].value = value;
    }
}
