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
    Our_Vector3 lVelocityInit = new Our_Vector3(0, 0, 0);
    Our_Vector3 lVelocityFin = new Our_Vector3(0, 0, 0);
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

    Our_Vector3 fDrag = new Our_Vector3(0, 0, 0);
    Our_Vector3 fMagnus = new Our_Vector3(0, 0, 0);
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
        getKickPosition = GameObject.Find("Pelota").GetComponent<GetKickPosition>().fromBallCoordinates; //Acceder a una propiedad de otro script en un objeto.

        fGravity = new Our_Vector3(0, 0, -mass * gravity);
        fMagnus = new Our_Vector3(0, 0, 0);
        fDrag = new Our_Vector3(0, 0, 0);

        rad.x = position.x - getKickPosition.x; //esto creo que cuando fromBallCoordinates sea un OurVector funcionara 
        rad.y = position.y - getKickPosition.y;
        rad.z = position.z - getKickPosition.z;

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

        //Calcular direccion Tau
        fTau = new Our_Vector3(0, 0, 0);
        fTau = rad.CrossProduct(fP); //hay que asignarle la barra de fuerza a fP
        //Calcular wVelocity (Inicial)
        wVelocity.x = fTau.x * inertiaMoment * dt;
        wVelocity.y = fTau.y * inertiaMoment * dt;
        wVelocity.z = fTau.z * inertiaMoment * dt;
        //Calcular lVelocity (Inicial)
        lVelocityInit.x = (fP.x * dt) / mass;
        lVelocityInit.y = (fP.y * dt) / mass;
        lVelocityInit.z = (fP.z * dt) / mass;
    }

    void Update()
    {
        fP.module = barra.value;
        // Debug.Log(VectorDireccion.transform.position.z);
        if (Input.GetKey(KeyCode.Space))
        {
            startKick();
        }
        
        transform.position = new Vector3(fTotal.x, fTotal.y, fTotal.z);
    }

    void startKick()
    {
        //Actualizar la velocidada lineal
       
        //Calcular fDrag
        fDrag.x = -Kd * lVelocityFin.Module() * lVelocityFin.x;
        fDrag.y = -Kd * lVelocityFin.Module() * lVelocityFin.y;
        fDrag.z = -Kd * lVelocityFin.Module() * lVelocityFin.z;
        lVelocityFin.x = (fDrag.x*dt);
        lVelocityFin.y = (fDrag.y * dt);
        lVelocityFin.z = (fDrag.z * dt);
        //Calcular fMagnus

        wVelocity.Normalize();
        fMagnus.x = Km * lVelocityFin.Module() * (wVelocity.y * lVelocityFin.z - lVelocityFin.y * wVelocity.z);
        fMagnus.y = Km * lVelocityFin.Module() * (wVelocity.x * lVelocityFin.z - lVelocityFin.x * wVelocity.z);
        fMagnus.z = Km * lVelocityFin.Module() * (wVelocity.x * lVelocityFin.y - lVelocityFin.x * wVelocity.y);

        //Agrupar fTotal
        //fTotal = new Our_Vector3(0, 0, 0);
        fTotal.x = fDrag.x + fMagnus.x;
        fTotal.y = -fDrag.y + fMagnus.y;
        fTotal.z = -fDrag.z + fMagnus.z + fGravity.z;

        //asignar la fuerza a transform.position
       // transform.position = new Vector3(fTotal.x, fTotal.y, fTotal.z);

        transform.position.Set(fTotal.x, fTotal.y, fTotal.z); //las propiedades get y set podemos usarlas
    }
}