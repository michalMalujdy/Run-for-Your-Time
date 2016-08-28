using UnityEngine;
using System.Collections;

public class SwingChainWithBall : MonoBehaviour {

    private Rigidbody2D rbComponent;

    private Vector2 initialForce = new Vector2 (1500000.0f, 0.0f);
    private float sustainingXForce = 500000f;

    private float xSpeedMargin = 0.15f;
     
    // Use this for initialization
	void Start () {
        rbComponent = GetComponent<Rigidbody2D>();

        rbComponent.AddForce(initialForce);
	}
	
	// Update is called once per frame
	void Update () {

        if(rbComponent.velocity.x < xSpeedMargin && rbComponent.velocity.x > -xSpeedMargin)
        {
            rbComponent.AddForce(new Vector2(sustainingXForce * (-Mathf.Sign(rbComponent.velocity.x)), - sustainingXForce));
        }	
	}
}
