using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour
{

    SimulationBoard simulationBoard;
    Texture2D texture;

    void Start()
    {
        simulationBoard = new SimulationBoard(50, 50);
        texture = new Texture2D(simulationBoard.width, simulationBoard.height, TextureFormat.RGBA32, false);
        texture.filterMode = FilterMode.Point;
        texture.wrapMode = TextureWrapMode.Clamp;

        // Create a new Material with the Unlit/Texture shader
        Material material = new Material(Shader.Find("Unlit/Texture"));
        material.mainTexture = texture;

        GetComponent<MeshRenderer>().material = material;

        InitializeBoard();
        UpdateTexture();
    }


    void InitializeBoard()
    {
        // Clear the board
        for (int y = 0; y < simulationBoard.height; y++)
        {
            for (int x = 0; x < simulationBoard.width; x++)
            {
                simulationBoard.SetCell(x, y, Vector3.zero);
            }
        }

        // Draw a glider
        simulationBoard.SetCell(1, 0, Vector3.one);
        simulationBoard.SetCell(2, 1, Vector3.one);
        simulationBoard.SetCell(0, 2, Vector3.one);
        simulationBoard.SetCell(1, 2, Vector3.one);
        simulationBoard.SetCell(2, 2, Vector3.one);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            simulationBoard.Step();
            UpdateTexture();
        }
    }

    void UpdateTexture()
    {
        for (int y = 0; y < simulationBoard.height; y++)
        {
            for (int x = 0; x < simulationBoard.width; x++)
            {
                Vector3 value = simulationBoard.GetCell(x, y).value;
                texture.SetPixel(simulationBoard.width - x - 1, y, new Color(value.x, value.y, value.z));
            }
        }

        texture.Apply();
    }
}
