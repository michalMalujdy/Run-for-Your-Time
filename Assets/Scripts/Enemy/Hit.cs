using UnityEngine;
using System.Collections;

public class Hit : MonoBehaviour {

	private bool stop = false;
	private float stickInDistance = 0.2f;
	private ShootingMode mainCharacter;
	private bool isHit = false;
	private FixedJoint2D  fixedJoint;
	private EnemyKilling handleKilling;
	private Shoot arrowType;
	private Tug tugComponent;
    private bool hitBefore = false;

	void Awake(){
		mainCharacter = GameObject.FindWithTag("Player").GetComponent<ShootingMode>();
		fixedJoint = GetComponents<FixedJoint2D> ()[1];
		handleKilling = GameObject.FindWithTag ("Player").GetComponent<EnemyKilling>();
		arrowType = GetComponent<Shoot> ();
		tugComponent = mainCharacter.GetComponent <Tug> ();
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnCollisionEnter2D(Collision2D coll) {

        if (coll.gameObject.tag == "Enemy") {
			if (arrowType.ArrowType == "Shoot") {
				handleKilling.KillEnemyOnLongDistance (coll);
				Destroy (this.gameObject);
			} 
			else if (arrowType.ArrowType == "Swing") {
				HookEnemy (coll);
			} 
		}
		else if (coll.gameObject.tag == "Terrain") {			
			
			CollisionWithTerrain ();
			if (mainCharacter.SwingMode) {
				GetComponent<RealTimeRopeCreating> ().StartMakingChain = true;
			}
            GetComponent<Rigidbody2D>().isKinematic = true;//sprawia że po uderzeniu ustalona rotacja i translacja strzaly sie nie zmienia
        }       
        isHit = true;
    }

    void CollisionWithTerrain(){
		GetComponent<Rigidbody2D> ().velocity = new Vector2 (0.0f, 0.0f);
		GetComponent<Rigidbody2D> ().angularVelocity = 0.0f;

		//transform.localRotation = Quaternion.Euler(0.0f,0.0f,GetComponent<Shoot>().Alpha);
		Vector2 velocity = GetComponent<Shoot>().velocity;
		
		Vector2 shortenVelocity = Vectors.ShortenVector(velocity, stickInDistance);
		transform.localPosition = new Vector2(transform.localPosition.x + shortenVelocity.x,transform.localPosition.y + shortenVelocity.y);
		GetComponent<BoxCollider2D> ().enabled = false;
	}

    

	void HookEnemy(Collision2D coll)
	{
		fixedJoint.enabled = true;
		fixedJoint.connectedBody = coll.rigidbody;
		fixedJoint.anchor = new Vector2 (0.7f, 0.0f);
		fixedJoint.connectedAnchor = new Vector2 (0.0f, 0.0f);

		GetComponent<RealTimeRopeCreating> ().StartMakingChain = true;
		GetComponent<BoxCollider2D> ().enabled = false;
		tugComponent.StartTugging ();
	}

    void IgnoreCollisionByTag(string tag, Collision2D coll)
    {
        if (coll.gameObject.tag == tag)
        {
            Physics2D.IgnoreCollision(coll.collider, GetComponent<Collider2D>());
        }
    }

    public bool Stop {
		get {
			return stop;
		}
	}

	public bool IsHit {
		get {
			return isHit;
		}
		set {
			isHit = value;
		}
	}
}
