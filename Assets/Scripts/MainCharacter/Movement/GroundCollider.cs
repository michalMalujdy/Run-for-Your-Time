using UnityEngine;
using System.Collections;

public class GroundCollider : MonoBehaviour {

	private Jump jumpComponent;
	public Rigidbody2D maincharacter;
	private bool isGrounded;
	private Transform transformComponent;
	private float radious = 0.05f;
	private Collider2D groundPointCollider;
    Collider2D[] collidersInCircle;
    private int terrainLayer;
    private int enemyLayer;

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
        terrainLayer = LayerMask.GetMask("Terrain");
        enemyLayer = LayerMask.GetMask("Enemy");
    }
	
	// Update is called once per frame
	void Update () {
		CheckForColliders (terrainLayer);
        if(!isGrounded)
        {
            CheckForColliders(enemyLayer);
        }
    }
	void CheckForColliders(int layerNumber)
	{
        collidersInCircle = Physics2D.OverlapCircleAll (transform.position, radious, layerNumber);

			if (collidersInCircle.GetLength (0) != 0) {
				isGrounded = true;
            }
            else
            {
                isGrounded = false;
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
