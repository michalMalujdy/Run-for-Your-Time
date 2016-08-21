using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {

	public Vector2 velocity;
	private float speed = 0.75f;
	private float alpha;
    private Vector2 startPoint;
    private Vector2 previousPosition;
    private Vector2 currentPosition;
    private float dx;
    private float dy;
    private Vector2 derivatePositionVector;
	private string arrowType;
	private ShootingMode mainCharacter;



    // Use this for initialization
    void Awake(){
		velocity = GameObject.Find("ScreenInputZone").GetComponent<OnDrag>().ArrowVelocity;
		alpha = GameObject.Find ("ScreenInputZone").GetComponent<OnDrag> ().alpha;
		mainCharacter = GameObject.FindWithTag ("Player").GetComponent<ShootingMode>();

        previousPosition = transform.localPosition;
        currentPosition = transform.localPosition;
        dx = 0.0f;
        dy = 0.0f;
        DerivatePositionVector = new Vector2(0.0f, 0.0f);
	}
	void Start () {
		StartPoint = transform.localPosition;
        GetComponent<Rigidbody2D>().velocity = new Vector2(velocity.x * speed, velocity.y * speed);
        
        if (mainCharacter.ShootMode) {
			arrowType = "Shoot";
            GameObject.Find("CameraHolder").GetComponent<CountingArrows>().AddNewArrow(gameObject, "Shoot");
        } else if (mainCharacter.SwingMode) {
			arrowType = "Swing";
		}
	}
	
	// Update is called once per frame
	void Update () {
	}

    public Vector2 StartPoint
    {
        get
        {
            return startPoint;
        }

        set
        {
            startPoint = value;
        }
    }

    public Vector2 DerivatePositionVector
    {
        get
        {
            return derivatePositionVector;
        }

        set
        {
            derivatePositionVector = value;
        }
    }

    void DeltaPosition()
    {
        currentPosition = transform.localPosition;
        dx = currentPosition.x - previousPosition.x;
        dy = currentPosition.y - previousPosition.y;
        DerivatePositionVector = new Vector2(dx, dy);
        previousPosition = transform.localPosition;
    }

	public string ArrowType {
		get {
			return arrowType;
		}
	}

	public float Alpha {
		get {
			return alpha;
		}
	}
}
