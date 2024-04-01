using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(TextMeshPro))]
public class SubScene : MonoBehaviour, ICliquable
{
    [SerializeField] private GameObject icone;

    public Collider2D colliderr { get => _collider; set => _collider = value; }
    public string nameSubScene = "default";
    

    private Collider2D _collider;
    private TextMeshPro text;
    void Awake()
    {
        text = GetComponent<TextMeshPro>(); 
        _collider = GetComponent<Collider2D>(); 

        text.text = nameSubScene;
        GameObject icon = Instantiate(icone, transform);
        icon.transform.localPosition = new Vector3(-12.15f, 1.8f, 0f);
        icon.transform.localScale = Vector3.one* 5f;
    }

    public void Activate()
    {
        print("activate");
        ScrollingMenu_Singleton.Instance.TrySwitchSubSceneEvent.Invoke(this);
    }
}
