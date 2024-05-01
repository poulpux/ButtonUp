using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider2D))]
public class LeaveMenuDefilant : MonoBehaviour, ICliquable
{
    Collider2D _collider;
    public Collider2D colliderr { get => _collider; set => _collider = value; }

    private void Start()
    {
        _collider = GetComponent<BoxCollider2D>();
        Close();
        MenuManager.Instance.OnCloseEvent.AddListener(() => Close());
        MenuManager.Instance.OnOpenEvent.AddListener(() => Open());
    }

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    public void Activate()
    {
        MenuManager.Instance.TryCloseEvent.Invoke();
    }

    private void Close()
    {
        colliderr.enabled = false;
    }

    private void Open()
    {
        colliderr.enabled = true;
    }
}
