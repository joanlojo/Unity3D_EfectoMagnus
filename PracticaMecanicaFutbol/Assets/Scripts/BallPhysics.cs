using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPhysics : MonoBehaviour
{
    float PI = 3.1415f;
    float gravity = 9.81f;

    Our_Vector3 position;
    Our_Vector3 lVelocity;
    Our_Vector3 wVelocity;
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

    Our_Vector3 fDrag;
    Our_Vector3 fMagnus;
    Our_Vector3 fGravity;
    Our_Vector3 fTau;
    Our_Vector3 fP;
    Our_Vector3 rad; //Este vector es el que necesitamos para fTau, es entre el centro y el punto de impacto

    Our_Vector3 fTotal; //Aqui guardamos la suma de todas las fuerzas.


    float Area()
    {
        return (radius * radius) * PI;
    }

    void Start()
    {
        //GetKickPosition getKickPosition;
        //rad = position - getKickPosition.fromBallCoordinates; //esto creo que cuando fromBallCoordinates sea un OurVector funcionara 

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
        // Our_Vector3 vCross = new Our_Vector3(0,0,0); //Asi se inicializa un Our_Vector.   
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
        //Calcular fGravity
        fGravity = new Our_Vector3(0, 0, -mass * gravity);
        //Calcular fDrag
        fDrag = new Our_Vector3(0, 0, 0);
        fDrag.x = -Kd * lVelocity.Module() * lVelocity.x;
        fDrag.y = -Kd * lVelocity.Module() * lVelocity.y;
        fDrag.z = -Kd * lVelocity.Module() * lVelocity.z;
        //Calcular fMagnus
        fMagnus = new Our_Vector3(0, 0, 0);
        wVelocity.Normalize();
        fMagnus.x = Km * lVelocity.Module() * (wVelocity.y * lVelocity.z - lVelocity.y * wVelocity.z);
        fMagnus.y = Km * lVelocity.Module() * (wVelocity.x * lVelocity.z - lVelocity.x * wVelocity.z);
        fMagnus.z = Km * lVelocity.Module() * (wVelocity.x * lVelocity.y - lVelocity.x * wVelocity.y);
        //Agrupar fTotal
        fTotal = new Our_Vector3(0, 0, 0);
        fTotal.x = fDrag.x + fMagnus.x;
        fTotal.y = -fDrag.y + fMagnus.y;
        fTotal.z = -fDrag.z + fMagnus.z + fGravity.z;

        //transform.position.x *= fTotal.x;
        //transform.position.y *= fTotal.y;
        //transform.position.z *= fTotal.z;
    }
}