using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class PlayerBoxControl : MonoBehaviour
{
    // Start is called before the first frame update
    public float RotateSpeed = 50f;
    public float angle = 30f;

    public float playerMaxPower = 20f;

    public float currentPower = 1f;
    public float maxPower = 1f;
    public float minPower = 0.1f;
    public GameObject aimIndicator;

    public Clubs selectedClub = Clubs.W1;

    public Vector3 offset = new Vector3(0, 0, 0);
    public BallTest theBall;

    public SwingManager swingManager;
    void Start()
    {
        GameStateManager.StartListening(GameState.SETUP_SHOT, moveBox);
        moveBox();
        //transform.localRotation = Quaternion.AngleAxis(angle, Vector3.right);
    }

    void moveBox()
    {
        transform.position = theBall.transform.position - offset;
        transform.rotation = theBall.transform.rotation;
    }

    private void ChangeClub(bool up = false)
    {
        if (up)
        {
            selectedClub = (Clubs)Mathf.Clamp((float)selectedClub + 1, 0, 13);

            aimIndicator.transform.eulerAngles =
                new Vector3(
                     -ClubDictionary.getClubData(selectedClub).angle,
                    aimIndicator.transform.eulerAngles.y,
                    aimIndicator.transform.eulerAngles.z
            );
            //aimIndicator.transform.Rotate(new Vector3(ClubDictionary.getClubData(selectedClub).angle, 0, 0));
            return;
        }

        aimIndicator.transform.eulerAngles = new Vector3(-ClubDictionary.getClubData(selectedClub).angle, aimIndicator.transform.eulerAngles.y, aimIndicator.transform.eulerAngles.z);
        selectedClub = (Clubs)Mathf.Clamp((float)selectedClub - 1, 0, 13);

    }

    private void ChangePower(bool up = true)
    {
        if (up)
        {
            currentPower += Time.deltaTime;
        }
        else
        {
            currentPower -= Time.deltaTime;
        }
        currentPower = Mathf.Clamp(currentPower, minPower, maxPower);

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

        if (Input.GetKey(KeyCode.Home))
        {
            ChangePower(true);
        }
        if (Input.GetKey(KeyCode.End))
        {
            ChangePower(false);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            theBall.GetComponent<Rigidbody>().Sleep();
            theBall.transform.position = transform.position;
            theBall.transform.localRotation = transform.rotation;
        }
        if (Input.GetKeyDown(KeyCode.Space) && GameStateManager.activeEvent == GameState.SETUP_SHOT)
        {
            var swing = swingManager.ButtonPressed();

            if (swing.powerModifier > 0 && swing.accuracy > 0)
                theBall.ShootBall(20, selectedClub, this.transform.rotation, swing.powerModifier);
        }


    }

}
