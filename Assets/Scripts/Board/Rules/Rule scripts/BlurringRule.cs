using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rules/Blurring Rule")]
public class BlurringRule : Rule
{

    public override Vector3 ApplyRuleOnCell(int x, int y, Board board)
    {
        Vector3 finalValue = Vector3.zero;
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {

                Vector3 currentCellValue = board.GetCell(x + i, y + j).value;
                finalValue += currentCellValue / 9;
            }
        }

        return finalValue;
    }
}
