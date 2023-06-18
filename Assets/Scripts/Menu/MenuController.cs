using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public static event Action OnToggleSimulation;

    public void ToggleSimulation()
    {
        OnToggleSimulation?.Invoke();
    }
}

