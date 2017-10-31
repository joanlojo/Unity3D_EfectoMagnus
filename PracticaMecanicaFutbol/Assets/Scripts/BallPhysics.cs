using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallPhysics : MonoBehaviour
{
    float PI = 3.1415f;
    float gravity = 9.81f;

    //public Transform sphere;
    public Transform VectorDireccion;
    public Slider barra;

    Our_Vector3 position = new Our_Vector3(0, 0, 0);
    Our_Vector3 lVelocity = new Our_Vector3(0, 0, 0);
    Our_Vector3 wVelocity = new Our_Vector3(0, 0, 0);
    float mass;
    float radius;

    float alpha;
    float airDensity;
    float dt; //Tiempo de chute
    float Kd;
    float Cd; //Coeficiente drag.
    float Km;
    float Cm; //Coeficiente magnus.
    float inertiaMoment;

    public Our_Vector3 getKickPosition;

    Our_Vector3 fDrag;
    Our_Vector3 fMagnus;
    Our_Vector3 fGravity;
    Our_Vector3 fTau = new Our_Vector3(0, 0, 0);
    Our_Vector3 fP = new Our_Vector3(0, 0, 0);
    Our_Vector3 rad; //Este vector es el que necesitamos para fTau, es entre el centro y el punto de impacto

    Our_Vector3 fTotal = new Our_Vector3(0, 0, 0); //Aqui guardamos la suma de todas las fuerzas.

    float Area() {
        return (radius * radius) * PI;
    }

    void Start()
    {
        fGravity = new Our_Vector3(0, 0, -mass * gravity);
        fMagnus = new Our_Vector3(0, 0, 0);
        fDrag = new Our_Vector3(0, 0, 0);

        //rad = position - getKickPosition.fromBallCoordinates; //esto creo que cuando fromBallCoordinates sea un OurVector funcionara 
        Debug.Log(GetComponent<GetKickPosition>().newPelota.transform.position.x);

        getKickPosition.x = GetComponent<GetKickPosition>().newPelota.transform.position.x;
        getKickPosition.y = GetComponent<GetKickPosition>().newPelota.transform.position.y;
        getKickPosition.z = GetComponent<GetKickPosition>().newPelota.transform.position.z;

        fP.x = getKickPosition.x - VectorDireccion.transform.position.x;
        fP.y = getKickPosition.y - VectorDireccion.transform.position.y;
        fP.z = getKickPosition.z - VectorDireccion.transform.position.z;

        float area = Area();
        airDensity = 1.23f;
        Cd = 0.25f;
        Cm = 0.25f;
        mass = 0.396f;
        radius = 0.279f;
        dt = 0.01f;

        inertiaMoment = (2 / 3) * mass * (radius * radius);
        Kd = (1 / 2) * airDensity * Cd * area;
        Km = (1 / 2) * airDensity * Cm * area;  
        // Calcular direccion Tau
        fTau = new Our_Vector3(0, 0, 0);
        fTau = rad.CrossProduct(fP); //hay que asignarle la barra de fuerza a fP
        //Calcular wVelocity (Inicial)
        wVelocity.x = fTau.x * inertiaMoment * dt;
        wVelocity.y = fTau.y * inertiaMoment * dt;
        wVelocity.z = fTau.z * inertiaMoment * dt;
        //Calcular lVelocity (Inicial)
        lVelocity.x = (fP.x * dt) / mass;
        lVelocity.y = (fP.y * dt) / mass;
        lVelocity.z = (fP.z * dt) / mass;

    }

    void Update()
    {
        fP.module = barra.value;
       // Debug.Log(VectorDireccion.transform.position.z);
        if (Input.GetKey(KeyCode.Space))
        {
            startKick();
        }
    }

    void startKick()
    {
        //Calcular fGravity

        //Calcular fDrag
        fDrag.x = -Kd * lVelocity.Module() * lVelocity.x;
        fDrag.y = -Kd * lVelocity.Module() * lVelocity.y;
        fDrag.z = -Kd * lVelocity.Module() * lVelocity.z;
        //Calcular fMagnus

        wVelocity.Normalize();
        fMagnus.x = Km * lVelocity.Module() * (wVelocity.y * lVelocity.z - lVelocity.y * wVelocity.z);
        fMagnus.y = Km * lVelocity.Module() * (wVelocity.x * lVelocity.z - lVelocity.x * wVelocity.z);
        fMagnus.z = Km * lVelocity.Module() * (wVelocity.x * lVelocity.y - lVelocity.x * wVelocity.y);
        //Agrupar fTotal
        //fTotal = new Our_Vector3(0, 0, 0);
        fTotal.x = fDrag.x + fMagnus.x;
        fTotal.y = -fDrag.y + fMagnus.y;
        fTotal.z = -fDrag.z + fMagnus.z + fGravity.z;

        //
        Vector3 temporal = new Vector3(fTotal.x, fTotal.y, fTotal.z);
        //
        transform.position += temporal;
    }
}