using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Slider : MonoBehaviour
{
    public float minY = 20f, maxY = 0f;
    [SerializeField] private float sliderSensi = 1f;
    [SerializeField] private GameObject ObjectToMove;
    public void TryToSlide(Vector3 deltaPos)
    {
        Vector3 futurPos = ObjectToMove.transform.position - deltaPos.y * Vector3.up * Time.deltaTime * sliderSensi;
        ObjectToMove.transform.position = futurPos.y > minY ? ReturnWithYChange(minY) : futurPos.y < maxY ? ReturnWithYChange(maxY) : futurPos;
    }

    private Vector3 ReturnWithYChange(float y)
    {
        return new Vector3(ObjectToMove.transform.position.x, y, ObjectToMove.transform.position.z);
    }
}
