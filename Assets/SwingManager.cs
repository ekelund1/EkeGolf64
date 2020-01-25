using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingManager : MonoBehaviour
{
    public float swingTimer = 0f;
    public float currentPower = 0f;
    public float currentAccuracy = 0f;
    public bool active = false;
    public bool pickedPower = false;

    void Start()
    {
        GameStateManager.StartListening(GameState.BALL_IN_MOTION, () =>
        {
            ResetSwing();
        });
    }

    void Update()
    {
        if (active)
            DoingSwing();
    }

    public SwingStats ButtonPressed()
    {
        if (!active)
        {
            StartSwing();
            return new SwingStats();
        }
        else if (!pickedPower)
        {
            pickedPower = true;
            return new SwingStats();
        }
        else
        {
            SwingFinished();
            var swing = new SwingStats();
            swing.accuracy = currentAccuracy;
            swing.powerModifier = currentPower;
            return swing;
        }
    }
    private void StartSwing()
    {
        currentPower = 0;
        swingTimer = 0;
        active = true;
    }
    private void DoingSwing()
    {
        swingTimer += Time.deltaTime;

        if (swingTimer < 2.5f && !pickedPower)
        {
            if (swingTimer <= 1f)
                currentPower += Time.deltaTime;
            else
                currentPower -= Time.deltaTime;
        }
        else if (pickedPower && swingTimer < 2.5f)
        {
            currentAccuracy = Mathf.Clamp(currentAccuracy + Time.deltaTime, 0.5f, 1.5f);
        }
        else if (swingTimer > 2.5f)
        {
            ResetSwing();
        }
    }
    private void ResetSwing()
    {
        currentAccuracy = 0;
        currentPower = 0;
        swingTimer = 0;
        active = false;
        pickedPower = false;
    }

    private void SwingFinished()
    {
        active = false;
    }
}

public class SwingStats
{
    public float powerModifier { get; set; } = -1;
    public float accuracy { get; set; } = -1;
}