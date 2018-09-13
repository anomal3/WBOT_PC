using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnDemo : MonoBehaviour {

    
    

   
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyUp(KeyCode.F1))
        {
            SceneManager.LoadSceneAsync("Apocalipt");
        }
        if (Input.GetKeyUp(KeyCode.F2))
        {
            SceneManager.LoadSceneAsync("Apocalipt2");
        }
        if (Input.GetKeyUp(KeyCode.F3))
        {
            SceneManager.LoadSceneAsync("Apocalipt3");
        }


    }
}
