using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colision : MonoBehaviour {
    public Our_Vector3 velocityP;
    public GameObject pelota;
	// Use this for initialization
	void Start () {
        velocityP = GameObject.Find("Pelota").GetComponent<BallPhysics>().lVelocityFin;
    }
	
	// Update is called once per frame
	void Update () {
        if(transform.position == pelota.transform.position)
        {

        }
	}
}
