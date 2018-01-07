using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setBallPointVector : MonoBehaviour {


    public Transform pelota;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        LineRenderer myLine = GetComponent<LineRenderer>();
       // myLine.SetPosition(0, new Vector3(transform.position.x, transform.position.y, transform.position.z));
        myLine.SetPosition(1, new Vector3(pelota.position.x, pelota.position.y, pelota.position.z));
    }
}
