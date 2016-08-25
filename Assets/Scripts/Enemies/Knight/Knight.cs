using UnityEngine;
using System.Collections;
using System;

public class Knight : Enemy_Abstract {

    private KnightAnimations knightAnimationsComponent;
    
    // Use this for initialization
	void Start () {
        knightAnimationsComponent = GetComponent<KnightAnimations>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public override void Attack()
    {
        animator.SetBool("Attack", true);
        SwitchBooleanAfterTime("Attack", false, knightAnimationsComponent.attack1.length);
    }

    public override void Die()
    {
        Debug.Log("Knight died");
    }
}
