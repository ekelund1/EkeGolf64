using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCameraUI : MonoBehaviour
{
    // Start is called before the first frame update

    public BallTest ball;




    // Update is called once per frame
    void Update()
    {

    }
    private void OnGUI()
    {
        float distance = Mathf.Round(Vector2.Distance(new Vector2(ball.startPos.x, ball.startPos.z), new Vector2(ball.transform.position.x, ball.transform.position.z)));
        GUI.Box(new Rect(400, 600, 150, 20), "Distance " + distance.ToString() + "units");
    }
}
