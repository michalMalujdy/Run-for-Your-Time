using UnityEngine;
using System.Collections;

public class DisableEnableMovement : MonoBehaviour {

    private Run runComponent;
    private Jump jumpComponent;

    public Buttons runButton;
    public Buttons backButton;
    public Buttons downButton;
    public Buttons upButton;
    public Buttons jumpButton;
    public Buttons attackButton;

    // Use this for initialization
	void Start () {
        runComponent = GetComponent<Run>();
        jumpComponent = GetComponent <Jump>();	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void disableRunning()
    {
        runButton.IsButtonActive = false;
        backButton.IsButtonActive = false;
    }

    public void enableRunning()
    {
        runButton.IsButtonActive = true;
        backButton.IsButtonActive = true;
    }

    public void disableJumping()
    {
        jumpButton.IsButtonActive = false;
    }

    public void enableJumping()
    {
        jumpButton.IsButtonActive = true;
    }

    public void disableClimbingAndTugging()
    {
        upButton.IsButtonActive = false;
        downButton.IsButtonActive = false;
    }

    public void enableClimbingAndTugging()
    {
        upButton.IsButtonActive = true;
        downButton.IsButtonActive = true;
    }

    public void disableAttack()
    {
        attackButton.IsButtonActive = false;
    }

    public void enableAttack()
    {
        attackButton.IsButtonActive = true;
    }

    public void disableMovement()
    {
        disableRunning();
        disableJumping();
        disableClimbingAndTugging();
        disableAttack();
    }

    public void enableMovement()
    {
        enableRunning();
        enableJumping();
        enableClimbingAndTugging();
        enableAttack();
    }

    public void disableMovementButAttack()
    {
        disableRunning();
        disableJumping();
        disableClimbingAndTugging();
    }
}
