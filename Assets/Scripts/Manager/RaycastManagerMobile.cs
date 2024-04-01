using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;

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
            RaycastHitTap(pos);
        });

        InputManager.Instance.press.AddListener((touch) =>
        {
            RaycastHitHold(touch.currentPosition, touch.deltaPosition);
        });
        
        InputManager.Instance.fingerUp.AddListener((touch) =>
        {
            RaycastHitEnd(touch.currentPosition);
        });

    }

    //----------------------------------------------------------------------------------------------------

    private void RaycastHitTap(Vector3 pos)
    {
        if (_currentScene == 0 && GameUI(pos) == false)
            Game(pos);
    }
    private void RaycastHitHold(Vector3 pos, Vector3 deltaPos)
    {
         GameSlide(pos, deltaPos);
    }
    private void RaycastHitEnd(Vector3 pos)
    {
        //if (_currentScene == 0 && GameUI(pos) == false)
        //    Game(pos);
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
        RaycastHit2D hit = shootRaycast2DTouch(pos);

        hit.collider?.GetComponent<ICliquable>()?.Activate();
    }

    private void GameSlide(Vector3 pos, Vector3 deltaPos)
    {
        RaycastHit2D hit = shootRaycast2DHold(pos);

        hit.collider?.GetComponent<Slider>().TryToSlide(deltaPos);
    }

    private RaycastHit2D shootRaycast2DTouch(Vector3 pos)
    {
        Ray ray = _camera.ScreenPointToRay(pos);
        return Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, (1 << LayerMask.NameToLayer("Touch")));
    }
    
    private RaycastHit2D shootRaycast2DHold(Vector3 pos)
    {
        Ray ray = _camera.ScreenPointToRay(pos);
        return Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, (1 << LayerMask.NameToLayer("Hold")));
    }
}
