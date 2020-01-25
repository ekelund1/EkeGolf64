using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleScript : MonoBehaviour
{
    public BallTest theBall;
    private float maxTimer = 0.5f;
    private float timer = 0f;
    void Update()
    {
        if (GameStateManager.activeEvent == GameState.BALL_IN_MOTION)
        {
            if (Vector3.Distance(theBall.transform.position, transform.position) < 12f)
            {
                CameraEventManager.TriggerEvent(CameraState.HOLE_CAMERA);
            }
            else if (CameraEventManager.activeEvent == CameraState.HOLE_CAMERA)
            {
                CameraEventManager.TriggerEvent(CameraState.BALL_CAMERA);
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "TheBall")
        {
            timer = 0f;
        }
    }

    void OnCollisionStay(Collision other)
    {
        if (other.gameObject.name == "TheBall")
        {
            timer += Time.deltaTime;
            if (timer > maxTimer && GameStateManager.activeEvent == GameState.BALL_IN_MOTION)
            {
                timer = 0f;
                GameStateManager.TriggerEvent(GameState.BALL_IN_HOLE);
                Debug.Log("Player won");
            }
        }
    }
    private void OnGUI()
    {
        GUI.Box(new Rect(900, 30, 250, 25), "Distance to hole: " + Vector3.Distance(theBall.transform.position, transform.position));
    }
}
