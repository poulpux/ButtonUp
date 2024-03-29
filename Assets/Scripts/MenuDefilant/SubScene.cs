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


[RequireComponent(typeof(BoxCollider2D))]
public class SubScene : MonoBehaviour, ICliquable
{
    Collider2D _collider;
    public Collider2D colliderr { get => _collider; set => _collider = value; }

    public string nameSubScene = "default";
    public GameObject content, image;
    void Start()
    {
        _collider = GetComponent<BoxCollider2D>();
    }

    public void Activate()
    {
        MenuDefilant_Singleton.Instance.SwitchSubSceneEvent.Invoke(this);
    }
}
