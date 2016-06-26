using UnityEngine;
using System.Collections;

public class AngleDuringShoot : MonoBehaviour {

	private Rigidbody2D mainCharacter;
	private Vector2 velocity;
	private float alpha;

	private float lastXposition;
	private float currentXposition;
	private float dx;

	private float lastYposition;
	private float currentYposition;
	private float dy;
	private float previousDy;
	private float maxDiffrence = 0.2f;

	void Awake(){
		mainCharacter = GameObject.FindGameObjectWithTag ("Player").GetComponent<Rigidbody2D> ();
	}

	// Use this for initialization
	void Start () {
		currentXposition = transform.localPosition.x;
		lastXposition = transform.localPosition.x; 

		currentYposition = transform.localPosition.y;
		lastYposition = transform.localPosition.y;
		dy = currentYposition - lastYposition;
		previousDy = currentYposition - lastYposition;
	}
	
	// Update is called once per frame
	void Update () {
		CalculateVelocity ();
		//velocity = new Vector2(GetComponent<Shoot>().speed, GetComponent<Rigidbody2D> ().velocity.y);
		CalculateAngle ();
		transform.localRotation = Quaternion.Euler (0.0f, 0.0f, alpha);
	}

	void CalculateAngle(){
		if (velocity.magnitude != 0.0f) {
			alpha = Mathf.Asin (velocity.y / velocity.magnitude) * 180.0f / Mathf.PI;
		}
		else {
			alpha = 0.0f;
		}
		Debug.Log (dx+","+dy);
		//Debug.Log (velocity);
	}

	void CalculateVelocity(){
		currentXposition = transform.localPosition.x;
		currentYposition = transform.localPosition.y;

		dx = currentXposition - lastXposition;
		dy = currentYposition - lastYposition;

		if (dy - previousDy > maxDiffrence) {
			dy = previousDy;
			Debug.Log(previousDy);
		}

		velocity = new Vector2 (dx, dy);

		lastXposition = currentXposition;
		lastYposition = currentYposition;
		previousDy = dy;
	}
}
