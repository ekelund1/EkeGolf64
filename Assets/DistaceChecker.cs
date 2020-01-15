using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistaceChecker : MonoBehaviour
{
    public GameObject theBall;
    public GameObject theHole;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnGUI()
    {
        GUI.Box(new Rect(50, 800, 250, 25), "Distance: " +
            Vector2.Distance(new Vector2(theBall.transform.position.x, theBall.transform.position.z),
             new Vector2(theHole.transform.position.x, theHole.transform.position.z)));
    }
}
