using UnityEngine;
using System.Collections;

public class Tug : MonoBehaviour {

	private CharacterCollision collisionComponent;
	private bool isTuggingEnable = false;
	private Rigidbody2D rb;
	private float armRadious = 1.0f;
	private EnemyKilling enemyKillComponent;
	private ChainConnection chainConnectionComponent;

    public Buttons runButton;
    public Buttons backButton;

    // Use this for initialization
    void Start () {
		collisionComponent = GetComponent<CharacterCollision> ();
		rb = GetComponent<Rigidbody2D> ();
		enemyKillComponent = GetComponent <EnemyKilling> ();
		chainConnectionComponent = GetComponent<ChainConnection> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (isTuggingEnable) {
			CheckForKill ();
		}
	}

	public void StartTugging()
	{
		isTuggingEnable = true;
		rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        runButton.IsButtonActive = false;
        backButton.IsButtonActive = false;
    }
	public void StopTugging()
	{
		isTuggingEnable = false;
		rb.constraints &= ~RigidbodyConstraints2D.FreezePositionX;
        runButton.IsButtonActive = true;
        backButton.IsButtonActive = true;
    }

	private void CheckForKill()
	{
		Collider2D[] collidersInCircle = Physics2D.OverlapCircleAll (rb.position, armRadious);
		for (int i = 0; i < collidersInCircle.GetLength(0); i++) {
			if (collidersInCircle [i].tag == "Enemy") {
				enemyKillComponent.KillEnemyOnShortDistance (collidersInCircle[i].gameObject);
				chainConnectionComponent.Dismount ();
			}
		}
	}

	public bool IsTuggingEnable {
		get {
			return isTuggingEnable;
		}
		set {
			isTuggingEnable = value;
		}
	}
}
