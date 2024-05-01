using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshPro))]
public class Theme : MonoBehaviour
{
    [SerializeField] private List<SubSceneScriptableObject> allSubScene = new List<SubSceneScriptableObject>();
    [SerializeField] private string nameTheme = "default";
    [SerializeField] private GameObject SubScenePrefab;
    private TextMeshPro text;

    private void Start()
    {
        SetText();
        GenerateTheme();
    }

    private void SetText()
    {
        text = GetComponent<TextMeshPro>();
        text.text = nameTheme;
    }

    private void GenerateTheme()
    {
        for (int i = 0; i < allSubScene.Count; i++)
        {
            GameObject subScene = Instantiate(SubScenePrefab.gameObject, transform);
            subScene.transform.localPosition = Vector3.up * MenuManager.Instance.distSubSceneY * (i + 1) + Vector3.right * MenuManager.Instance.distSubSceneX;

            TextMeshPro text = subScene.GetComponent<TextMeshPro>();    
            text.text = allSubScene[i].namee;

            GameObject icone = Instantiate(allSubScene[i].icon, subScene.transform);
            icone.transform.localPosition = MenuManager.Instance.localPosIcon;
        }
    }

    public SubSceneScriptableObject GetFirstElement()
    {
        return allSubScene[0];
    }

    public int GetCountAllSubScene()
    {
        return allSubScene.Count;
    }
}
