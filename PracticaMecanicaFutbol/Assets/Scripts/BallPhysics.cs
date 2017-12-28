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
    public Our_Vector3 lVelocityFin = new Our_Vector3(0, 0, 0);
    Our_Vector3 wVelocity = new Our_Vector3(0, 0, 0);
    float mass;
    float radius = 0.279f;

    float alpha;
    float airDensity;
    float dt; //Tiempo de chute
    float Kd;
    float Cd; //Coeficiente drag.
    float Km;
    float Cm; //Coeficiente magnus.
    float inertiaMoment;

    bool startKicked = false;

    public Our_Vector3 getKickPosition;
    Our_Vector3 puntoDeImpacto = new Our_Vector3(0, 0, 0);

    Our_Vector3 fDrag = new Our_Vector3(0, 0, 0);
    Our_Vector3 fMagnus = new Our_Vector3(0, 0, 0);
    Our_Vector3 fGravity;
    Our_Vector3 fTau = new Our_Vector3(0, 0, 0);
    Our_Vector3 fP = new Our_Vector3(0, 0, 0);
    Our_Vector3 dirfP = new Our_Vector3(0, 0, 0);
    Our_Vector3 rad = new Our_Vector3(0, 0, 0); //Este vector es el que necesitamos para fTau, es entre el centro y el punto de impacto

    Our_Vector3 fTotal = new Our_Vector3(0, 0, 0); //Aqui guardamos la suma de todas las fuerzas.
    Our_Vector3 wNorm = new Our_Vector3(0, 0, 0);
    Quaternion q;

    float Area() {
        return (radius * radius) * PI;
    }

    void Start()
    {
        getKickPosition = GameObject.Find("ScriptsObject").GetComponent<GetKickPosition>().fromBallCoordinates; //Punto de impacto a la pelota respecto a su centro    
        airDensity = 1.23f;
        Cd = 0.5f;//0.25 originalmente
        Cm = 0.5f;
        mass = 0.3f; //0.396f
        //radius = 0.279f; //0.279f
        dt = 0.01f; //PUEDE SER Q NECESITEMOS 2 DT
        inertiaMoment = (0.667f) * mass * (radius * radius);
        Kd = 0.5f * airDensity * Cd * Area();
        Km = 0.5f * airDensity * Cm * Area();
        
    }

    void startKick()
    {
       
        fGravity = new Our_Vector3(0, -mass * gravity,0);
        fMagnus = new Our_Vector3(0, 0, 0);
        fDrag = new Our_Vector3(0, 0, 0);
        fTau = new Our_Vector3(0, 0, 0);

        rad.x = getKickPosition.x - transform.position.x;// NO ESTOY 100% DE QUE ESTO ESTE BIEN
        rad.y = getKickPosition.y - transform.position.y;// VECTOR ENTRE EL CENTRO DE LA PELOTA Y EL PUNTO DE IMPACTO
        rad.z = getKickPosition.z - transform.position.z;//
        //Debug.Log(rad.Module());

        //fP.module = barra.value;// FUERZA TOTAL DE FP    
        dirfP.x = (getKickPosition.x - VectorDireccion.position.x);//CREO Q ESTA BIEN
        dirfP.y = (getKickPosition.y - VectorDireccion.position.y);//ESTO TE DA LA DIRECCION DEL VECTOR, PERO NO FP
        dirfP.z = (getKickPosition.z - VectorDireccion.position.z);//

        //FP EN SUS COMPONENTES 
        dirfP.Normalize();
        fP.x = dirfP.x * barra.value;//CREO Q ESTA BIEN
        fP.y = dirfP.y * barra.value;
        fP.z = dirfP.z * barra.value;
        //Calcular direccion Tau
        fTau = rad.CrossProduct(fP);//CREO QUE ESTA BIEN
       
        //Calcular wVelocity (Inicial)
        wVelocity.x = fTau.x / inertiaMoment * dt;
        wVelocity.y = fTau.y / inertiaMoment * dt;
        wVelocity.z = fTau.z / inertiaMoment * dt;
        //Calcular lVelocity (Inicial)
        lVelocityInit.x = (fP.x * dt) / mass;//aqui el dt puede ser q sea otro
        lVelocityInit.y = (fP.y * dt) / mass;
        lVelocityInit.z = (fP.z * dt) / mass;
        //Debug.Log("X :" + lVelocityInit.x);
        //Debug.Log("Y :" + lVelocityInit.y);
        //Debug.Log("Z :" + lVelocityInit.z);
        //Debug.Log(lVelocityInit.Module());
        //wNorm = wVelocity.Normalize();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
           startKick();
            startKicked = true;
        }
        //APLICAMOS LA ROTACION A LA PELOTA A PARTIR DE FTAU         
        transform.Rotate(new Vector3(fTau.x, fTau.y, fTau.z), wVelocity.Module());//PARA ROTAR PASAMOS EL EJE DE ROTACION Y EL MODULO DE HOMEGA
        Debug.DrawLine(new Vector3(transform.position.x, transform.position.y, transform.position.z), new Vector3(VectorDireccion.position.x, VectorDireccion.position.y, VectorDireccion.position.z), Color.black);//VEC DIRECCION IMPACTO
        Debug.DrawRay(transform.position, new Vector3(fTau.x, fTau.y, fTau.z), Color.black);//VECTOR DIR HOMEGA, EJE DE ROTACION
        if (startKicked == true){        
            //Calcular fDrag
            fDrag.x = -Kd * lVelocityInit.Module() * lVelocityInit.x;
            fDrag.y = -Kd * lVelocityInit.Module() * lVelocityInit.y;
            fDrag.z = -Kd * lVelocityInit.Module() * lVelocityInit.z;
            //Debug.Log("Drag" + lVelocityInit.Module());
            //Debug.Log("Drag x: " + fDrag.x);
            //Debug.Log("Drag y: " + fDrag.y);
            //Debug.Log("Drag z: " + fDrag.z);

            //Calcular fMagnus
            //Our_Vector3 wNorm = wVelocity
            Our_Vector3 wVelocityAux = new Our_Vector3(wVelocity.x, wVelocity.y, wVelocity.z);
            wVelocityAux.Normalize(); //ESTO ENTEORIA TIENE Q ESTAR NORMALIZADO PARA EL MAGNUS, PERO ENTONCES COMO ESTA EN EL BUCLE CUANDO APLICA LA ROTACION EL MODULO DE LA VELOCIDAD SIEMPRE SERA 1, Q ASI NO ESTARA BIEN
            //Debug.Log(wVelocity.Module());
            fMagnus.x = Km * lVelocityInit.Module() * (wVelocityAux.y * lVelocityInit.z - lVelocityInit.y * wVelocityAux.z);
            fMagnus.y = Km * lVelocityInit.Module() * (wVelocityAux.x * lVelocityInit.z - lVelocityInit.x * wVelocityAux.z);
            fMagnus.z = Km * lVelocityInit.Module() * (wVelocityAux.x * lVelocityInit.y - lVelocityInit.x * wVelocityAux.y);
            //Debug.Log("Magnus" + lVelocityInit.Module());
            //Debug.Log("Magnus x: " + fMagnus.x);
            //Debug.Log("Magnus y: " + fMagnus.y);
            //Debug.Log("Magnus z: " + fMagnus.z);
            //Agrupar fTotal
            fTotal.x = fDrag.x + fMagnus.x + fGravity.x;//
            fTotal.y = fDrag.y + fMagnus.y + fGravity.y;//CREO QUE NO HACE FALTA
            fTotal.z = fDrag.z + fMagnus.z + fGravity.z;//
            //Debug.Log("Antes:" + fTotal.Module());
            //ACTUALIZAR lVelocityInit A PARTIR DE LAS NUEVAS FORUMLAS CON EL METODO DE EULER   
            //vanterior = lVelocityFin
            //act v anterior
            //modificar lVelocityFin = vanterior
            float aTx = fTotal.x / mass;
            float aTy = fTotal.y / mass;
            //Debug.Log(fGravity.y);
            float aTz = fTotal.z / mass;
            //Debug.Log(aTx);
            //Debug.Log(aTy);
            //Debug.Log(aTz);
            //Debug.Log("Despues:" + fTotal.Module());
            //Debug.DrawLine(transform.position, transform.position + fDrag.Divide(mass), Color.blue);
            //Debug.DrawLine(transform.position, transform.position + fMagnus.Divide(mass), Color.red);
            //Debug.DrawLine(transform.position, transform.position + fP.Divide(mass), Color.yellow);
            //Debug.DrawLine(transform.position, transform.position + new Vector3(aTx, aTy, aTz), Color.magenta);
            //Debug.DrawLine(transform.position, transform.position + fGravity.Divide(mass), Color.magenta);
            //Debug.Log("Antes" + lVelocityInit.Module());
            lVelocityFin = lVelocityInit;
            //lVelocityInit = lVelocityFin.Add(a.Multiply(Time.deltaTime));
            lVelocityInit.x = lVelocityFin.x + aTx * Time.deltaTime;
            lVelocityInit.y = lVelocityFin.y + aTy * Time.deltaTime;
            lVelocityInit.z = lVelocityFin.z + aTz * Time.deltaTime;
            Debug.Log("Antes" + lVelocityFin.Module());
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z) + (new Vector3((lVelocityFin.x*Time.deltaTime), (lVelocityFin.y * Time.deltaTime), (lVelocityFin.z * Time.deltaTime)));
            Debug.Log("Despues" + lVelocityFin.Module());
            //Debug.Log("X :" + lVelocityInit.x);
            //Debug.Log("Y :" + lVelocityInit.y);
            //Debug.Log("Z :" + lVelocityInit.z);
            //Debug.Log("Despues" +lVelocityInit.Module());
        }         
    }
}