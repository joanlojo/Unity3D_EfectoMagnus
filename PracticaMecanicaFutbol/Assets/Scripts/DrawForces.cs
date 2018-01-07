using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawForces : MonoBehaviour {
    public GameObject magnus;
    public GameObject drag;
    public GameObject gravity;
    public GameObject tau;
    public GameObject velocity;
    public GameObject MainCamera;
    public GameObject CameraGol;
    // Use this for initialization
    void Start () {
        MainCamera.SetActive(true);
        CameraGol.SetActive(false);
        magnus.SetActive(false);
        gravity.SetActive(false);
        drag.SetActive(false);
        velocity.SetActive(false);
        tau.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.R))
        {
            magnus.SetActive(true);
            gravity.SetActive(true);
            drag.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            velocity.SetActive(true);
            tau.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            MainCamera.SetActive(false);
            CameraGol.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            MainCamera.SetActive(true);
            CameraGol.SetActive(false);
        }


    }
}
