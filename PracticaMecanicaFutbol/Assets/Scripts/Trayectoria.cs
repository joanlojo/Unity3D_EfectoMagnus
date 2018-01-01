using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trayectoria : MonoBehaviour {

    public Transform tPelota;
    public Material newMat;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        GameObject path = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        path.transform.localScale = new Vector3(0.03F, 0.03F, 0.03F);
        path.transform.position = tPelota.position;
        path.GetComponent<Renderer>().material = newMat;
	}
}
