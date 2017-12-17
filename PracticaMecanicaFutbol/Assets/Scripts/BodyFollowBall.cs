using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyFollowBall : MonoBehaviour {

    // Use this for initialization
    public Transform Pelota;
    
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Pelota.position.z < 0.75f && Pelota.position.z > -0.75f) { transform.position = new Vector3(-5.36f, -1.12f, Pelota.position.z); }
	}
}
