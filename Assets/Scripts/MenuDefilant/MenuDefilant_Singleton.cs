using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MenuDefilant_Singleton : MonoBehaviour
{
    [HideInInspector] public UnityEvent TryOpenEvent = new UnityEvent();
    [HideInInspector] public UnityEvent TryCloseEvent = new UnityEvent();
    [HideInInspector] public UnityEvent OnOpenEvent = new UnityEvent();
    [HideInInspector] public UnityEvent OnCloseEvent = new UnityEvent();

    [SerializeField] private float closePosX, openPosX;
    [SerializeField] private SpriteRenderer cacheNoir;

    private bool isNotInteractible, open;
    private float timeStateChange;

    private static MenuDefilant_Singleton instance;
    public static MenuDefilant_Singleton Instance { get { return instance; } }
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            Debug.Log("There is two MenuDefilant_Singleton");
        }
        else
            instance = this;
    }

    void Start()
    {
        TryCloseEvent.AddListener(() => StartCoroutine(TryInteract(false)));
        TryOpenEvent.AddListener(() => StartCoroutine(TryInteract(true)));
    }

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    public IEnumerator TryInteract(bool stateWanted)
    {
        if(isNotInteractible || open == stateWanted)
            yield break;
        else
        {
            InitCoroutine();

            while (timeStateChange <= 0.18f)
            {
                Transition();
                yield return null;
            }

            EndCoroutine();
            yield break;
        }
    }
    
    private void InitCoroutine()
    {
        isNotInteractible = true;
        timeStateChange = 0f;
    }

    private void EndCoroutine()
    {
        isNotInteractible = false;
        open = !open;
        if(open)
            OnOpenEvent.Invoke();
        else
            OnCloseEvent.Invoke();
    }

    private void Transition()
    {
        timeStateChange += Time.deltaTime;

        float t = Mathf.Clamp01(timeStateChange / 0.18f);
        float lerpedValue = Mathf.Lerp(!open? closePosX : openPosX, !open ? openPosX : closePosX, t);
        transform.position = new Vector3(lerpedValue, transform.position.y, transform.position.z);
        cacheNoir.material.color = new Color(cacheNoir.material.color.r, cacheNoir.material.color.g, cacheNoir.material.color.b,!open ? t * 0.8f : 0.8f- t * 0.8f);
    }
}
