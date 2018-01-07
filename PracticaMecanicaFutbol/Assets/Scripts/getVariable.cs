using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getVariable : MonoBehaviour {

    public int VariableToGet;

    public GameObject Pelota;

    private BallPhysics scriptToAccess;

    private Our_Vector3 LINE_lVelocityFin = new Our_Vector3(0, 0, 0);
    private Our_Vector3 LINE_fMagnus = new Our_Vector3(0, 0, 0);
    private Our_Vector3 LINE_fDrag = new Our_Vector3(0, 0, 0);
    private Our_Vector3 LINE_fTau = new Our_Vector3(0, 0, 0);

    // Use this for initialization
    void Start () {

        scriptToAccess = Pelota.GetComponent<BallPhysics>();

        if (VariableToGet == 0) {
            LINE_lVelocityFin = scriptToAccess.lVelocityFin;
            transform.position = new Vector3(LINE_lVelocityFin.x, LINE_lVelocityFin.y, LINE_lVelocityFin.z);
        }

        if (VariableToGet == 1)
        {
            LINE_lVelocityFin = scriptToAccess.fMagnus;
            transform.position = new Vector3(LINE_fMagnus.x, LINE_fMagnus.y, LINE_fMagnus.z);
        }

        if (VariableToGet == 2)
        {
            LINE_fDrag = scriptToAccess.fDrag;
            transform.position = new Vector3(LINE_fDrag.x, LINE_fDrag.y, LINE_fDrag.z);
        }
        if (VariableToGet == 3)
        {
            LINE_fTau = scriptToAccess.fTau;
            transform.position = new Vector3(LINE_fTau.x, LINE_fTau.y, LINE_fTau.z);
        }

    }
	
	// Update is called once per frame
	void Update () {
        if (VariableToGet == 0)
        {
            LINE_lVelocityFin = scriptToAccess.lVelocityFin;
            transform.position = new Vector3(LINE_lVelocityFin.x, LINE_lVelocityFin.y, LINE_lVelocityFin.z);
        }

        if (VariableToGet == 1)
        {
            LINE_fMagnus = scriptToAccess.fMagnus;
            transform.position = new Vector3(LINE_fMagnus.x, LINE_fMagnus.y, LINE_fMagnus.z);
        }

        if (VariableToGet == 2)
        {
            LINE_fDrag = scriptToAccess.fDrag;
            transform.position = new Vector3(LINE_fDrag.x, LINE_fDrag.y, LINE_fDrag.z);
        }
        if (VariableToGet == 3)
        {
            LINE_fTau = scriptToAccess.fTau;
            transform.position = new Vector3(LINE_fTau.x, LINE_fTau.y, LINE_fTau.z);
        }
    }
}
