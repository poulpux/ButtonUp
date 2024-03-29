using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MenuDefilant_Singleton : MonoBehaviour
{
    [HideInInspector] public UnityEvent TryOpenEvent = new UnityEvent();
    [HideInInspector] public UnityEvent TryCloseEvent = new UnityEvent();
    public static event Action OnOpen, OnClose;


    private bool isNotInteractible, open;
    private float timeStateChange;
    [SerializeField] private float closePosX, openPosX;
    [SerializeField] private SpriteRenderer cacheNoir;


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
        TryCloseEvent.AddListener(() => StartCoroutine(TryClose()));
        TryOpenEvent.AddListener(() => StartCoroutine(TryOpen()));
    }

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    public IEnumerator TryClose()
    {
        if(isNotInteractible || !open)
            yield break;
        else
        {
            isNotInteractible = true;
            timeStateChange = 0f;

            while (timeStateChange <= 0.25f)
            {
                MoveMenu();
                yield return null;
            }

            isNotInteractible = false;
            open = false;
            OnClose?.Invoke();
            yield break;
        }
    }
    public IEnumerator TryOpen()
    {
        if(isNotInteractible || open)
            yield break;
        else
        {
            isNotInteractible = true;
            timeStateChange = 0f;

            while (timeStateChange <= 0.25f)
            {
                MoveMenu();
                yield return null;
            }

            isNotInteractible = false;
            open = true;
            OnOpen?.Invoke();
            yield break;
        }
    }

    private void MoveMenu()
    {
        timeStateChange += Time.deltaTime;

        float t = Mathf.Clamp01(timeStateChange / 0.25f);
        float lerpedValue = Mathf.Lerp(!open? closePosX : openPosX, !open ? openPosX : closePosX, t);
        transform.position = new Vector3(lerpedValue, transform.position.y, transform.position.z);
        cacheNoir.material.color = new Color(cacheNoir.material.color.r, cacheNoir.material.color.g, cacheNoir.material.color.b,!open ? t * 0.8f : 0.8f- t * 0.8f);
    }
}
