using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPhysics : MonoBehaviour
{
    float PI = 3.1415f;
    float gravity = 9.81f;

    Our_Vector3 position;
    Our_Vector3 lVelocity;

    float mass;
    float radius;
    float wVelocity;

    float alpha;
    float airDensity;
    float dt; //Tiempo de chute
    float Cd; //Coeficiente drag.
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
        inertiaMoment = (2 / 3) * mass * (radius * radius);
        Our_Vector3 v3 = new Our_Vector3(0,0,0); //Asi se inicializa un Our_Vector.
        v3.x = 10; //Esto ya funciona.
      
        //TO DO - Aplicar formulas de angulo y diagramas de fuerza en el suelo
        // Calcular direccion Tau
        //fTau = v3.CrossProduct()
        //Calcular wVelocity (Inicial)
        //wVelocity = fTau / inertiaMoment * dt;
        //Calcular lVelocity (Inicial)
        
         
    }

    void Update()
    {
        //TO DO - Aplicar formulas de diagrama de fuerzas en el aire
        //Calcular fGravity

        //fGravity.setVariables(0, 0, -mass * gravity);
        fGravity = new Our_Vector3(0, 0, -mass * gravity);

        //Calcular fDrag

        //Calcular fMagnus
        //Agrupar fTotal
         

        //transform.position.x *= fTotal.x;
        //transform.position.y *= fTotal.y;
        //transform.position.z *= fTotal.z;
    }
}