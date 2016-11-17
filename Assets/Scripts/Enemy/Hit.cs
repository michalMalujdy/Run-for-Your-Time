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
    public bool IgnoreOnCollisionEnter { get; set; }
    private GameObject hitGameObject;

	void Awake(){
		mainCharacter = GameObject.FindWithTag("Player").GetComponent<ShootingMode>();
		fixedJoint = GetComponent<FixedJoint2D> ();
		handleKilling = GameObject.FindWithTag ("Player").GetComponent<EnemyKilling>();
		arrowType = GetComponent<Shoot> ();
		tugComponent = mainCharacter.GetComponent <Tug> ();
        IgnoreOnCollisionEnter = false;
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnCollisionEnter2D(Collision2D coll) {

        if (!IgnoreOnCollisionEnter)
        {

            if (coll.gameObject.tag == "Enemy" && !isHit)
            {
                if (arrowType.ArrowType == "Shoot")
                {
                    handleKilling.KillEnemyOnLongDistance(coll);
                    Destroy(this.gameObject);
                }
                else if (arrowType.ArrowType == "Swing")
                {
                    HookEnemy(coll);
                }
            }
            else if (coll.gameObject.tag == "Terrain" && !isHit)
            {

                CollisionWithTerrain();
                if(coll.gameObject.GetComponent<CharacterClimbUp>())
                {
                    coll.gameObject.GetComponent<CharacterClimbUp>().setArrowStuckIn(true);
                }
                if (mainCharacter.SwingMode)
                {
                    GetComponent<RunTimeRopeCreator>().enabled = true;
                }
                GetComponent<Rigidbody2D>().isKinematic = true;//sprawia że po uderzeniu ustalona rotacja i translacja strzaly sie nie zmienia
            }
            isHit = true;
            hitGameObject = coll.gameObject;
        }
    }

    void CollisionWithTerrain(){
		GetComponent<Rigidbody2D> ().velocity = new Vector2 (0.0f, 0.0f);
		GetComponent<Rigidbody2D> ().angularVelocity = 0.0f;
        GetComponent<SpriteRenderer>().sortingLayerName = "ArrowStuck";

		//transform.localRotation = Quaternion.Euler(0.0f,0.0f,GetComponent<Shoot>().Alpha);
		Vector2 velocity = GetComponent<Shoot>().velocity;
		
		Vector2 shortenVelocity = Vectors.ShortenVector(velocity, stickInDistance);
		transform.localPosition = new Vector2(transform.localPosition.x + shortenVelocity.x,transform.localPosition.y + shortenVelocity.y);
        IgnoreOnCollisionEnter = true;

	}

    

	void HookEnemy(Collision2D coll)
	{
		fixedJoint.enabled = true;
		fixedJoint.connectedBody = coll.rigidbody;
		fixedJoint.anchor = new Vector2 (0.7f, 0.0f);
		fixedJoint.connectedAnchor = new Vector2 (0.0f, 0.0f);

		GetComponent<RealTimeRopeCreating> ().StartMakingChain = true;
        //GetComponent<BoxCollider2D> ().enabled = false;
        IgnoreOnCollisionEnter = true;
		tugComponent.StartTugging (gameObject);
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
            if(hitGameObject.tag == "Terrain")
            {
                GetComponent<CharacterClimbUp>().setArrowStuckIn(false);
            }
		}
	}
}
