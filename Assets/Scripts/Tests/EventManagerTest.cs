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
            GameStateManager.TriggerEvent(GameState.BALL_IN_MOTION);
            yield return waitTime;
            GameStateManager.TriggerEvent(GameState.SETUP_SHOT);
            yield return waitTime;
            GameStateManager.TriggerEvent(GameState.SHOT_FINISHED);
        }
    }

    void OnEnable()
    {
        GameStateManager.StartListening(GameState.BALL_IN_MOTION, someListener);
        GameStateManager.StartListening(GameState.SETUP_SHOT, SomeOtherFunction);
        GameStateManager.StartListening(GameState.SHOT_FINISHED, SomeThirdFunction);
    }

    void OnDisable()
    {
        GameStateManager.StopListening(GameState.BALL_IN_MOTION, someListener);
        GameStateManager.StopListening(GameState.SETUP_SHOT, SomeOtherFunction);
        GameStateManager.StopListening(GameState.SHOT_FINISHED, SomeThirdFunction);
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