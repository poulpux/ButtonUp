using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Slider : MonoBehaviour
{
    public float minY = 20f, maxY = 0f;
    [SerializeField] private float sliderSensi = 1f;
    [SerializeField] private GameObject ObjectToMove;
    private float intertie;
    float timer;

    private void Update()
    {
        intertie = Mathf.Lerp(intertie, 0, -Mathf.Abs(intertie)/2f);

        //Calculate futurPos
        Vector3 futurPos = ObjectToMove.transform.position - intertie * Vector3.up * Time.deltaTime * sliderSensi;
        ObjectToMove.transform.position = futurPos.y > minY ? ReturnWithYChange(minY) : futurPos.y < maxY ? ReturnWithYChange(maxY) : futurPos;
    }
    public void TryToSlide(Vector3 deltaPos)
    {
        intertie = deltaPos.y;
    }

    private Vector3 ReturnWithYChange(float y)
    {
        return new Vector3(ObjectToMove.transform.position.x, y, ObjectToMove.transform.position.z);
    }
}
