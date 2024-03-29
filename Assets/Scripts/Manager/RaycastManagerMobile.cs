using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RaycastManagerMobile : MonoBehaviour
{
    [SerializeField]
    private Camera _camera;

    //Events


    [SerializeField]
    private GraphicRaycaster UIRaycaster;

    [SerializeField]
    EventSystem eventSystem;
    PointerEventData pointerEventData;

    private int _currentScene;
    private static RaycastManagerMobile instance;
    public static RaycastManagerMobile Instance { get { return instance; } }
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    void Start()
    {
        pointerEventData = new PointerEventData(eventSystem);
        InputManager.Instance.tap.AddListener((pos) =>
        {
            RaycastHit(pos);
        });

        InputManager.Instance.press.AddListener((touch) =>
        {
            RaycastHit(touch.currentPosition);
        });

    }

    //----------------------------------------------------------------------------------------------------

    private void RaycastHit(Vector3 pos)
    {
        if (_currentScene == 0 && GameUI(pos) == false)
            Game(pos);
    }

    private bool GameUI(Vector3 pos)
    {
        pointerEventData.position = pos;
        List<RaycastResult> results = new List<RaycastResult>();
        UIRaycaster.Raycast(pointerEventData, results);

        if (results.Count > 0)
        {
            //Your UnityEvent
            return true;
        }

        return false;
    }

    private void Game(Vector3 pos)
    {
        Ray ray = _camera.ScreenPointToRay(pos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            hit.collider?.GetComponent<ICliquable>()?.Activate();
        }
    }
}
