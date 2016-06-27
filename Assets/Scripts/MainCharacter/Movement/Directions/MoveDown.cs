using UnityEngine;
using System.Collections;

public class MoveDown : MonoBehaviour {

	float buttonLeft = Screen.width * 0.08f;
	float buttonRight = Screen.width * 0.18f;
	float buttonUp = Screen.height * 0.17f;
	float buttonDown = Screen.height * 0.02f;
	public MoveAlongRope mainCharacter;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		mainCharacter.IsRunningDown = GetComponent<Buttons>().IsButtonOrKeyboardDown(buttonLeft,buttonRight,buttonDown,buttonUp, KeyCode.S);
	}


}
