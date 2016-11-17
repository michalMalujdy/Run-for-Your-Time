using UnityEngine;
using System.Collections;

public class PauseText : MonoBehaviour {

    bool isPaused = false;
    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetKeyDown(KeyCode.P))
        {
            isPaused = !isPaused;
            if(isPaused)
            {
                Time.timeScale = 0.0f;
            }
            else
            {
                Time.timeScale = 1.0f;
            }
        }
	}
}
