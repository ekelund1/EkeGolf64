using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtHoleCamera : MonoBehaviour
{
    private float timer = 0f;
    private float maxTimer = 2.5f;

    private GameObject target;
    private Quaternion targetRotation;

    void Update()
    {
        if (enabled)
        {
            Vector3 targetPoint = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z) - transform.position;
            targetRotation = Quaternion.LookRotation(targetPoint, Vector3.up);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 1.0f);

            timer += Time.deltaTime;

            if (timer > maxTimer)
            {
                GameStateManager.TriggerEvent(GameState.SETUP_SHOT);
            }
        }
    }
    void Start()
    {
        target = GameObject.FindWithTag("Player");
    }
    void OnEnable()
    {
        target = GameObject.FindWithTag("TheHole");

        transform.LookAt(target.transform.position, Vector3.up);
        timer = 0;
    }
}
