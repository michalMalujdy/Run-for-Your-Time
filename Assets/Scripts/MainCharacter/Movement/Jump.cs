using UnityEngine;
using System.Collections;

public class Jump : MonoBehaviour {

	float buttonLeft = Screen.width * 0.77f;
	float buttonRight = Screen.width * 0.87f;
	float buttonUp = Screen.height * 0.19f;
	float buttonDown = Screen.height * 0.03f;

	public Rigidbody2D mainCharacterRB;
	private ChainConnection mainCharacterChainConnection;
	public GroundCollider groundColliderComponent;
    private HandleAnimations characterAnimations;

	private bool isJumping = false;
	public float jumpVelocity = 1.5f;

    bool isTimerOn = false;
    float timePassed = 0.0f;
    float TimeOfJump = 1.2f;

	// Use this for initialization
	void Start () {
		mainCharacterChainConnection = mainCharacterRB.GetComponent<ChainConnection> ();
        characterAnimations = mainCharacterRB.GetComponent<HandleAnimations>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!mainCharacterChainConnection.IsCharacterAttachedToChain) {
			if (GetComponent<Buttons>().IsButtonOrKeyboardDown(buttonLeft, buttonRight, buttonDown, buttonUp, KeyCode.Space) && !isJumping) {
				mainCharacterRB.velocity = new Vector2 (mainCharacterRB.velocity.x, mainCharacterRB.velocity.y + jumpVelocity);
				IsJumping = true;
				groundColliderComponent.IsGrounded = false;

                isTimerOn = true;
			}
		}
		if (isJumping && groundColliderComponent.IsGrounded) {
			IsJumping = false;
		}

        if(isTimerOn)
        {
            if(timePassed >= TimeOfJump)
            {
                IsJumping = false;
            }

            timePassed += Time.deltaTime;
        }
	}

	public bool IsJumping {
		get {
			return isJumping;
		}
		set {
            if(isJumping != value)
            {
                isJumping = value;
                characterAnimations.setIsJumping(value);

                isTimerOn = value;
                if (!value)
                {
                    timePassed = 0.0f;
                }
            }
		}
	}
}
