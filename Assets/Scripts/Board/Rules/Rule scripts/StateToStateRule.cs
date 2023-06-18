using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Vector3Row
{
    public List<Vector3> row = new List<Vector3>();
}

[CreateAssetMenu(menuName = "Rules/StateToState Rule")]
public class StateToStateRule : Rule
{
    [SerializeField]
    private List<Vector3Row> initialState = new List<Vector3Row>();

    [SerializeField]
    private List<Vector3Row> finalState = new List<Vector3Row>();

    public override Vector3 ApplyRuleOnCell(int x, int y, Board board)
    {
        Vector3 currentCellValue = board.GetCell(x, y).value;

        Vector3 value = AreaMatchesState(x, y, board, initialState);
        if (value != new Vector3(-1, -1, -1))
        {
            return value;
        }

        return currentCellValue;
    }



    private Vector3 AreaMatchesState(int x, int y, Board board, List<Vector3Row> stateToCheck)
    {
        for (int i = 0; i < stateToCheck.Count; i++)
        {
            for (int j = 0; j < stateToCheck[i].row.Count; j++)
            {
                if (stateToCheck[i].row[j] == board.GetCell(x, y).value)
                {
                    if (CheckIfStateMatches(x - j, y - i, board, stateToCheck))
                    {
                        return finalState[i].row[j];
                    }
                }
            }
        }

        return new Vector3(-1, -1, -1);
    }


    private bool CheckIfStateMatches(int x, int y, Board board, List<Vector3Row> stateToCheck)
    {
        for (int i = 0; i < stateToCheck.Count; i++)
        {
            for (int j = 0; j < stateToCheck[i].row.Count; j++)
            {

                if (stateToCheck[i].row[j] != board.GetCell(x + j, y + i).value)
                {
                    return false;
                }
            }
        }
        return true;
    }

}
