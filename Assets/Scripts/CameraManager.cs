using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CameraTags
{
    BallCamera,
    AlignShotCamera
}
public class CameraManager : MonoBehaviour
{
    public Camera[] cameras;
    private string activeCamera;

    void Start()
    {
        EventManager.StartListening(GameState.BALL_IN_MOTION, () =>
        {
            switchCamera("BallCamera");
        });
        EventManager.StartListening(GameState.SETUP_SHOT, () =>
        {
            switchCamera("AlignShotCamera");
        });
        switchCamera("AlignShotCamera");
    }

    private void OnGUI()
    {
        GUI.Box(new Rect(10, 40, 250, 25), "Camera: " + activeCamera);
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
        EventManager.StopListening(GameState.BALL_IN_MOTION, () =>
        {
            switchCamera("BallCamera");
        });
        EventManager.StopListening(GameState.SETUP_SHOT, () =>
        {
            switchCamera("AlignShotCamera");
        });
    }
}
