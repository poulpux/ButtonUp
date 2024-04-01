using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScrollingMenu_Singleton : MonoBehaviour
{
    [HideInInspector] public UnityEvent TryOpenEvent = new UnityEvent();
    [HideInInspector] public UnityEvent TryCloseEvent = new UnityEvent();
    [HideInInspector] public UnityEvent OnOpenEvent = new UnityEvent();
    [HideInInspector] public UnityEvent OnCloseEvent = new UnityEvent();
    [HideInInspector] public UnityEvent<SubScene> TrySwitchSubSceneEvent = new UnityEvent<SubScene>();

    [SerializeField] private float closePosX, openPosX;
    [SerializeField] private SpriteRenderer cacheNoir;
    [SerializeField] private List<Theme> themeList;
    [SerializeField] private Vector3 offSet;
    [SerializeField] private Slider slider;
    [SerializeField] private GameObject background;


    public float distSubSceneY, distSubSceneX, distTheme;

    private bool isNotInteractible, open;
    private float timeStateChange;
    private SubScene currentSubScene;
    private static ScrollingMenu_Singleton instance;
    public static ScrollingMenu_Singleton Instance { get { return instance; } }
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
        TrySwitchSubSceneEvent.AddListener((subScene) => SwitchSubScene(subScene));

        currentSubScene = themeList[0].allSubScene[0];

        InstantiateAllTheme();
    }

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    private void InstantiateAllTheme()
    {
        float maxLenght = 0f;
        int weight = 0;
        for (int i = 0; i < themeList.Count; i++)
        {
            Vector3 distTheme = Vector3.up * i * this.distTheme;
            Vector3 distSubScene = weight * distSubSceneY / 5f * Vector3.up; // Normaly it's only distSubScene but I dont know why i need to put /5f
            GameObject theme = Instantiate(themeList[i].gameObject, transform);
            theme.transform.localPosition = distTheme + distSubScene + offSet;
            maxLenght = theme.transform.localPosition.y;
            weight+= themeList[i].allSubScene.Count;
        }

        maxLenght += themeList[themeList.Count - 1].allSubScene.Count * distSubSceneY / 5f;
        print(maxLenght);
        if(maxLenght < -4f)
        {
            float diff = -maxLenght + 4f;
            background.transform.localScale = new Vector3(background.transform.localScale.x, background.transform.localScale.y + diff, background.transform.localScale.z);
            background.transform.localPosition -= Vector3.up * diff / 2f;
            slider.minY = diff / 5f;
        }
        else
            slider.minY = 0f;
        //background.transform.localScale =
        //slider = weight
    }

    private void SwitchSubScene(SubScene subScene)
    {

    }

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
