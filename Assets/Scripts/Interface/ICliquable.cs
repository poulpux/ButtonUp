using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICliquable
{
    Collider2D colliderr { get; set; }
    void Activate();
}
