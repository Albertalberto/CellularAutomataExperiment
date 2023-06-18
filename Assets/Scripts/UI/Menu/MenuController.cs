using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public static event Action<bool> OnToggleSimulation;

    public void ToggleSimulation(bool isSimulationActive)
    {
        OnToggleSimulation?.Invoke(isSimulationActive);
    }
    
    public static event Action<int> OnSimulationSpeedChanged;

    public void OnSimulationSpeedSliderValueChanged(float value)
    {
        ChangeSimulationSpeed(Mathf.RoundToInt(value));
    }

    public void ChangeSimulationSpeed(int newSpeed)
    {
        OnSimulationSpeedChanged?.Invoke(newSpeed);
    }
}


