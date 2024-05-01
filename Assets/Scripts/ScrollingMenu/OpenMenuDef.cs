using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(BoxCollider2D))]
public class OpenMenuDef : MonoBehaviour, ICliquable
{
    Collider2D _collider;
    public Collider2D colliderr { get => _collider; set => _collider = value; }
    void Start()
    {
        _collider = GetComponent<BoxCollider2D>();
    }

    public void Activate()
    {
        MenuManager.Instance.TryOpenEvent.Invoke();
    }
}
