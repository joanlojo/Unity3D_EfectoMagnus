using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetKickPosition : MonoBehaviour {

	public GameObject newPelota;
	private RaycastHit colision;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)){
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast (ray, out colision)) {
				Debug.Log (colision.collider.name);
				newPelota.transform.position = colision.point;
			}
		}
	}
}
