using UnityEngine;
using System.Collections;

public class Buttons : MonoBehaviour {

	bool clickedDown = false;
	bool clicked = false;
	int ID;
	bool isButtonActive = true;
    
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

	public bool isButtonDown(float buttonLeft, float buttonRight, float buttonDown, float buttonUp){
		if (gameObject.active && isButtonActive) {
			int fingersRunning = 0;

			foreach (Touch touch in Input.touches) {
				if (touch.position.x <= buttonRight && touch.position.x >= buttonLeft &&
				    touch.position.y >= buttonDown && touch.position.y <= buttonUp) {
					fingersRunning++;
				}
			}
			if (fingersRunning > 0) {
				return true;
			} else {
				return false;
			}
		} else {
			return false;
		}
	}

	public bool isButtonClicked(float buttonLeft, float buttonRight, float buttonDown, float buttonUp){
		
		if (gameObject.active && isButtonActive) {	
			foreach (Touch touch in Input.touches) {
				if (!clickedDown && touch.phase == TouchPhase.Began &&
				   touch.position.x <= buttonRight && touch.position.x >= buttonLeft &&
				   touch.position.y >= buttonDown && touch.position.y <= buttonUp) {
					clickedDown = true;
					ID = touch.fingerId; 
				}

				if (clickedDown && (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled) && touch.fingerId == ID &&
				   touch.position.x <= buttonRight && touch.position.x >= buttonLeft &&
				   touch.position.y >= buttonDown && touch.position.y <= buttonUp) {
					clicked = true;
					clickedDown = false;
				} else {
					clicked = false;
				}
			}
			return clicked;
		}
		else {
            clicked = false;
            clickedDown = false;
            return false;
		}
	}

    public bool IsButtonOrKeyboardDown(float buttonLeft, float buttonRight, float buttonDown, float buttonUp, KeyCode key)
    {
        if(isButtonActive)
        {
            bool result = GetComponent<Buttons>().isButtonDown(buttonLeft, buttonRight, buttonDown, buttonUp);
            //#if UNITY_EDITOR
            if (result != true)
                result = Input.GetKey(key);
            //#endif
            return result;
        }
        else
        {
            return false;
        }
    }

    public bool IsButtonActive {
		get {
			return isButtonActive;
		}
		set {
			isButtonActive = value;
		}
	}
}
