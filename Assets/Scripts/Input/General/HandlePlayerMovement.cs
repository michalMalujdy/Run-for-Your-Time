using UnityEngine;
using System.Collections;

public class HandlePlayerMovement : MonoBehaviour {
    //CHARACTER
    private Transform mainCharacter;
    private Rigidbody2D mainCharacterRB;
    private GroundCollider groundColliderComponent;

    // JUMP
    private bool isJumping = false;
    public float jumpVelocity = 2.4f;
    private ChainConnection mainCharacterChainConnection;

    //RUN
    private Run runComponent;

    //CLIMB
    private MoveAlongRope moveAlongRopeComponent;

    public bool FreezeRunningBothDirections { get; set; }
    public bool FreezeClimbingBothDirections { get; set; }


    // Use this for initialization
    void Start () {
        mainCharacter = GetComponent<Transform>();
        mainCharacterRB = GetComponent<Rigidbody2D>();
        mainCharacterChainConnection = mainCharacterRB.GetComponent<ChainConnection>();
        groundColliderComponent = mainCharacter.GetComponentInChildren <GroundCollider>();
        runComponent = GetComponent<Run>();
        moveAlongRopeComponent = GetComponent<MoveAlongRope>();

        FreezeClimbingBothDirections = false;
        FreezeRunningBothDirections = false;
    }
	
	// Update is called once per frame
	void Update () {
        if(FreezeRunningBothDirections)
        {
            StopRunningBothDirections();
        }

        if(FreezeClimbingBothDirections)
        {
            StopClimbingBothDirections();
        }	
	}

    public void RunForward()
    {
        runComponent.IsRunningForeward = true;
    }

    public void StopRunningForward()
    {
        runComponent.IsRunningForeward = false;
    }

    public void RunBack()
    {
        runComponent.IsRunningBack = true;
    }

    public void StopRunningBack()
    {
        runComponent.IsRunningBack = false;
    }

    public void StopRunningBothDirections()
    {
        runComponent.IsRunningBack = false;
        runComponent.IsRunningForeward = false;
    }

    public void Jump()
    {
        if (!mainCharacterChainConnection.IsCharacterAttachedToChain)
        {
            if (!isJumping)
            {
                mainCharacterRB.velocity = new Vector2(mainCharacterRB.velocity.x, mainCharacterRB.velocity.y + jumpVelocity);
                isJumping = true;
                groundColliderComponent.IsGrounded = false;
            }
        }
        if (isJumping && groundColliderComponent.IsGrounded)
        {
            isJumping = false;
        }
    }

    public void ClimbUp()
    {
        moveAlongRopeComponent.IsRunningUp = true;
    }

    public void StopClimbingUp()
    {
        moveAlongRopeComponent.IsRunningUp = false;
    }

    public void ClimbDown()
    {
        moveAlongRopeComponent.IsRunningDown = true;
    }

    public void StopClimbingDown()
    {
        moveAlongRopeComponent.IsRunningDown = false;
    }

    public void StopClimbingBothDirections()
    {
        moveAlongRopeComponent.IsRunningDown = false;
        moveAlongRopeComponent.IsRunningUp = false;
    }
}
