using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SwingShoot : MonoBehaviour {

	float buttonLeft = Screen.width * 0.88f;
	float buttonRight = Screen.width * 0.98f;
	float buttonUp = Screen.height * 0.12f;
	float buttonDown = Screen.height * 0.03f;

	public Sprite shootSprite;
	public Sprite swingSprite;
	private ShootingMode mainCharacter;
    private Image imageComponent;

	int timesClicked = 0;
	bool clicked = false;
	bool previusState = false;

	void Awake(){
		mainCharacter = GameObject.FindWithTag ("Player").GetComponent<ShootingMode> ();
        imageComponent = GetComponent<Image>();
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
			if (timesClicked == 1) {
				mainCharacter.ShootMode = false;
				mainCharacter.SwingMode = true;
                imageComponent.sprite = swingSprite;
			} else {
				mainCharacter.ShootMode = true;
				mainCharacter.SwingMode = false;
                imageComponent.sprite = shootSprite;
				timesClicked = 0;
			}
			previusState = true;
		}

	}
}
