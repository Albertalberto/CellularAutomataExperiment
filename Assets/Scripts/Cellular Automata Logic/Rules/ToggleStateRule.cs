using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rules/Toggle State Rule")]
public class ToggleStateRule : Rule
{
    public override Vector3 ApplyRuleOnCell(int x, int y, Board board)
    {
        return board.GetCell(x, y).value == Vector3.zero ? Vector3.one : Vector3.zero;
    }
}

