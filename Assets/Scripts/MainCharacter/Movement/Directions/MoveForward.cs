using UnityEngine;
using System.Collections;

public class MoveForward : MonoBehaviour {
    private float buttonLeft;
    private float buttonRight;
    private float buttonUp;
    private float buttonDown;
    public Run mainCharacter;
    private ChainConnection chainConnectionComponent;

	
	// Use this for initialization
	void Start () {
        chainConnectionComponent = mainCharacter.GetComponent<ChainConnection>();
        buttonLeft = Screen.width * 0.15f;
        buttonRight = Screen.width * 0.25f;
        buttonUp = Screen.height * 0.34f;
        buttonDown = Screen.height * 0.19f;
    }
	
	// Update is called once per frame
	void Update () {
        if (!chainConnectionComponent.IsCharacterAttachedToChain)
        {
            mainCharacter.IsRunningForeward = GetComponent<Buttons>().IsButtonOrKeyboardDown(buttonLeft, buttonRight, buttonDown, buttonUp, KeyCode.D);
        }
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

    public float ButtonRight
    {
        get
        {
            return buttonRight;
        }
    }
}
