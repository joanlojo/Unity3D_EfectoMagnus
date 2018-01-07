using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartLevel : MonoBehaviour {

    public Transform pelota;
    public string levelName;
    public Our_Vector3 velocityP, gravity, Welocity, tau;
    // Use this for initialization
    void Start () {
        velocityP = GameObject.Find("Pelota").GetComponent<BallPhysics>().lVelocityInit;
        gravity = GameObject.Find("Pelota").GetComponent<BallPhysics>().fGravity;
        Welocity = GameObject.Find("Pelota").GetComponent<BallPhysics>().wVelocity;
        tau = GameObject.Find("Pelota").GetComponent<BallPhysics>().fTau;
    }
	
	// Update is called once per frame
	void Update () {
        if (levelName == "Start")
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                SceneManager.LoadScene("Main");
            }
        }

      /*  if (levelName == "Main")
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene(levelName);
            }
        }*/

        if(pelota.transform.position.y < -2.1)
        {
            gravity.y = 0;
            Welocity.module = 0;
            Welocity.x = 0;
            Welocity.y = 0;
            Welocity.z = 0;
            tau.x = 0;
            tau.y = 0;
            tau.z = 0;
            velocityP.x = 0;
            velocityP.y = 0;
            velocityP.z = 0;
            //SceneManager.LoadScene("Start");
        }


        //LineRenderer lr = myLine.GetComponent<LineRenderer>();
        

    }
}
