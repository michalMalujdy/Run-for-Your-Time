using UnityEngine;
using System.Collections;

public class SwingChainWithBall : MonoBehaviour {

    public int cellsNumber;

    private Rigidbody2D rbComponent;
    private float yStartPositon;
    private float positionMargin = 0.1f;
    private bool forceAdded = false;

    private Vector2 initialForce;
    private float sustainingForce;

    private float xSpeedMargin = 0.15f;

    private float maxAngle = 0.0f;
     
    // Use this for initialization
	void Start () {
        rbComponent = GetComponent<Rigidbody2D>();
        yStartPositon = rbComponent.position.y;

        if(cellsNumber == 12)
        {
            initialForce = new Vector2(3500000.0f, 0.0f);
            sustainingForce = 100000f;
        }
        else if(cellsNumber == 8)
        {
            initialForce = new Vector2(3250000.0f, 0.0f);
            sustainingForce = 50000f;
        }
        else if (cellsNumber == 6)
        {
            initialForce = new Vector2(2700000.0f, 0.0f);
            sustainingForce = 30000f;
        }

        rbComponent.AddForce(initialForce);
	}
	
	// Update is called once per frame
	void Update () {

        /*if(rbComponent.rotation > Mathf.Abs(maxAngle))
        {
            maxAngle = Mathf.Abs(rbComponent.rotation);
            Debug.Log("Max angle = " + maxAngle);
        }*/

        if(rbComponent.position.y <= yStartPositon + positionMargin && rbComponent.position.y >= yStartPositon - positionMargin)
        {
            if(!forceAdded)
            {
                rbComponent.AddForce(new Vector2(sustainingForce * Mathf.Sign(rbComponent.velocity.x), sustainingForce));
                forceAdded = true;
            }
        }
        else
        {
            forceAdded = false;
        }
	}
}
