using UnityEngine;
using System.Collections;

public class MoveUp : MonoBehaviour {

	float buttonLeft = Screen.width * 0.08f;
	float buttonRight = Screen.width * 0.18f;
	float buttonUp = Screen.height * 0.5f;
	float buttonDown = Screen.height * 0.35f;
	public MoveAlongRope mainCharacter;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		mainCharacter.IsRunningUp = GetComponent<Buttons>().isButtonDown(buttonLeft,buttonRight,buttonDown,buttonUp);
	}
}
