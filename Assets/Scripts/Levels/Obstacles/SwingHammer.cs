using UnityEngine;
using System.Collections;

public class SwingHammer : MonoBehaviour {

    //Swinging hammer
    public enum Size
    {
        Small = 1,
        Medium = 2,
        LongerMedium = 4,
        Big = 3
    }

    public Size hammerSize = Size.Small;

    private Rigidbody2D rbComponent;
    private float inclinationMargin = 15.0f;
    private bool forceAdded = false;

    private Vector2 initialForce;
    private float sustainingForce;


    //Making damages
    public float damage = 50.0f;
    bool cantMove = false;
    float cantMoveTimer = 0.0f;
    float cantMoveTime = 1.0f;
    DisableEnableMovement characterMovementManagament;

    // Use this for initialization
    void Start()
    {
        rbComponent = GetComponent<Rigidbody2D>();

        if (hammerSize == Size.Big)
        {
            initialForce = new Vector2(6700000.0f, 0.0f);
            sustainingForce = 10000f;
        }
        else if (hammerSize == Size.Medium)
        {
            initialForce = new Vector2(3350000.0f, 0.0f);
            sustainingForce = 4500f;
        }
        else if (hammerSize == Size.LongerMedium)
        {
            initialForce = new Vector2(3750000.0f, 0.0f);
            sustainingForce = 5000f;
        }
        else if (hammerSize == Size.Small)
        {
            initialForce = new Vector2(1920000.0f, 0.0f);
            sustainingForce = 4000f;
        }

        rbComponent.AddForce(initialForce);
    }

    // Update is called once per frame
    void Update()
    {        
        if (transform.rotation.eulerAngles.z >= 360.0f - inclinationMargin || transform.rotation.eulerAngles.z <= inclinationMargin)
        {
            if (!forceAdded)
            {
                rbComponent.AddForce(new Vector2(sustainingForce * Mathf.Sign(rbComponent.velocity.x), sustainingForce));
                forceAdded = true;
            }
        }
        else
        {
            forceAdded = false;
        }


        if (cantMove)
        {
            cantMoveTimer += Time.deltaTime;
            if (cantMoveTimer >= cantMoveTime)
            {
                characterMovementManagament.enableMovement();
                cantMove = false;
                cantMoveTimer = 0.0f;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player" && !cantMove)
        {
            other.gameObject.GetComponent<Stats>().AddSubstractCurrentHealth(-damage);

            characterMovementManagament = other.gameObject.GetComponent<DisableEnableMovement>();
            characterMovementManagament.disableMovement();

            cantMove = true;
        }
    }
}
