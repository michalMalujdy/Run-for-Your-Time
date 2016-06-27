using UnityEngine;
using System.Collections;

public class MoveForward : MonoBehaviour {
	float buttonLeft = Screen.width * 0.15f;
	float buttonRight = Screen.width * 0.25f;
	float buttonUp = Screen.height * 0.34f;
	float buttonDown = Screen.height * 0.19f;
	public Run mainCharacter;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		mainCharacter.IsRunningForeward = GetComponent<Buttons>().IsButtonOrKeyboardDown(buttonLeft, buttonRight, buttonDown, buttonUp, KeyCode.D);
    }

	public float ButtonLeft {
		get {
			return buttonLeft;
		}
	}

	public float ButtonUp {
		get {
			return buttonUp;
		}
	}

	public float ButtonDown {
		get {
			return buttonDown;
		}
	}
}
