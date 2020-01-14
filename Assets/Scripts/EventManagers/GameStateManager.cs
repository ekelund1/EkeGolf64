using System.Collections.Generic;
using System;
using UnityEngine;


public enum GameState
{
    BALL_IN_MOTION,
    SETUP_SHOT,
    SHOT_FINISHED,
}


public class GameStateManager : MonoBehaviour
{
    private Dictionary<GameState, Action> eventDictionary;

    private static GameState _activeGameState;
    private static GameStateManager eventManager;

    public static GameStateManager instance
    {
        get
        {
            if (!eventManager)
            {
                eventManager = FindObjectOfType(typeof(GameStateManager)) as GameStateManager;

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
        GUI.Box(new Rect(10, 10, 250, 25), "GameState: " + _activeGameState);
    }

    public static GameState activeEvent
    {
        get { return _activeGameState; }
        set { _activeGameState = value; }
    }

    void Init()
    {
        if (eventDictionary == null)
        {
            _activeGameState = GameState.SETUP_SHOT;
            eventDictionary = new Dictionary<GameState, Action>();
        }
    }

    public static void StartListening(GameState eventName, Action listener)
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

    public static void StopListening(GameState eventName, Action listener)
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

    public static void TriggerEvent(GameState eventName)
    {
        Action thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke();
            //instance.eventDictionary[eventName]();
            _activeGameState = eventName;

        }
    }
}