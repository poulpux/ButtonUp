using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Theme : MonoBehaviour
{
    public List<SubScene> allSubScene = new List<SubScene>();
    public string nameTheme = "default";
    [HideInInspector] public int weight;

    private void Awake()
    {
        weight = 1 + allSubScene.Count;
    }
}
