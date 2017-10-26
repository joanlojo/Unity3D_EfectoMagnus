using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPhysics : MonoBehaviour
{
    float PI = 3.1415f;
    Our_Vector3 gravity = new Our_Vector3();

    Our_Vector3 position;
    Our_Vector3 lVelocity;

    float mass;
    float radius;
    float wVelocity;

    float alpha;
    float airDensity;
    float dt; //tiempo de chute
    float Cd; //Coeficiente drag.
    float Cm; //Coeficiente magnus.
    float inertiaMoment;

    Our_Vector3 fDrag;
    Our_Vector3 fMagnus;
    Our_Vector3 fGravity;
    Our_Vector3 fTau;

    Our_Vector3 fTotal; // Aqui guardamos la suma de todas las fuerzas.


    float Area()
    {
        return (radius * radius) * PI;
    }

    void Start()
    {
        gravity.setVariables(0.0f,0.0f,-9.8f);
        /*TO DO - Aplicar formulas de angulo y diagramas de fuerza en el suelo
         Calcular direccion Tau
         Calcular wVelocity (Inicial)
         Calcular lVelocity (Inicial)
         */
    }

    void Update()
    {
        /*TO DO - Aplicar formulas de diagrama de fuerzas en el aire
         Calcular fGravity
         Calcular fDrag
         Calcular fMagnus
         Agrupar fTotal
         */

        //transform.position.x *= fTotal.getX();
        //transform.position.y *= fTotal.getY();
        //transform.position.z *= fTotal.getZ();
    }
}