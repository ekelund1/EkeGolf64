using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTest : TerrainReader
{
    private Rigidbody _rb;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        _rb = GetComponent<Rigidbody>();
        //_rb.velocity = new Vector3(10, 30, 50);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (_rb.velocity.magnitude < 0.2f && EventManager.activeEvent == GameState.BALL_IN_MOTION)
        {
            this._shotFinished();
        }
        // transform.position += new Vector3(-0f, 0, 0.1f);
    }

    private void _shotFinished()
    {
        _rb.Sleep();
        EventManager.TriggerEvent(GameState.SETUP_SHOT);
    }

    private float ImpactDecay()
    {
        switch (surfaceName)
        {
            case "fairway":
                return 0.97f;
            case "rough":
                return 0.6f;
            default:
                return 0.99f;

        }
    }

    private float RollingDecay()
    {
        switch (surfaceName)
        {
            case "fairway":
                _rb.angularDrag = 0.6f;
                return 0.95f;
            case "rough":
                _rb.angularDrag = 0.9f;
                return 0.5f;
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
    public void ShootBall(float power, float angle)
    {
        _rb.AddRelativeForce(Vector3.forward * power, ForceMode.Impulse);
        Debug.Log("Ball got force");
        EventManager.TriggerEvent(GameState.BALL_IN_MOTION);
    }


    void OnGUI()
    {
        GUI.Box(new Rect(50, 80, 250, 25), "Magnitude: " + _rb.velocity.magnitude + ", surface: " + base.surfaceName.ToString());
    }

}
