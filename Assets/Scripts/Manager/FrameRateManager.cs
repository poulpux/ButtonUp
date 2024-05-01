using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameRateManager : MonoBehaviour
{
    void Awake()
    {
        Application.targetFrameRate = 100;
    }
}
