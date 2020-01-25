using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum CameraState
{
    HOLE_CAMERA,
    FREE_CAMERA,
    BALL_CAMERA,
    ALIGN_SHOT_CAMERA,
    WIN_CAMERA,
    LOOK_AT_HOLE_CAMERA,
}
public class CameraEventManager : MonoBehaviour
{
    private Dictionary<CameraState, Action> eventDictionary;

    private static CameraState _activeCameraState;
    private static CameraEventManager eventManager;

    public static CameraEventManager instance
    {
        get
        {
            if (!eventManager)
            {
                eventManager = FindObjectOfType(typeof(CameraEventManager)) as CameraEventManager;

                if (!eventManager)
                {
                    Debug.LogError("There needs to be one active EventManger script on a GameObject in your scene.");
                }
                else
                {
                    eventManager.Init();
                }
            }

            return eventManager;
        }
    }

    private void OnGUI()
    {
        GUI.Box(new Rect(900, 10, 250, 25), "CameraState: " + _activeCameraState);
    }

    public static CameraState activeEvent
    {
        get { return _activeCameraState; }
        set { _activeCameraState = value; }
    }

    void Init()
    {
        if (eventDictionary == null)
        {
            _activeCameraState = CameraState.ALIGN_SHOT_CAMERA;
            eventDictionary = new Dictionary<CameraState, Action>();
        }
    }

    public static void StartListening(CameraState eventName, Action listener)
    {
        Action thisEvent;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            //Add more event to the existing one
            thisEvent += listener;

            //Update the Dictionary
            instance.eventDictionary[eventName] = thisEvent;
        }
        else
        {
            //Add event to the Dictionary for the first time
            thisEvent += listener;
            instance.eventDictionary.Add(eventName, thisEvent);
        }
    }

    public static void StopListening(CameraState eventName, Action listener)
    {
        if (eventManager == null) return;
        Action thisEvent;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            //Remove event from the existing one
            thisEvent -= listener;
            //Update the Dictionary
            instance.eventDictionary[eventName] = thisEvent;
        }
    }

    public static void TriggerEvent(CameraState eventName, bool isGameState = false)
    {
        Action thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke();
            //instance.eventDictionary[eventName]();
            _activeCameraState = eventName;

        }
    }
}
