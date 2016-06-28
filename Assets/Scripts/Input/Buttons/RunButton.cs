using UnityEngine;
using System.Collections;
using CustomButtons;

public class RunButton : MonoBehaviour {

    float buttonLeft = 0.15f;
    float buttonRight = 0.25f;
    float buttonUp = 0.34f;
    float buttonDown = 0.19f;

    Button runButton;

    // Use this for initialization
    void Start () {
	    runButton = new Button (buttonLeft, buttonRight, buttonDown, buttonUp);
    }
	
	// Update is called once per frame
	void Update () {
	    if(runButton.IsButtonDown())
        {
            runButton.HandlePlayerMovement.RunForward();
        }
        else
        {
            runButton.HandlePlayerMovement.StopRunningForward();
        }
	}
}
