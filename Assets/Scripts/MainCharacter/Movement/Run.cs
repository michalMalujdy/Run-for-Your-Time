using UnityEngine;
using System.Collections;

public class Run : MonoBehaviour {

	private bool isRunningForeward = false;
	private bool isRunningBack = false;
	public int velocity = 6;
	//private bool isCharacterAttachedToChain = false;
	private float slowDownSpeed = 0.25f;
	//Kierunek biegu w poprzedniej klatce. Używane do wyhamowywania: 1 w prawo, -1 w lewo, 0 w miejscu
	private int previousDirection = 0;
 
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (isRunningForeward) {
			runForeward ();
			previousDirection = 1;
		} 
		else if (isRunningBack) {
			runBack ();
			previousDirection = -1;
		} 
		else {
			if (!GetComponent<ChainConnection>().IsCharacterAttachedToChain) {
				slowDown ();
			}
		}
	}

	void runForeward(){
		GetComponent<Rigidbody2D>().velocity = new Vector2(velocity, GetComponent<Rigidbody2D>().velocity.y);
	}
	void runBack(){
		GetComponent<Rigidbody2D>().velocity = new Vector2(-velocity, GetComponent<Rigidbody2D>().velocity.y);
	}
	void slowDown()
	{
		if (Mathf.Abs (GetComponent<Rigidbody2D> ().velocity.x) > 0) {
			if (previousDirection == 1) {
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (GetComponent<Rigidbody2D> ().velocity.x - slowDownSpeed, GetComponent<Rigidbody2D> ().velocity.y);

				if (GetComponent<Rigidbody2D> ().velocity.x < 0) {
					GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, GetComponent<Rigidbody2D> ().velocity.y);
					previousDirection = 0;
				}
			}
			else if (previousDirection == -1) {
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (GetComponent<Rigidbody2D> ().velocity.x + slowDownSpeed, GetComponent<Rigidbody2D> ().velocity.y);

				if (GetComponent<Rigidbody2D> ().velocity.x > 0) {
					GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, GetComponent<Rigidbody2D> ().velocity.y);
					previousDirection = 0;
				}
			}

			if (GetComponent<Rigidbody2D> ().velocity.x == 0) {
				previousDirection = 0;
			}
		}
	}

    public void StopRunning()
    {
        isRunningForeward = false;
        isRunningBack = false;
    }

	public bool IsRunningForeward {
		get {
			return isRunningForeward;
		}
		set {
			isRunningForeward = value;
		}
	}

	public bool IsRunningBack {
		get {
			return isRunningBack;
		}
		set {
			isRunningBack = value;
		}
	}

	public int PreviousDirection {
		get {
			return previousDirection;
		}
	}
}
