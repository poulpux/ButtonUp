using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshPro))]
public class Theme : MonoBehaviour
{
    public List<SubScene> allSubScene = new List<SubScene>();
    public string nameTheme = "default";
    [HideInInspector] public int weight;
    private TextMeshPro text;

    private void Start()
    {
        weight = 1 + allSubScene.Count;
        text = GetComponent<TextMeshPro>();
        text.text = nameTheme;

        InstantiateAllSubScene();
    }

    private void InstantiateAllSubScene()
    {
        for (int i = 0; i < allSubScene.Count; i++)
        {
            GameObject subScene = Instantiate(allSubScene[i].gameObject, transform);
            print(Vector3.up * -2f * (i + 1));
            subScene.transform.localPosition = Vector3.up  *ScrollingMenu_Singleton.Instance.distSubSceneY * (i+1)+Vector3.right *ScrollingMenu_Singleton.Instance.distSubSceneX;
        }
    }
}
