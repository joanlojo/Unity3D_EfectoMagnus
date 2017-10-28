using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetKickPosition : MonoBehaviour {

	public GameObject newPelota;
    public GameObject VectorDireccion;
	private RaycastHit colision;

	void Start () {
		
	}
	
	void Update () {
		if(Input.GetMouseButtonDown(0)){
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast (ray, out colision)) {
				newPelota.transform.position = colision.point;
                VectorDireccion.transform.position = colision.point;

                //Coordenadas del click respecto a la pelota.
				Vector3 fromBallCoordinates = newPelota.transform.position-colision.transform.position; 
				Debug.Log ("Punto de colision respecto a la pelota"+ fromBallCoordinates);
			}
		}

        //Esto no funciona con Our_Vector3 por como funciona el .Rotate internamente
        //Habra que mirarlo mejor o implementarlo de forma distinta
        if (Input.GetKey(KeyCode.UpArrow))
        {
            Vector3 rot = new Vector3(-1, 0, 0);
            VectorDireccion.transform.Rotate(rot);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            Vector3 rot = new Vector3(1,0, 0);
            VectorDireccion.transform.Rotate(rot);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Vector3 rot = new Vector3(0, -1, 0);
            VectorDireccion.transform.Rotate(rot);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            Vector3 rot = new Vector3(0, 1, 0);
            VectorDireccion.transform.Rotate(rot);
        }
    }
}
