using UnityEngine;
using System.Collections;
using CustomButtons;

public class BackButton : MonoBehaviour {

    float buttonLeft = 0.88f;
    float buttonRight = 0.98f;
    float buttonUp = 0.34f;
    float buttonDown = 0.19f;

    Button backButton;

    // Use this for initialization
    void Start () {
	    backButton = new Button (buttonLeft, buttonRight, buttonDown, buttonUp);
    }
	
	// Update is called once per frame
	void Update () {
        if (backButton.IsButtonDown())
        {
            backButton.HandlePlayerMovement.RunBack();
        }
        else
        {
            backButton.HandlePlayerMovement.StopRunningBack();
        }
    }
}
