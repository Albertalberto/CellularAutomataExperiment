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

    bool stepFlag;
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
        simulationBoard.SetCell(2, 1, Vector3.one);
        simulationBoard.SetCell(3, 1, new Vector3(1, 0, 0));
    }

    private void Update()
    {
        frameCount++;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            stepFlag = true;
        }

    }


    void FixedUpdate()
    {
        if (stepFlag)
        {
            takeStep();
            stepFlag = false;
        }

        frameCount++;
        if (simulationActive && frameCount >= simulationSpeed)
        {
            frameCount = 0;
            stepFlag = true;
        }
    }

    void takeStep()
    {
        simulationBoard.Step();
        UpdateTexture();
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

    void HandleCellClicked(float x, float y, Vector3 value, int brushSize, int brushType)
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


        xCoord = Mathf.Clamp(xCoord, 0, simulationBoard.width - 1);
        yCoord = Mathf.Clamp(yCoord, 0, simulationBoard.height - 1);

        bool updated = false;

        brushSize--;

        if (brushType == DrawingController.SQUARE_BRUSH)
        {
            for (int i = -brushSize; i <= brushSize; i++)
            {
                for (int j = -brushSize; j <= brushSize; j++)
                {
                    int currentX = xCoord + i;
                    int currentY = yCoord + j;

                    if (currentX >= 0 && currentX < simulationBoard.width &&
                        currentY >= 0 && currentY < simulationBoard.height)
                    {
                        updated |= simulationBoard.SetCell(currentX, currentY, value);
                    }
                }
            }
        } else if (brushType == DrawingController.CIRCLE_BRUSH)
        {
            for (int i = -brushSize; i <= brushSize; i++)
            {
                for (int j = -brushSize; j <= brushSize; j++)
                {
                    // Check if the point falls within the circle
                    if (i * i + j * j <= brushSize * brushSize)
                    {
                        int currentX = xCoord + i;
                        int currentY = yCoord + j;

                        if (currentX >= 0 && currentX < simulationBoard.width &&
                            currentY >= 0 && currentY < simulationBoard.height)
                        {
                            updated |= simulationBoard.SetCell(currentX, currentY, value);
                        }
                    }
                }
            }
        }
        
        if (updated)
        {
            UpdateTexture();
        }
    }




}
