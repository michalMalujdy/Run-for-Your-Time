using UnityEngine;
using System.Collections;

public class GroundCollider : MonoBehaviour {

	private Jump jumpComponent;
	public Rigidbody2D maincharacter;
	private bool isGrounded;
	private Transform transformComponent;
	private float radious = 0.2f;
	private Collider2D groundPointCollider;

	void Awake()
	{
		transformComponent = GetComponent <Transform> ();
		groundPointCollider = GetComponent <Collider2D> ();
	}

	// Use this for initialization
	void Start () {
		if (maincharacter.velocity.y == 0.0f) {
			isGrounded = true;
		} 
		else {
			isGrounded = false;
		}

	}
	
	// Update is called once per frame
	void Update () {
		CheckForColliders ();
	}
	void CheckForColliders()
	{
		Collider2D[] collidersInCircle = Physics2D.OverlapCircleAll (transform.position, radious);
		for (int i = 0; i < collidersInCircle.GetLength (0); i++) {
			if (collidersInCircle [i].tag != "Player") {
				if (collidersInCircle.GetLength (0) != 0) {
					isGrounded = true;
				}
			}
		}

	}

	void OnCollisionEnter2D(Collision2D coll) {
		//isGrounded = true;

	}

	public bool IsGrounded {
		get {
			return isGrounded;
		}
		set {
			isGrounded = value;
		}
	}
}
