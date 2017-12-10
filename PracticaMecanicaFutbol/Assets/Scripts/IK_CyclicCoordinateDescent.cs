using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IK_CyclicCoordinateDescent : MonoBehaviour {

    //CYCLIC COORDINATE DESCENT

    public GameObject[] joints; //Robotic arm joints
    public GameObject target; //Pelota
    public float[] theta; //Angulos para cada joint

    private float[] sin;
    private float[] cos;

    private bool done = false; //Comprivar si se alcanza la posicion objetivo
    private Vector3 targetPosition; //Guarda la posicion del target

    private int intentos = 0;
    private int Max_intentos = 10;

    private float rango = 0.1f;

	void Start () {
        theta = new float[joints.Length];   //Crea arrays de igual tamaño a la contidad de joints
        sin = new float[joints.Length];
        cos = new float[joints.Length];
        targetPosition = target.transform.position;
	}
	
	void Update () {
        if (!done)
        {
            if(intentos < Max_intentos)
            {
                for(int i = joints.Length-2; i >= 0; i--)
                {
                    //CCD algoritmo
                    Vector3 r1 = joints[joints.Length - 1].transform.position - joints[i].transform.position;
                    Vector2 r2 = targetPosition - joints[i].transform.position;

                    if (r1.magnitude * r2.magnitude <= 0.001f)
                    {

                    }
                    else
                    {
                        cos[i] = Vector3.Dot(r1, r2) / (r1.magnitude * r2.magnitude);
                        sin[i] = Vector3.Cross(r1, r2).magnitude / (r1.magnitude * r2.magnitude);
                    }
                   

                    Vector3 rotationAxis = Vector3.Cross(r1, r2).normalized;
                    theta[i] = Mathf.Acos(cos[i]);

                    if (sin[i] < 0) { theta[i] = -theta[i]; }

                    theta[i] *= Mathf.Rad2Deg;

                    joints[i].transform.rotation = Quaternion.AngleAxis(theta[i], rotationAxis) * joints[i].transform.rotation;
                }
                intentos++;
            }
        }
        float f = (targetPosition - joints[joints.Length - 1].transform.position).magnitude;

        if (f < rango) { done = true; } else { done = false; } //End effector esta lo suficientemente cerca del target

        if(targetPosition != target.transform.position)
        {
            intentos = 0;
            targetPosition = target.transform.position;
        }
	}
}
