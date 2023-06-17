using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rules/Multiplying Rule")]
public class MultiplyValueRule : Rule
{
    public Color multiplier;
    public override Vector3 ApplyRuleOnCell(int x, int y, Board board)
    {
        Vector3 currentValues = board.GetCell(x, y).value;
        return new Vector3(currentValues.x * multiplier.r, currentValues.y * multiplier.g, currentValues.z * multiplier.b);
    }
}
