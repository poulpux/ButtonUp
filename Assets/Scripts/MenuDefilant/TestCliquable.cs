using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCliquable : MonoBehaviour, ICliquable
{
    [SerializeField] Collider2D _collider;
    public Collider2D colliderr { get => _collider; set => _collider = value; }

    public void Activate()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
