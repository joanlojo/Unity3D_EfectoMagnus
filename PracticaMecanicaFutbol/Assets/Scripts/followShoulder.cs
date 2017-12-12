using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followShoulder : MonoBehaviour {

    public Transform Hombro;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = Hombro.transform.position;
	}
}
