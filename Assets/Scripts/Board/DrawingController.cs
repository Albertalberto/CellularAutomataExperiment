using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawingController : MonoBehaviour
{
    // Define a new event that takes two integers (the cell coordinates)
    public static event Action<float, float, Vector3, int, int> OnCellClicked;

    public static readonly int CIRCLE_BRUSH = 1;
    public static readonly int SQUARE_BRUSH = 2;

    public Color currentColor;
    public int brushSize = 1;
    public int brushType = 1;

    void Update()
    {
        if (Input.GetMouseButton(0)) // if left button pressed...
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                // Get the point where the mouse ray intersected the mesh
                Vector3 clickedPosition = hit.point;
                // Invoke the event, passing the cell coordinates
                OnCellClicked?.Invoke(clickedPosition.x, clickedPosition.y, new Vector3(currentColor.r, currentColor.g, currentColor.b), brushSize, brushType);
            }
        }
    }
}

public struct brushData {
    float x;
    float y;
    Vector3 color;
    int brushSize;
}

