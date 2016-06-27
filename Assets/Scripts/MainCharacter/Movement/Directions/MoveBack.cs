using UnityEngine;
using System.Collections;

public class MoveBack : MonoBehaviour {
	float buttonLeft = Screen.width * 0.02f;
	float buttonRight = Screen.width * 0.12f;
	float buttonUp = Screen.height * 0.34f;
	float buttonDown = Screen.height * 0.19f;
	public Run mainCharacter;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		mainCharacter.IsRunningBack = GetComponent<Buttons>().IsButtonOrKeyboardDown(buttonLeft, buttonRight, buttonDown, buttonUp, KeyCode.A);
    }
}
