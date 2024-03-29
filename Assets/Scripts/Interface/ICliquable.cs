using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICliquable
{
    [SerializeField] Collider2D collider { get; set; }
    public void Activate();
}
