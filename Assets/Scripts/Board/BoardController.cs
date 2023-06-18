using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour
{

    SimulationBoard simulationBoard;
    Texture2D texture;
    MeshFilter mesh;

    [SerializeField]
    List<Rule> rules;

    bool simulationActive = false;
    private int simulationSpeed = 3;
    private int frameCount = 0;

    int width = 50;
    int height = 50;

    void Start()
    {
        mesh = GetComponent<MeshFilter>();

        simulationBoard = new SimulationBoard(width, height);
        simulationBoard.SetRules(rules);

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


    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            simulationBoard.Step();
            UpdateTexture();
        }

        frameCount++;

        if (simulationActive && frameCount >= simulationSpeed)
        {
            simulationBoard.Step();
            UpdateTexture();
            frameCount = 0;
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

    //External events
    private void OnEnable()
    {
        MenuController.OnToggleSimulation += SetSimulationActive;
        MenuController.OnSimulationSpeedChanged += HandleSimulationSpeedChanged;
        DrawingController.OnCellClicked += HandleCellClicked;
    }

    private void OnDisable()
    {
        MenuController.OnToggleSimulation -= SetSimulationActive;
        MenuController.OnSimulationSpeedChanged -= HandleSimulationSpeedChanged;
        DrawingController.OnCellClicked -= HandleCellClicked;
    }

    void SetSimulationActive(bool isSimulationActive)
    {
        simulationActive = isSimulationActive;
    }

    private void HandleSimulationSpeedChanged(int newSpeed)
    {
        simulationSpeed = newSpeed;
    }

    void HandleCellClicked(float x, float y, Vector3 value)
    {
        // Get the bounds of the mesh
        Bounds meshBounds = GetComponent<MeshRenderer>().bounds;

        // Calculate the clicked position relative to the center of the mesh
        Vector2 clickedPosition = new Vector2(x - meshBounds.center.x, y - meshBounds.center.y);

        // Scale the clicked position to be within the range (-0.5, -0.5) to (0.5, 0.5)
        clickedPosition.x /= meshBounds.size.x;
        clickedPosition.y /= meshBounds.size.y;

        // Translate the clicked position to be within the range (0, 0) to (1, 1)
        clickedPosition.x = clickedPosition.x + 0.5f;
        clickedPosition.y = 1.0f - (clickedPosition.y + 0.5f); // Inverted

        // Convert the relative coordinates to cell coordinates in the board
        int xCoord = Mathf.FloorToInt(clickedPosition.x * simulationBoard.width);
        int yCoord = Mathf.FloorToInt(clickedPosition.y * simulationBoard.height);

        // Ensure that the coordinates are within the board's bounds
        xCoord = Mathf.Clamp(xCoord, 0, simulationBoard.width - 1);
        yCoord = Mathf.Clamp(yCoord, 0, simulationBoard.height - 1);

        // Update the cell
        if (simulationBoard.SetCell(xCoord, yCoord, value))
        {
            UpdateTexture();
        }
    }




}