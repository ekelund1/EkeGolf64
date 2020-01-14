using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManagerTest : MonoBehaviour
{
    private Action someListener;

    void Awake()
    {
        someListener = new Action(SomeFunction);
        StartCoroutine(invokeTest());
    }

    IEnumerator invokeTest()
    {
        WaitForSeconds waitTime = new WaitForSeconds(2);
        while (true)
        {
            yield return waitTime;
            EventManager.TriggerEvent(GameState.BALL_IN_MOTION);
            yield return waitTime;
            EventManager.TriggerEvent(GameState.SETUP_SHOT);
            yield return waitTime;
            EventManager.TriggerEvent(GameState.SHOT_FINISHED);
        }
    }

    void OnEnable()
    {
        EventManager.StartListening(GameState.BALL_IN_MOTION, someListener);
        EventManager.StartListening(GameState.SETUP_SHOT, SomeOtherFunction);
        EventManager.StartListening(GameState.SHOT_FINISHED, SomeThirdFunction);
    }

    void OnDisable()
    {
        EventManager.StopListening(GameState.BALL_IN_MOTION, someListener);
        EventManager.StopListening(GameState.SETUP_SHOT, SomeOtherFunction);
        EventManager.StopListening(GameState.SHOT_FINISHED, SomeThirdFunction);
    }

    void SomeFunction()
    {
        Debug.Log("Some Function was called!");
    }

    void SomeOtherFunction()
    {
        Debug.Log("Some Other Function was called!");
    }

    void SomeThirdFunction()
    {
        Debug.Log("Some Third Function was called!");
    }
}