using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCamera : MonoBehaviour
{
    public HoleScript theHole;
    private float timer = 0f;
    private float maxTimer = 4f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (this.isActiveAndEnabled)
        {
            transform.LookAt(theHole.transform.position);
            transform.Translate(Vector3.right * Time.deltaTime * 3);
            timer += Time.deltaTime;

            if (timer > maxTimer)
            {
                GameStateManager.TriggerEvent(GameState.SETUP_SHOT);
            }
        }
    }

    void OnEnable()
    {
        timer = 0;
    }

}
