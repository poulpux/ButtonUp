using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Structure pour stocker les informations relatives au toucher
public class TouchPress
{
    public Touch touch; // Les données du toucher
    public Vector3 startPosition; // La position de départ du toucher
    public Vector3 currentPosition; // La position actuelle du toucher
    public Vector3 lastPosition; // La position actuelle du toucher
    public Vector3 deltaPosition;
}

public class InputManager : MonoBehaviour
{
    // Événements pour les interactions tactiles
    [HideInInspector] public UnityEvent<Vector3> tap = new UnityEvent<Vector3>(); // Pour le tap simple
    [HideInInspector] public UnityEvent<TouchPress> press = new UnityEvent<TouchPress>(); // Quand le doigt est maintenu
    [HideInInspector] public UnityEvent<TouchPress> fingerUp = new UnityEvent<TouchPress>(); // Quand le doigt quitte l'écran

    List<TouchPress> savedTouches = new List<TouchPress>(); // Liste pour stocker les données des touchers

    private static InputManager instance; // Instance unique de l'InputManager
    public static InputManager Instance { get { return instance; } }

    private void Awake()
    {
        // Gestion de l'instance unique
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    void Update()
    {
        TouchHandling(); // Gestion des touches
    }

    private void TouchHandling()
    {
        if (Input.touchCount > 0)
        {
            foreach (var touch in Input.touches)
            {
                TouchBegin(touch);
                TouchHold(touch);
                TouchEnd(touch);
            }
        }
    }

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    private void TouchBegin(Touch touch)
    {
        if (touch.phase == TouchPhase.Began)
        {
            // Initialisation des données pour un nouveau toucher
            TouchPress newTouch = new TouchPress();
            newTouch.startPosition = touch.position;
            newTouch.touch = touch;
            newTouch.currentPosition = touch.position;
            savedTouches.Add(newTouch);

            // Événement de tap
            tap.Invoke((Vector3)touch.position);
        }
    }

    private void TouchHold(Touch touch)
    {
        if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
        {
            // Mise à jour des données pour les touchers en mouvement ou statiques
            foreach (var savedTouch in savedTouches)
            {
                if (touch.fingerId == savedTouch.touch.fingerId)
                {
                    savedTouch.lastPosition = savedTouch.currentPosition;
                    savedTouch.currentPosition = touch.position;
                    savedTouch.deltaPosition = savedTouch.lastPosition - savedTouch.currentPosition;
                    // Événement de pression
                    press.Invoke(savedTouch);
                }
            }
        }
    }

    private void TouchEnd(Touch touch)
    {
        if (touch.phase == TouchPhase.Ended)
        {
            TouchPress touchToRemove = new TouchPress();

            // Recherche et enlève le toucher qui a été retiré de l'écran
            foreach (var savedTouch in savedTouches)
            {
                if (touch.fingerId == savedTouch.touch.fingerId)
                {
                    touchToRemove = savedTouch;

                    // Événement pour le relâchement du doigt
                    fingerUp.Invoke(savedTouch);
                }
            }

            savedTouches.Remove(touchToRemove); // Retire le toucher de la liste
        }
    }
}