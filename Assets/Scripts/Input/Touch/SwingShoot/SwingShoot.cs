using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SwingShoot : MonoBehaviour {

	float buttonLeft = Screen.width * 0.88f;
	float buttonRight = Screen.width * 0.98f;
	float buttonUp = Screen.height * 0.12f;
	float buttonDown = Screen.height * 0.03f;

	private Text shootText;
	private Text swingText;
	private ShootingMode mainCharacter;

	int timesClicked = 0;
	bool clicked = false;
	bool previusState = false;

	void Awake(){
		shootText = GameObject.Find ("ShootText").GetComponent<Text> ();
		swingText = GameObject.Find ("SwingText").GetComponent<Text> ();
		mainCharacter = GameObject.FindWithTag ("Player").GetComponent<ShootingMode> ();
		swingText.enabled = true;
		shootText.enabled = false;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (GetComponent<Buttons>().IsButtonOrKeyboardDown (buttonLeft, buttonRight, buttonDown, buttonUp, KeyCode.LeftShift)) {
			clicked = true;
		}
		else {
			clicked = false;
			previusState = false;
		}
		if (clicked && previusState != true) {
			timesClicked++;
			//Debug.Log ("clicked");
			if (timesClicked == 1) {
				mainCharacter.ShootMode = false;
				mainCharacter.SwingMode = true;
				swingText.enabled = false;
				shootText.enabled = true;
			} else {
				mainCharacter.ShootMode = true;
				mainCharacter.SwingMode = false;
				swingText.enabled = true;
				shootText.enabled = false;
				timesClicked = 0;
			}
			previusState = true;
		}

	}
}
