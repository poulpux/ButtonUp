using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.Events;

public class MenuManager : MonoBehaviour
{
    [HideInInspector] public UnityEvent TryOpenEvent = new UnityEvent();
    [HideInInspector] public UnityEvent TryCloseEvent = new UnityEvent();
    [HideInInspector] public UnityEvent OnOpenEvent = new UnityEvent();
    [HideInInspector] public UnityEvent OnCloseEvent = new UnityEvent();
    [HideInInspector] public UnityEvent<SubSceneScriptableObject> TrySwitchSubSceneEvent = new UnityEvent<SubSceneScriptableObject>();
    [Header("======Content======")]
    [Space(10)]
    [SerializeField] private List<Theme> themeList;

    [Header("======All Values======")]
    [Space(10)]

    [SerializeField] private float closePosX, openPosX;
    [SerializeField] private Vector3 offSet;
    public float distSubSceneY, distSubSceneX, distTheme;
    public Vector3 localPosIcon;

    [Header("======Visuel======")]
    [Space(10)]
    [SerializeField] private Slider slider;
    [SerializeField] private GameObject background;
    [SerializeField] private SpriteRenderer cacheNoir;

    private AnimatingCurve curve = new AnimatingCurve(0f,0f, 0.2f, GRAPH.EASESIN, INANDOUT.IN, LOOP.CLAMP) ;
    private bool isNotInteractible, open;
    private float timeStateChange;


    private SubSceneScriptableObject currentSubScene;

    private static MenuManager instance;
    public static MenuManager Instance { get { return instance; } }
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

        currentSubScene = themeList[0].GetFirstElement();

        InstantiateAllTheme();
    }

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    private void InstantiateAllTheme()
    {
        float maxLenght = 0f;
        SpawnTheme(ref maxLenght);
        RezizeScrollingMenu(ref maxLenght);

        themeList.Clear();
    }

    private void SwitchSubScene(SubSceneScriptableObject subSceneScriptableObject)
    {

    }

    private void Transition()
    {
        timeStateChange += Time.deltaTime;

        float t = Mathf.Clamp01(timeStateChange / 0.18f);
        float lerpedValue = transform.position.x;
        Tools.PlayCurve(ref curve, ref lerpedValue);
        transform.position = new Vector3(lerpedValue, transform.position.y, transform.position.z);
        cacheNoir.material.color = new Color(cacheNoir.material.color.r, cacheNoir.material.color.g, cacheNoir.material.color.b, !open ? curve.timeSinceBegin / curve.duration * 0.8f : 0.8f - curve.timeSinceBegin / curve.duration * 0.8f);
    }

    private void RezizeScrollingMenu(ref float maxLenght)
    {
        maxLenght += themeList[themeList.Count - 1].GetCountAllSubScene() * distSubSceneY / 5f;

        if (maxLenght < -4f)
        {
            float diff = -maxLenght - 4f;
            background.transform.localScale = new Vector3(background.transform.localScale.x, background.transform.localScale.y + diff, background.transform.localScale.z);
            background.transform.localPosition -= Vector3.up * diff / 2f;
            slider.minY = diff;
        }
        else
            slider.minY = 0f;
    }

    private void SpawnTheme(ref float maxLenght)
    {
        int weight = 0;
        for (int i = 0; i < themeList.Count; i++)
        {
            Vector3 distTheme = Vector3.up * i * this.distTheme;
            Vector3 distSubScene = weight * distSubSceneY / 5f * Vector3.up; // Normaly it's only distSubScene but I dont know why i need to put /5f
            GameObject theme = Instantiate(themeList[i].gameObject, transform);
            theme.transform.localPosition = distTheme + distSubScene + offSet;
            maxLenght = theme.transform.localPosition.y;
            weight += themeList[i].GetCountAllSubScene();
        }
    }


    //====================================================
    //==========================
    //===========

    public IEnumerator TryInteract(bool stateWanted)
    {
        if (isNotInteractible || open == stateWanted)
            yield break;
        else
        {
            InitCoroutine();

            while (!Tools.isCurveFinish(curve))
            {
                Transition();
                yield return new WaitForEndOfFrame();
            }

            EndCoroutine();
            yield break;
        }
    }

    private void InitCoroutine()
    {
        isNotInteractible = true;
        timeStateChange = 0f;
        curve.timeSinceBegin = 0;
        curve.beginValueF = transform.position.x;
        curve.endValueF = !open ? openPosX : closePosX;

    }

    private void EndCoroutine()
    {
        isNotInteractible = false;
        open = !open;
        if (open)
            OnOpenEvent.Invoke();
        else
            OnCloseEvent.Invoke();
    }

    //===========
    //==========================
    //====================================================
}
