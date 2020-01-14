using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimIndicator : MonoBehaviour
{
    void OnEnable()
    {
        GameStateManager.StartListening(GameState.BALL_IN_MOTION, _disable);
        GameStateManager.StartListening(GameState.SETUP_SHOT, _enable);
    }

    void OnDisable()
    {
        // EventManager.StopListening(GameState.BALL_IN_MOTION, _disable);
        // EventManager.StopListening(GameState.SETUP_SHOT, _enable);
    }

    private void _disable()
    {
        Debug.Log("Disabled AimIndicator");
        this.gameObject.SetActive(false);
    }
    private void _enable()
    {
        Debug.Log("Enabled AimIndicator!");
        this.gameObject.SetActive(true);
    }


}
