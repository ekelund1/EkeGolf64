using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class PlayerBoxControl : MonoBehaviour
{
    // Start is called before the first frame update
    public float RotateSpeed = 50f;
    public float angle = 30f;

    public float playerMaxPower = 20f;

    public Clubs selectedClub = Clubs.W1;

    public Vector3 offset = new Vector3(0, 0, 0);
    public BallTest theBall;
    void Start()
    {
        GameStateManager.StartListening(GameState.SETUP_SHOT, moveBox);
        moveBox();
        //transform.localRotation = Quaternion.AngleAxis(angle, Vector3.right);
    }

    void moveBox()
    {
        transform.position = theBall.transform.position - offset;
    }

    private void ChangeClub(bool up = false)
    {
        if (up)
        {
            selectedClub = (Clubs)Mathf.Clamp((float)selectedClub + 1, 0, 13);
            return;
        }
        selectedClub = (Clubs)Mathf.Clamp((float)selectedClub - 1, 0, 13);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            theBall.transform.RotateAround(theBall.transform.position, Vector3.down, RotateSpeed);
            transform.RotateAround(transform.position, Vector3.down, RotateSpeed);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            theBall.transform.RotateAround(theBall.transform.position, Vector3.up, RotateSpeed);
            transform.RotateAround(transform.position, Vector3.up, RotateSpeed);
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            theBall.transform.RotateAround(theBall.transform.position, Vector3.right, 0.5f * RotateSpeed);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            theBall.transform.RotateAround(theBall.transform.position, Vector3.left, 0.5f * RotateSpeed);
        }

        if (Input.GetKeyDown(KeyCode.PageDown))
        {
            ChangeClub(false);
        }
        if (Input.GetKeyDown(KeyCode.PageUp))
        {
            ChangeClub(true);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            theBall.GetComponent<Rigidbody>().Sleep();
            theBall.transform.position = transform.position;
            theBall.transform.localRotation = transform.rotation;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            theBall.ShootBall(20, 10);
        }
    }
}
