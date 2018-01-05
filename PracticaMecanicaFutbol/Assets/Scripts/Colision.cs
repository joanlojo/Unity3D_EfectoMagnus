using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colision : MonoBehaviour {
    public Our_Vector3 velocityP, gravity, Welocity, tau;
    public GameObject pelota;
	// Use this for initialization
	void Start () {
        velocityP = GameObject.Find("Pelota").GetComponent<BallPhysics>().lVelocityInit;
        gravity = GameObject.Find("Pelota").GetComponent<BallPhysics>().fGravity;
        Welocity = GameObject.Find("Pelota").GetComponent<BallPhysics>().wVelocity;
        tau = GameObject.Find("Pelota").GetComponent<BallPhysics>().fTau;
    }
	
	// Update is called once per frame
	void Update () {

        // 0.5   ---  3
        Our_Vector3 dist = new Our_Vector3(pelota.transform.position.x - transform.position.x, pelota.transform.position.y - transform.position.y, pelota.transform.position.z - transform.position.z);
        Debug.DrawLine(pelota.transform.position, transform.position, Color.green);
        if(dist.Module() < 0.2)
        {
            //Debug.Log("Col");
            gravity.y = 0;
            Welocity.module = 0;
            Welocity.x = 0;
            Welocity.y = 0;
            Welocity.z = 0;
            tau.x = 0;
            tau.y = 0;
            tau.z = 0;
            velocityP.x = 0;
            velocityP.y = 0;
            velocityP.z = 0;
            
            
        }
        //Debug.Log(velocityP.x);
    }
}
