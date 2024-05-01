using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.FullSerializer;
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
            GameObject subScene = GenerateSubSceneObj(i);
            SetText(allSubScene[i],ref subScene);
            GenerateIcon(allSubScene[i], ref subScene);
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

    private GameObject GenerateSubSceneObj(int i)
    {
        GameObject subScene = Instantiate(SubScenePrefab.gameObject, transform);
        subScene.transform.localPosition = Vector3.up * MenuManager.Instance.distSubSceneY * (i + 1) + Vector3.right * MenuManager.Instance.distSubSceneX;
        subScene.GetComponent<SubScene>().subSceneSO = allSubScene[i];

        TextMeshPro text = subScene.GetComponent<TextMeshPro>();
        text.text = allSubScene[i].namee;

        GameObject icone = Instantiate(allSubScene[i].icon, subScene.transform);
        icone.transform.localPosition = MenuManager.Instance.localPosIcon;

        return subScene;
    }

    private void SetText(SubSceneScriptableObject SO, ref GameObject subScene)
    {
        TextMeshPro text = subScene.GetComponent<TextMeshPro>();
        text.text = SO.namee;
    }

    private void GenerateIcon(SubSceneScriptableObject SO, ref GameObject subScene)
    {
        GameObject icone = Instantiate(SO.icon, subScene.transform);
        icone.transform.localPosition = MenuManager.Instance.localPosIcon;
    }
}
