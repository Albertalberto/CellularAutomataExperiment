using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulationBoard
{
    private Board board;

    public int width { get; private set; }
    public int height { get; private set; }

    List<Rule> rules = new List<Rule>();


    public SimulationBoard(int width, int height)
    {
        this.width = width;
        this.height = height;
        board = new Board(width, height);
    }

    public void SetRules(List<Rule> rules)
    {
        this.rules = rules;
    }

    public void Step()
    {
        // Create a copy of the current board
        Board boardCopy = new Board(board.width, board.height);
        boardCopy.SetBoard(board.cells);

        // Apply rules to each cell
        foreach (Rule rule in rules)
        {
            for (int y = 0; y < board.height; y++)
            {
                for (int x = 0; x < board.width; x++)
                {
                    // Use the copy of the board as the basis for applying the rules
                    ApplyRule(x, y, rule, boardCopy);
                }
            }
            boardCopy.SetBoard(board.cells);
        }
    }


    private void ApplyRule(int x, int y, Rule rule, Board baseBoard)
    {
        Vector3 newCellValue = rule.ApplyRuleOnCell(x, y, baseBoard);
        board.SetCell(x, y, newCellValue);  // Apply the changes to the original board
    }




    public Cell GetCell(int x, int y)
    {
        return board.GetCell(x, y);
    }

    public bool SetCell(int x, int y, Vector3 value)
    {
        if (board.GetCell(x, y).value != value)
        {
            board.SetCell(x, y, value);
            return true;
        }
        return false;
    }


}
