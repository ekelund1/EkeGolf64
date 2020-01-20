using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTest : TerrainReader
{
    private Rigidbody _rb;

    public GameObject theHole;

    public Vector3 startPos;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        _rb = GetComponent<Rigidbody>();
        disable();
        GameStateManager.StartListening(GameState.BALL_IN_MOTION, enable);
        GameStateManager.StartListening(GameState.SETUP_SHOT, disable);
    }

    public void disable()
    {
        _rb.detectCollisions = false;
        _rb.useGravity = false;
        this._resetRotation();

    }
    public void enable()
    {
        _rb.detectCollisions = true;
        _rb.useGravity = true;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (_rb.velocity.magnitude < 0.2f && GameStateManager.activeEvent == GameState.BALL_IN_MOTION)
        {
            this._shotFinished();
        }
        if (transform.position.y < 0)
        {
            this._shotFinished();
            this._respawn();
        }
        // transform.position += new Vector3(-0f, 0, 0.1f);
    }

    private void _shotFinished()
    {
        _rb.Sleep();
        GameStateManager.TriggerEvent(GameState.SETUP_SHOT);
    }

    private float ImpactDecay()
    {
        switch (surfaceName)
        {
            case "fairway":
                return 0.97f;
            case "rough":
                return 0.6f;
            case "deep_rough":
                return 0.4f;
            default:
                return 0.99f;

        }
    }

    private float RollingDecay()
    {
        switch (surfaceName)
        {
            case "fairway":
                _rb.angularDrag = 0.8f;
                return 0.85f;
            case "rough":
                _rb.angularDrag = 0.9f;
                return 0.5f;
            case "deep_rough":
                _rb.angularDrag = 0.96f;
                return 0.3f;
            default:
                return 0.99f;

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.tag == "Course")
        {
            _rb.angularDrag = 0.8F;
            _rb.velocity *= this.ImpactDecay();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.gameObject.tag == "Course")
        {
            _rb.velocity *= this.RollingDecay();
        }
    }

    public static Vector3 calculateForce(ClubData club, float power)
    {
        float forceZ = power * club.powerLossRatio;

        if (club.constantPower > 0)
        {
            forceZ = club.constantPower;
        }
        float forceY = Mathf.Sin(club.angle / 90) * power * club.powerLossRatio;
        return new Vector3(0, forceY, forceZ);
    }

    public void ShootBall(float power, Clubs club, Quaternion angle)
    {
        this.startPos = transform.position;


        Debug.Log(angle.ToString());

        this.transform.rotation = angle;

        ClubData cb = ClubDictionary.getClubData(club);
        Debug.Log("Loaded club " + cb.ToString());


        Vector3 force = calculateForce(cb, power);

        _rb.AddRelativeForce(force, ForceMode.Impulse);

        GameStateManager.TriggerEvent(GameState.BALL_IN_MOTION);
        CameraEventManager.TriggerEvent(CameraState.BALL_CAMERA);
    }

    private void _resetRotation()
    {
        this.transform.LookAt(theHole.transform, Vector3.up);
        //this.transform.rotation = new Quaternion();
    }

    private void _respawn()
    {
        this.transform.position = startPos;
    }

    void OnGUI()
    {
        GUI.Box(new Rect(50, 80, 250, 25), "Magnitude: " + _rb.velocity.magnitude + ", surface: " + base.surfaceName.ToString());
    }

}
