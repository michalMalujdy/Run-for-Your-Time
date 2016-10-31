using UnityEngine;
using System.Collections;

public class MoveUp : MonoBehaviour {

	float buttonLeft = Screen.width * 0.08f;
	float buttonRight = Screen.width * 0.16f;
	float buttonUp = Screen.height * 0.45f;
	float buttonDown = Screen.height * 0.30f;
	public MoveAlongRope mainCharacter;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		mainCharacter.IsRunningUp = GetComponent<Buttons>().IsButtonOrKeyboardDown(buttonLeft, buttonRight, buttonDown, buttonUp, KeyCode.W);
    }
}
