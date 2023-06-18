using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rules/Movement Rule")]
public class MovingRule : Rule
{
    public Vector2Int movement;
    public override Vector3 ApplyRuleOnCell(int x, int y, Board board)
    {
        return board.GetCellValue(x - movement.x, y + movement.y);
    }
}
