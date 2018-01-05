using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartLevel : MonoBehaviour {

    public GameObject pelota;
    public string levelName;
   // GameObject myLine = new GameObject();
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (levelName == "Start")
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(levelName);
            }
        }

        if (levelName == "Main")
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene(levelName);
            }
        }

        if(pelota.transform.position.y < -2.84)
        {
            SceneManager.LoadScene("Start");
        }


        //LineRenderer lr = myLine.GetComponent<LineRenderer>();
        

    }
}
