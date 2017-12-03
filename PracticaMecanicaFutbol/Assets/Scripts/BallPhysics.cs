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
    float radius ;

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

    Quaternion q;

    float Area() {
        return (radius * radius) * PI;
    }

    void Start()
    {
        getKickPosition = GameObject.Find("ScriptsObject").GetComponent<GetKickPosition>().fromBallCoordinates; //Punto de impacto a la pelota respecto a su centro
        
    }

    void startKick()
    {
        float area = Area();
        airDensity = 1.23f;
        Cd = 0.25f;
        Cm = 0.25f;
        mass = 0.396f; //0.396f
        radius = 0.279f; //0.279f
        dt = 0.01f;
        fGravity = new Our_Vector3(0, 0, -mass * gravity);
        fMagnus = new Our_Vector3(0, 0, 0);
        fDrag = new Our_Vector3(0, 0, 0);
        fTau = new Our_Vector3(0, 0, 0);

        barra.value *= 50;
       // puntoDeImpacto.x = VectorDireccion.transform.position.x - transform.position.x;//
       // puntoDeImpacto.y = VectorDireccion.transform.position.y - transform.position.y;//PUNTO DONDE SE APLICA FP, ESTA RESPECTO AL CENTRO DE LA PELOTA
       // puntoDeImpacto.z = VectorDireccion.transform.position.z - transform.position.z;//

        rad.x = getKickPosition.x - transform.position.x;// 
        rad.y = getKickPosition.y - transform.position.y;// VECTOR ENTRE EL CENTRO DE LA PELOTA Y EL PUNTO DE IMPACTO
        rad.z = getKickPosition.z - transform.position.z;//

        //fP.module = barra.value;// FUERZA TOTAL DE FP
       
        dirfP.x = (getKickPosition.x - VectorDireccion.position.x);//
        dirfP.y = (getKickPosition.y - VectorDireccion.position.y);//ESTO TE DA LA DIRECCION DEL VECTOR, PERO NO FP
        dirfP.z = (getKickPosition.z - VectorDireccion.position.z);//

        //FP EN SUS COMPONENTES 
        dirfP.Normalize();
        fP.x = dirfP.x * barra.value;
        fP.y = dirfP.y * barra.value;
        fP.z = dirfP.z * barra.value;
       

       inertiaMoment = (0.667f) * mass * (radius * radius);
        Kd = (1 / 2) * airDensity * Cd * area;
        Km = (1 / 2) * airDensity * Cm * area;

        //Calcular direccion Tau
        fTau = rad.CrossProduct(fP);
        //Calcular wVelocity (Inicial)
        wVelocity.x = fTau.x / inertiaMoment * dt;
        wVelocity.y = fTau.y / inertiaMoment * dt;
        wVelocity.z = fTau.z / inertiaMoment * dt;
        //Calcular lVelocity (Inicial)
        lVelocityInit.x = (fP.x * dt) / mass;
        lVelocityInit.y = (fP.y * dt) / mass;
        lVelocityInit.z = (fP.z * dt) / mass;
        //POR UN LADO HAY Q DARLE UNA VELOCIDAD ANGULAR A LA PELOTA --> WVELOCITY, ES SIEMPRE LA MISMA CREO*

        //transform.rotation.Set(fTau.x*wVelocity.x, fTau.y * wVelocity.y, fTau.z * wVelocity.z, 1);


        Debug.Log(wVelocity.Module());

        Our_Quaternion rotacionPelota = new Our_Quaternion(fTau, 2);
        

        


        q = new Quaternion(rotacionPelota.x, rotacionPelota.y, rotacionPelota.z, rotacionPelota.w);
        transform.rotation = q;
        //POR OTRO LADO HAY Q DARLE EL EJE DE ROTACION --> FTAU Q ES PARA TODA LA EJECUCION EL MISMO
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
           startKick();
            startKicked = true;
        }

        if (startKicked == true){
            transform.Rotate(new Vector3(fTau.x, fTau.y,fTau.z),2.0f);
            //transform.rotation = q;
            //Debug.DrawLine(new Vector3(transform.position.x, transform.position.y, transform.position.z), new Vector3(dirfP.x,dirfP.y,dirfP.z),Color.black);
            Debug.DrawRay(transform.position, new Vector3(fTau.x, fTau.y, fTau.z), Color.black);
        }
        /*    //EL PRIMER FRAME UTILIZA LA VELOCIDAD INICIAL, A PARTIR DE AHI SE DEBE ACTUALIZAR
            //Calcular fDrag
            fDrag.x = -Kd * lVelocityInit.Module() * lVelocityFin.x;
            fDrag.y = -Kd * lVelocityInit.Module() * lVelocityFin.y;
            fDrag.z = -Kd * lVelocityInit.Module() * lVelocityFin.z;

            //Calcular fMagnus
            wVelocity.Normalize();
            fMagnus.x = Km * lVelocityInit.Module() * (wVelocity.y * lVelocityInit.z - lVelocityInit.y * wVelocity.z);
            fMagnus.y = Km * lVelocityInit.Module() * (wVelocity.x * lVelocityInit.z - lVelocityInit.x * wVelocity.z);
            fMagnus.z = Km * lVelocityInit.Module() * (wVelocity.x * lVelocityInit.y - lVelocityInit.x * wVelocity.y);

            //Agrupar fTotal
            fTotal.x = fDrag.x + fMagnus.x;//
            fTotal.y = fDrag.y + fMagnus.y;//CREO QUE NO HACE FALTA
            fTotal.z = fDrag.z + fMagnus.z + fGravity.z;//

            //ACTUALIZAR lVelocityInit A PARTIR DE LAS NUEVAS FORUMLAS CON EL METODO DE EULER
            //vanterior = lVelocityFin
            //act v anterior
            //modificar lVelocityFin = vanterior

            //CALCULAR DERIVADA DE POSICION
            //MODIFICAR LA POSITION DE LA PELOTA*/
    }
}