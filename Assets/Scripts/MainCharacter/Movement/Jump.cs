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

	private bool isJumping = false;
	public float jumpVelocity = 1.5f;

	// Use this for initialization
	void Start () {
		mainCharacterChainConnection = mainCharacterRB.GetComponent<ChainConnection> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!mainCharacterChainConnection.IsCharacterAttachedToChain) {
			if (GetComponent<Buttons>().IsButtonOrKeyboardDown(buttonLeft, buttonRight, buttonDown, buttonUp, KeyCode.Space) && !isJumping) {
				mainCharacterRB.velocity = new Vector2 (mainCharacterRB.velocity.x, mainCharacterRB.velocity.y + jumpVelocity);
				isJumping = true;
				groundColliderComponent.IsGrounded = false;
			}
		}
		if (isJumping && groundColliderComponent.IsGrounded) {
			isJumping = false;
		}
	}

	public bool IsJumping {
		get {
			return isJumping;
		}
		set {
			isJumping = value;
		}
	}
}
