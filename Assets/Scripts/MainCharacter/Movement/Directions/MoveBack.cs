using UnityEngine;
using System.Collections;

public class MoveBack : MonoBehaviour {
	float buttonLeft = Screen.width * 0.02f;
	float buttonRight = Screen.width * 0.12f;
	float buttonUp = Screen.height * 0.34f;
	float buttonDown = Screen.height * 0.19f;
	public Run mainCharacter;
    private ChainConnection chainConnectionComponent;

   

    // Use this for initialization
    void Start () {
        chainConnectionComponent = mainCharacter.GetComponent<ChainConnection>();
    }
	
	// Update is called once per frame
	void Update () {
        if (!chainConnectionComponent.IsCharacterAttachedToChain)
        {
            mainCharacter.IsRunningBack = GetComponent<Buttons>().IsButtonOrKeyboardDown(buttonLeft, buttonRight, buttonDown, buttonUp, KeyCode.A);
        }
    }

    public float ButtonLeft
    {
        get
        {
            return buttonLeft;
        }
    }

    public float ButtonRight
    {
        get
        {
            return buttonRight;
        }
    }

    public float ButtonUp
    {
        get
        {
            return buttonUp;
        }
    }

    public float ButtonDown
    {
        get
        {
            return buttonDown;
        }
    }
}
