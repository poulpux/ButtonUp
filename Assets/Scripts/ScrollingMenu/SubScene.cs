using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(TextMeshPro))]
public class SubScene : MonoBehaviour, ICliquable
{
    public Collider2D colliderr { get => _collider; set => _collider = value; }  
    private Collider2D _collider;

    [HideInInspector] public TextMeshPro text;
    [HideInInspector] public SubSceneScriptableObject subSceneSO;

    void Awake()
    {
        _collider = GetComponent<Collider2D>();
        text = GetComponent<TextMeshPro>();
    }

    public void Activate()
    {
        print("activate");
        MenuManager.Instance.TrySwitchSubSceneEvent.Invoke(subSceneSO);
    }

    public void SetSO(SubSceneScriptableObject SO) =>
        subSceneSO = SO;
}
