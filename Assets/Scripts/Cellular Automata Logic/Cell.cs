using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell
{
    public Vector3 value;


    public Cell()
    {
        value = Vector3.zero;
    }

    public Cell(Vector3 value)
    {
        this.value = value;
    }
}
