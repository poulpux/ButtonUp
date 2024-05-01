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
        //timer += Time.deltaTime;
        //float t = Mathf.Clamp01(timer / 20f);
        //intertie = Mathf.Lerp(intertie, 0, t);

        ////Calculate futurPos
        //Vector3 futurPos = ObjectToMove.transform.position - intertie * Vector3.up * Time.deltaTime * sliderSensi;
        //ObjectToMove.transform.position = futurPos.y > minY ? ReturnWithYChange(minY) : futurPos.y < maxY ? ReturnWithYChange(maxY) : futurPos;
    }
    public void EndedPress(Vector3 deltaPos)
    {
        StartCoroutine(PlayInertia(deltaPos.y));
    }

    public void MoveSlider(Vector3 deltaPos)
    {
        StopCoroutine(PlayInertia(deltaPos.y));
        PlayMovement(deltaPos.y);
    }

    private void PlayMovement(float deltaPos)
    {
        Vector3 futurPos = ObjectToMove.transform.position - deltaPos * Vector3.up * Time.deltaTime * sliderSensi;
        ObjectToMove.transform.position = futurPos.y > minY ? ReturnWithYChange(minY) : futurPos.y < maxY ? ReturnWithYChange(maxY) : futurPos;
    }

    private Vector3 ReturnWithYChange(float y)
    {
        return new Vector3(ObjectToMove.transform.position.x, y, ObjectToMove.transform.position.z);
    }

    private IEnumerator PlayInertia(float deltaPos)
    {
        float inertia = deltaPos;
        while (inertia > 0f)
        {
            inertia -= Time.deltaTime;
            PlayMovement(inertia);
            yield return null;
        }
    }
}
