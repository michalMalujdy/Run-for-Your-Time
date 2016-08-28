using UnityEngine;
using System.Collections;
using System;

public class Knight : Enemy_Abstract {

    private Transform mainCharacter;
    private KnightAnimations knightAnimationsComponent;
    
    // Use this for initialization
	void Start () {
        knightAnimationsComponent = GetComponent<KnightAnimations>();
        mainCharacter = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public override void Attack()
    {
        animator.SetBool("Attack", true);
        SwitchBooleanAfterTime("Attack", false, knightAnimationsComponent.attack1.length);

        mainCharacter.GetComponent<Stats>().AddSubstractCurrentHealth(-damage);
    }

    public override void Die()
    {
        Debug.Log("Knight died");
        Destroy(this.gameObject);
    }
}
