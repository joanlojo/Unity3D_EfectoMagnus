using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colision : MonoBehaviour {
    public Our_Vector3 velocityP;
    public GameObject pelota;
	// Use this for initialization
	void Start () {
        velocityP = GameObject.Find("Pelota").GetComponent<BallPhysics>().lVelocityInit;
    }
	
	// Update is called once per frame
	void Update () {

        // 0.5   ---  3
        Our_Vector3 dist = new Our_Vector3(pelota.transform.position.x - transform.position.x, pelota.transform.position.y - transform.position.y, pelota.transform.position.z - transform.position.z);
        Debug.DrawLine(pelota.transform.position, transform.position, Color.green);
        if(dist.Module() < 0.2)
        {
            Debug.Log("Col");
            velocityP.x = 0;
            velocityP.y = 0;
            velocityP.z = 0;
            
        }
        //Debug.Log(velocityP.x);
    }
}
