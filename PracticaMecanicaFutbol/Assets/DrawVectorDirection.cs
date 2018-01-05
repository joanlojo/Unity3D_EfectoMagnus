using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawVectorDirection : MonoBehaviour {

    public Transform pelota;
    private bool isKicked = false;
	// Use this for initialization
	void Start () {
        LineRenderer myLine = gameObject.AddComponent<LineRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
        if (isKicked == false)
        {
            LineRenderer myLine = GetComponent<LineRenderer>();
            myLine.SetPosition(0, new Vector3(transform.position.x, transform.position.y, transform.position.z));
            myLine.SetPosition(1, new Vector3(pelota.position.x, pelota.position.y, pelota.position.z));
            myLine.endWidth = 0.02f;
            myLine.startWidth = 0.001f;
            myLine.endColor = Color.red;
            myLine.startColor = Color.red;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isKicked = true;
        }
    }
}
