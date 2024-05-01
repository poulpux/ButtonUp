using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSubScene", menuName = "ScriptableObjects/SubScene")]
public class SubSceneScriptableObject : ScriptableObject
{
    public string namee;
    public GameObject icon;
    public GameObject content;
    public BoxCollider2D scrollLimit;
    public SubSceneScriptableObject parent;
}
