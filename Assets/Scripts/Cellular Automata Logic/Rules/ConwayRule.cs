using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rules/Conway Rule")]
public class ConwayRule : Rule
{
    public override Vector3 ApplyRuleOnCell(int x, int y, Board board)
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
                    Vector3 value = board.GetCellValue(neighborX, neighborY);  // change from (x, y) to (neighborX, neighborY)
                    if (value == Vector3.one)
                    {
                        aliveNeighbors++;
                    }
                }
            }
        }

        Vector3 currentCell = board.GetCell(x, y).value;
        if (currentCell == Vector3.one && (aliveNeighbors < 2 || aliveNeighbors > 3))
        {
            return Vector3.zero;
        }
        else if (currentCell == Vector3.zero && aliveNeighbors == 3)
        {
            return Vector3.one;
        }

        return currentCell; // if no condition applies, return the current cell's state
    }

}
