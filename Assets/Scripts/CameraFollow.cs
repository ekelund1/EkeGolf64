using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform ballObject;
    public float distanceFromBall = 20f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookOnObject = ballObject.position - transform.position;
        lookOnObject = ballObject.position - transform.position;
        transform.forward = lookOnObject.normalized;

        

        distanceFromBall = Mathf.Max(ballObject.GetComponent<Rigidbody>().velocity.magnitude, 20f);

        Vector3 playerLastPosition;
        playerLastPosition = ballObject.position - lookOnObject.normalized * distanceFromBall;
        transform.position = playerLastPosition;
    }
}
