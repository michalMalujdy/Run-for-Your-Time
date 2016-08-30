using UnityEngine;
using System.Collections;

public class BallWithSpikes : MonoBehaviour {

    public float damage = 30.0f;
    public float xForceOnPlayer = 1000.0f;
    public float cantMoveTimer;

    private bool cantMove;

    
    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D (Collision2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            //other.gameObject.GetComponent<Stats>().AddSubstractCurrentHealth(-damage);

            //other.rigidbody.AddForce(new Vector2(xForceOnPlayer * Mathf.Sign(GetComponent<Rigidbody2D>().velocity.x), 0.0f));
         
        }
    }
}
