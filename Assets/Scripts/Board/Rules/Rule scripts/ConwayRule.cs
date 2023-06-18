using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rules/Conway Rule")]
public class ConwayRule : Rule
{
    public override Vector3 ApplyRuleOnCell(int x, int y, Board board)
    {
        int aliveNeighbours = 0;
        Vector3 alive = new Vector3(1, 1, 1);
        Vector3 dead = Vector3.zero;

        // Count the alive neighbours
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (i == 0 && j == 0)
                    continue;

                Vector3 neighbourValue = board.GetCell(x + i, y + j).value;
                if (neighbourValue == alive)
                    aliveNeighbours++;
            }
        }

        Vector3 currentValue = board.GetCell(x, y).value;

        // Apply the rules of Game of Life
        if (currentValue == alive && (aliveNeighbours < 2 || aliveNeighbours > 3))
        {
            // Any live cell with fewer than two live neighbours dies, as if by underpopulation.
            // Any live cell with more than three live neighbours dies, as if by overpopulation.
            return dead;
        }
        else if (aliveNeighbours == 3)
        {
            // Any dead cell with exactly three live neighbours becomes a live cell, as if by reproduction.
            return alive;
        }
        else
        {
            // Any live cell with two or three live neighbours lives on to the next generation.
            return currentValue;
        }
    }

}
