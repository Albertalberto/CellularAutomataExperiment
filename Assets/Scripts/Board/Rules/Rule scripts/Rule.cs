using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Rule : ScriptableObject
{
    public abstract Vector3 ApplyRuleOnCell(int x, int y, Board board);
}
