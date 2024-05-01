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

    void Awake()
    {
        _collider = GetComponent<Collider2D>();
        text = GetComponent<TextMeshPro>();
        //GameObject icon = Instantiate(icone, transform);
        //icon.transform.localPosition = new Vector3(-12.15f, 1.8f, 0f);
        //icon.transform.localScale = Vector3.one* 5f;
    }

    public void Activate()
    {
        print("activate");
        MenuManager.Instance.TrySwitchSubSceneEvent.Invoke(this);
    }
}
