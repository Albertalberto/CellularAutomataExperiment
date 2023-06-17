using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulationBoard
{
    private Board board;

    public int width { get; private set; }

    public int height { get; private set; }
    public SimulationBoard(int width, int height)
    {
        this.width = width;
        this.height = height;
        board = new Board(width, height);
    }

    public void Step()
    {
        // Copy of the current board state
        Cell[,] copy = new Cell[board.width, board.height];
        for (int y = 0; y < board.height; y++)
        {
            for (int x = 0; x < board.width; x++)
            {
                Vector3 currentValue = board.GetCell(x, y).value;
                copy[x, y] = new Cell(currentValue);
            }
        }

        // Apply rules to each cell
        for (int y = 0; y < board.height; y++)
        {
            for (int x = 0; x < board.width; x++)
            {
                ApplyRules(x, y, copy);
            }
        }
    }

    private void ApplyRules(int x, int y, Cell[,] copy)
    {
        // Count the number of alive neighbors
        int aliveNeighbors = 0;

        for (int yOffset = -1; yOffset <= 1; yOffset++)
        {
            for (int xOffset = -1; xOffset <= 1; xOffset++)
            {
                if (xOffset == 0 && yOffset == 0)
                    continue;

                int neighborX = x + xOffset;
                int neighborY = y + yOffset;

                if (neighborX >= 0 && neighborY >= 0 && neighborX < board.width && neighborY < board.height)
                {
                    Vector3 value = copy[neighborX, neighborY].value;
                    if (value == Vector3.one) // Check if the neighbor cell is alive
                    {
                        aliveNeighbors++;
                    }
                }
            }
        }

        // Apply the Game of Life rules
        Vector3 currentCell = board.GetCell(x, y).value;
        if (currentCell == Vector3.one && (aliveNeighbors < 2 || aliveNeighbors > 3))
        {
            // Any live cell with fewer than two live neighbors dies, as if by underpopulation.
            // Any live cell with more than three live neighbors dies, as if by overpopulation.
            board.SetCell(x, y, Vector3.zero);
        }
        else if (currentCell == Vector3.zero && aliveNeighbors == 3)
        {
            // Any dead cell with exactly three live neighbors becomes a live cell, as if by reproduction.
            board.SetCell(x, y, Vector3.one);
        }
    }


    public Cell GetCell(int x, int y)
    {
        return board.GetCell(x, y);
    }

    public void SetCell(int x, int y, Vector3 value)
    {
        board.SetCell(x, y, value);
    }

}
