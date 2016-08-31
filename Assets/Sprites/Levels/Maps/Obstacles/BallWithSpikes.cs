using UnityEngine;
using System.Collections;

public class BallWithSpikes : MonoBehaviour {

    public float damage = 30.0f;
    //public float xForceOnPlayer = 1000.0f;


    private float cantMoveTimer = 0.0f;
    private float cantMoveTime = 2.0f;
    private bool cantMove = false;
    private DisableEnableMovement characterMovementManagament;




    
    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(cantMove)
        {
            cantMoveTimer += Time.deltaTime;
            if(cantMoveTimer >= cantMoveTime)
            {
                characterMovementManagament.enableMovement();
                cantMove = false;
                cantMoveTimer = 0.0f;
            }
        }
	}

    void OnCollisionEnter2D (Collision2D other)
    {
        if(other.gameObject.tag == "Player" && !cantMove)
        {
            other.gameObject.GetComponent<Stats>().AddSubstractCurrentHealth(-damage);

            //other.rigidbody.AddForce(new Vector2(xForceOnPlayer * Mathf.Sign(GetComponent<Rigidbody2D>().velocity.x), 0.0f));

            characterMovementManagament = other.gameObject.GetComponent<DisableEnableMovement>();
            characterMovementManagament.disableMovement();

            cantMove = true;              
        }
    }
}
