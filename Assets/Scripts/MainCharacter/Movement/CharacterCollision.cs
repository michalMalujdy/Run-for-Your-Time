using UnityEngine;
using System.Collections;

public class CharacterCollision : MonoBehaviour {


	public Buttons runButton;
	public Buttons backButton;
	private Run runDirection;

	private float buttonInactiveTime = 1.0f;
	public float xVelocity;
	public float yVelocity;

	public float xVelocityAttached;
	public float yVelocityAttached;

	private Vector2 velocityVector;

	private Rigidbody2D rb;

	bool makeJumpOff = false;

	private float groundedVeloctiyMarginY = 0.1f;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		runDirection = GetComponent<Run> ();
	}

	//funkcja CalculateJumpOff() wykonuje sie w chwili kolizji, bo wektor odbicia sie potrzebujemy wyliczyc natychmiast, w momencie uderzenia, zas funkcja JumpOff() wykonuje się
	// w LateUpdate(), bo potrzebujemy skorygowac predkosc PO wykonaniu sie wszystkich obliczen fizycznych Unity i nadaniu predkosci przez przyciski. Jak wykonywalem te
	// funkcje w chwili uderzenia, czyli OnCollisionEnter2D(), wtedy gdy CalculateJumpOff(), to obliczenia Unity i przyciski biegniecia strasznie mieszaly i efekt byl mierny
	void LateUpdate()
	{
		if (makeJumpOff) {
			JumpOff ();
		}
	}

	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.tag == "Enemy")
		{
			GetComponent<Stats> ().AddSubstractCurrentHealth (-15.0f);
			CalculateJumpOff ();
			makeJumpOff = true;
		}
	}

	void JumpOff()// postac ma odskoczyc w momencie uderzenia z przeciwnikiem, tracac przy tym zdrowie
	{
		rb.velocity = velocityVector;

		makeJumpOff = false;

		runButton.IsButtonActive = false;
		backButton.IsButtonActive = false;

		StartCoroutine (WaitToActivateButton ());
	}

	private void CalculateJumpOff()
	{
		if (!GetComponent<ChainConnection> ().IsCharacterAttachedToChain) {
			if (rb.velocity.x < -0.2f || rb.velocity.x > 0.2f) {
				velocityVector.x = -(Mathf.Sign (rb.velocity.x)) * xVelocity;
			}
			else {
				velocityVector.x = xVelocity;
			}

			if (rb.velocity.y < -0.2f || rb.velocity.y > 0.2f) {
				velocityVector.y = -(Mathf.Sign (rb.velocity.y)) * yVelocity;
			}
			else {
				velocityVector.y = yVelocity;
			}
		}			
		else {
			velocityVector.x = - (Mathf.Sign (rb.velocity.x)) * xVelocityAttached;
			velocityVector.y = - (Mathf.Sign (rb.velocity.y)) * yVelocityAttached;
		}

		//Tutaj dodaje lub odejmuje x predkosc po skladowej x w zaleznosci czy w tej klatce w momencie uderzenia trzymany byl ktorys
		// z przyciskow run lub back, bo oznacza to, że w tej klatce dodano lub odjęto dodatkową prędkość
		if (runDirection.IsRunningForeward) {
			velocityVector.x -= 6.0f;
		}
		if (runDirection.IsRunningBack) {
			velocityVector.x += 6.0f;
		}
	}

	IEnumerator WaitToActivateButton()
	{
		yield return new WaitForSeconds (buttonInactiveTime);
		runButton.IsButtonActive = true;	
		backButton.IsButtonActive = true;
	}

	public bool isCharacterGrounded()
	{
		if (rb.velocity.y > -groundedVeloctiyMarginY && rb.velocity.y < groundedVeloctiyMarginY) {
			return true;
		}
		else {
			return false;
		}
	}


}
