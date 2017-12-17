using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IK_CyclicCoordinateDescent : MonoBehaviour {

    //CYCLIC COORDINATE DESCENT

    public GameObject[] joints; //Robotic arm joints
    public GameObject target; //Pelota
    public float[] theta; //Angulos para cada joint
    private float[] firstAngles; //Angulos iniciales de los joints

    public string type;

    [Range(0.0f, 180.0f)]
    public float maxAngle = 360.0f;

    [Range(0.0f, 360.0f)]
    public float minAngle = 0.0f;

    [SerializeField]
    private float[] sin;
    [SerializeField]
    private float[] cos;

    private bool done = false; //Comprivar si se alcanza la posicion objetivo
    private Our_Vector3 targetPosition = new Our_Vector3(0,0,0); //Guarda la posicion del target
    //private Vector3 targetPosition;
    [SerializeField]
    private int intentos = 0;
    [SerializeField]
    private int M_intentos = 10;

    private float rango = 0.1f;

	void Start () {
        theta = new float[joints.Length];   //Crea arrays de igual tamaño a la contidad de joints
        firstAngles = new float[joints.Length];
        sin = new float[joints.Length];
        cos = new float[joints.Length];
        targetPosition.x = target.transform.position.x;
        targetPosition.y = target.transform.position.y;
        targetPosition.z = target.transform.position.z;
    }
	
	void Update () {
    
        if (!done)
        {
            if (intentos <= M_intentos)
            {
                for (int i = joints.Length - 2; i >= 0; i--)
                {
                    //Vector3 r1 = new Vector3(0,0,0);//= joints[joints.Length - 1].transform.position - joints[i].transform.position;               
                    //Vector3 r2 = new Vector3(0,0,0); //= targetPosition - joints[i].transform.position;

                    Our_Vector3 r1 = new Our_Vector3(0, 0, 0);
                    r1.x = joints[joints.Length - 1].transform.position.x - joints[i].transform.position.x;
                    r1.y = joints[joints.Length - 1].transform.position.y - joints[i].transform.position.y;
                    r1.z = joints[joints.Length - 1].transform.position.z - joints[i].transform.position.z;
                    Our_Vector3 r2 = new Our_Vector3(0, 0, 0);
                    r2.x = targetPosition.x - joints[i].transform.position.x;
                    r2.y = targetPosition.y - joints[i].transform.position.y;
                    r2.z = targetPosition.z - joints[i].transform.position.z;
                    if (r1.Module() * r2.Module() <= 0.001f)
                    {

                    }
                    else
                    {
                        //cos[i] = Vector3.Dot(r1, r2) / (r1.magnitude * r2.magnitude);
                        //sin[i] = Vector3.Cross(r1, r2).magnitude / (r1.magnitude * r2.magnitude);
                        cos[i] = r1.DotProduct(r2) / (r1.Module() * r2.Module());
                        sin[i] = r1.CrossProduct(r2).Module() / (r1.Module() * r2.Module());

                    }

                    Our_Vector3 rotationAxis = rotationAxis = r1.CrossProduct(r2);
                    rotationAxis.Normalize();

                    if(type == "cadera")
                    {
                        if (i == 0)
                        {

                            theta[i] = Mathf.Acos(cos[i]);
                            theta[i] = theta[i] * Mathf.Rad2Deg;
                            if (sin[i] < 0) { theta[i] = -theta[i]; }

                            Our_Quaternion rt = new Our_Quaternion(joints[i].transform.rotation.x, joints[i].transform.rotation.y, joints[i].transform.rotation.z, joints[i].transform.rotation.w);
                            Our_Quaternion myRotation = new Our_Quaternion(theta[i], rotationAxis); //ESTO FALLA
                            myRotation.Multiply(rt);
                            myRotation.y = myRotation.z = 0; //Rotation only in X axis.


                            Quaternion temp = new Quaternion(myRotation.x, myRotation.y, myRotation.z, myRotation.w);

                            float angleF;
                            Vector3 axisF;
                            temp.ToAngleAxis(out angleF, out axisF);
                            //Debug.Log(angleF);
                            if (angleF > 130 && angleF < 230)
                            {

                                joints[i].transform.rotation = new Quaternion(myRotation.x, myRotation.y, myRotation.z, myRotation.w);
                            }
                        }
                    }

                    if (type == "brazoD")
                    {
                        if (i == 0)
                        {

                            theta[i] = Mathf.Acos(cos[i]);
                            theta[i] = theta[i] * Mathf.Rad2Deg;
                            if (sin[i] < 0) { theta[i] = -theta[i]; }

                            Our_Quaternion rt = new Our_Quaternion(joints[i].transform.rotation.x, joints[i].transform.rotation.y, joints[i].transform.rotation.z, joints[i].transform.rotation.w);
                            Our_Quaternion myRotation = new Our_Quaternion(theta[i], rotationAxis); //ESTO FALLA
                            myRotation.Multiply(rt);
                            myRotation.y = myRotation.z = 0; //Rotation only in X axis.


                            Quaternion temp = new Quaternion(myRotation.x, myRotation.y, myRotation.z, myRotation.w);

                            float angleF;
                            Vector3 axisF;
                            temp.ToAngleAxis(out angleF, out axisF);
                            //Debug.Log(angleF);
                            if (angleF > 15 && angleF < 270)
                            {

                                joints[i].transform.rotation = new Quaternion(myRotation.x, myRotation.y, myRotation.z, myRotation.w);
                            }
                        }
                     
                        if (i == 1)
                        {
                            
                            theta[i] = Mathf.Acos(cos[i]);
                            theta[i] = theta[i] * Mathf.Rad2Deg;
                            if (sin[i] < 0) { theta[i] = -theta[i]; }

                            Our_Quaternion rt = new Our_Quaternion(joints[i].transform.rotation.x, joints[i].transform.rotation.y, joints[i].transform.rotation.z, joints[i].transform.rotation.w);
                            Our_Quaternion myRotation = new Our_Quaternion(theta[i], rotationAxis); //ESTO FALLA
                            myRotation.Multiply(rt);
                            myRotation.y = myRotation.z = 0; //Rotation only in X axis.


                            Quaternion temp = new Quaternion(myRotation.x, myRotation.y, myRotation.z, myRotation.w);

                            float angleF;
                            Vector3 axisF;
                            temp.ToAngleAxis(out angleF, out axisF);
                            Debug.Log(angleF);
                            if (angleF > 10 && angleF < 90)
                            {

                                joints[i].transform.rotation = new Quaternion(myRotation.x, myRotation.y, myRotation.z, myRotation.w);
                            }
                        }
                    }

                    if (type == "brazoI")
                    {
                        if (i == 0)
                        {

                            theta[i] = Mathf.Acos(cos[i]);
                            theta[i] = theta[i] * Mathf.Rad2Deg;
                            if (sin[i] < 0) { theta[i] = -theta[i]; }

                            Our_Quaternion rt = new Our_Quaternion(joints[i].transform.rotation.x, joints[i].transform.rotation.y, joints[i].transform.rotation.z, joints[i].transform.rotation.w);
                            Our_Quaternion myRotation = new Our_Quaternion(theta[i], rotationAxis); //ESTO FALLA
                            myRotation.Multiply(rt);
                            myRotation.y = myRotation.z = 0; //Rotation only in X axis.


                            Quaternion temp = new Quaternion(myRotation.x, myRotation.y, myRotation.z, myRotation.w);

                            float angleF;
                            Vector3 axisF;
                            temp.ToAngleAxis(out angleF, out axisF);
                            //Debug.Log(angleF);
                            if (angleF > 90  && angleF < 345)
                            {

                                joints[i].transform.rotation = new Quaternion(myRotation.x, myRotation.y, myRotation.z, myRotation.w);
                            }
                        }
                        if (i == 1)
                        {

                            theta[i] = Mathf.Acos(cos[i]);
                            theta[i] = theta[i] * Mathf.Rad2Deg;
                            if (sin[i] < 0) { theta[i] = -theta[i]; }

                            Our_Quaternion rt = new Our_Quaternion(joints[i].transform.rotation.x, joints[i].transform.rotation.y, joints[i].transform.rotation.z, joints[i].transform.rotation.w);
                            Our_Quaternion myRotation = new Our_Quaternion(theta[i], rotationAxis); //ESTO FALLA
                            myRotation.Multiply(rt);
                            myRotation.y = myRotation.z = 0; //Rotation only in X axis.


                            Quaternion temp = new Quaternion(myRotation.x, myRotation.y, myRotation.z, myRotation.w);

                            float angleF;
                            Vector3 axisF;
                            temp.ToAngleAxis(out angleF, out axisF);

                            //Debug.Log(angleF);
                            if (angleF > 50 && angleF > 270)
                            {
                                joints[i].transform.rotation = new Quaternion(myRotation.x, myRotation.y, myRotation.z, myRotation.w);
                            }
                        }
                    }

                }
                intentos++;
            }
        }
        float f = (targetPosition.x - joints[joints.Length - 1].transform.position.x + targetPosition.y - joints[joints.Length - 1].transform.position.y + targetPosition.z - joints[joints.Length - 1].transform.position.z);

        if (f < rango) { done = true; } else { done = false; } //End effector esta lo suficientemente cerca del target

        if(targetPosition.x != target.transform.position.x || targetPosition.y != target.transform.position.y || targetPosition.z != target.transform.position.z) //targetPosition != target.transform.position
        {
            intentos = 0;
           //targetPosition = target.transform.position;
            targetPosition.x = target.transform.position.x;
            targetPosition.y = target.transform.position.y;
            targetPosition.z = target.transform.position.z;
        }
	}
}
