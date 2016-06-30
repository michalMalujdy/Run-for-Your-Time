using UnityEngine;
using System.Collections;

public class SwingOnChain : MonoBehaviour {

    private float xForce = 8000.0f;
    private float velocityMin = 0.25f;

    private ChainConnection chainConnectionComponent;
    private Tug tugComponent;
    private Rigidbody2D rigidbodyComponent;
    private GroundCollider groundColliderComponent;

    //FORWARD BUTTON
    public Buttons swingForwardButton;
    float buttonLeftF;
    float buttonRightF;
    float buttonUpF;
    float buttonDownF;

    //BACK BUTTON
    public Buttons swingBackButton;
    float buttonLeftB;
    float buttonRightB;
    float buttonUpB;
    float buttonDownB;

    // Use this for initialization
    void Start () {
        chainConnectionComponent = GetComponent<ChainConnection>();
        tugComponent = GetComponent<Tug>();
        rigidbodyComponent = GetComponent<Rigidbody2D>();
        groundColliderComponent = GetComponentInChildren<GroundCollider>();

        buttonLeftF = swingForwardButton.GetComponent<MoveForward>().ButtonLeft;
        buttonRightF = swingForwardButton.GetComponent<MoveForward>().ButtonRight;
        buttonUpF = swingForwardButton.GetComponent<MoveForward>().ButtonUp;
        buttonDownF = swingForwardButton.GetComponent<MoveForward>().ButtonDown;

        buttonLeftB = swingBackButton.GetComponent<MoveBack>().ButtonLeft;
        buttonRightB = swingBackButton.GetComponent<MoveBack>().ButtonRight;
        buttonUpB = swingBackButton.GetComponent<MoveBack>().ButtonUp;
        buttonDownB = swingBackButton.GetComponent<MoveBack>().ButtonDown;
    }
	
	// Update is called once per frame
	void Update () {
	    if(chainConnectionComponent.IsCharacterAttachedToChain && !tugComponent.IsTuggingEnable && !groundColliderComponent.IsGrounded)
        {

            if (IsSwingingForward())
            {
                if(IsForwardButtonDown())
                {
                    rigidbodyComponent.AddForce(new Vector2(xForce, 0.0f));
                }
            }
                
            else if (IsSwingingBack())
            {
                if(IsBackButtonDown())
                {
                    rigidbodyComponent.AddForce(new Vector2(-xForce, 0.0f));
                }
            }
            else
            {
                if (IsForwardButtonDown())
                {
                    rigidbodyComponent.AddForce(new Vector2(xForce, 0.0f));
                }

                if (IsBackButtonDown())
                {
                    rigidbodyComponent.AddForce(new Vector2(-xForce, 0.0f));
                }
            }
        }
	}

    bool IsForwardButtonDown()
    {
        return swingForwardButton.IsButtonOrKeyboardDown(buttonLeftF, buttonRightF, buttonDownF, buttonUpF, KeyCode.D);
    }

    bool IsBackButtonDown()
    {
        return swingBackButton.IsButtonOrKeyboardDown(buttonLeftB, buttonRightB, buttonDownB, buttonUpB, KeyCode.A);
    }

    bool IsSwingingForward()
    {
        if(rigidbodyComponent.velocity.x > velocityMin)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    bool IsSwingingBack()
    {
        if (rigidbodyComponent.velocity.x < -velocityMin)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
