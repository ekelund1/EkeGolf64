﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CameraTags
{
    BallCamera,
    AlignShotCamera,
    HoleCamera,
}
public class CameraManager : MonoBehaviour
{
    public Camera[] cameras;
    private string activeCamera;

    void Start()
    {
        switchCamera("AlignShotCamera");

        GameStateManager.StartListening(GameState.BALL_IN_MOTION, () =>
        {
            switchCamera("BallCamera");
        });
        GameStateManager.StartListening(GameState.SETUP_SHOT, () =>
        {
            switchCamera("AlignShotCamera");
        });
    }

    private void switchCamera(string tag)
    {
        foreach (Camera camera in cameras)
        {
            if (camera.gameObject.tag == tag)
            {
                camera.gameObject.SetActive(true);
                activeCamera = tag;
            }
            else
                camera.gameObject.SetActive(false);
        }
    }

    void OnDisable()
    {
        GameStateManager.StopListening(GameState.BALL_IN_MOTION, () =>
        {
            switchCamera("BallCamera");
        });
        GameStateManager.StopListening(GameState.SETUP_SHOT, () =>
        {
            switchCamera("AlignShotCamera");
        });
    }
}
