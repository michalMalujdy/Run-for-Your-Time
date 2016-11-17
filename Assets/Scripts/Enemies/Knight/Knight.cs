using UnityEngine;
using System.Collections;
using System;

public class Knight : Enemy_Abstract {

    private Transform mainCharacter;
    private KnightAnimations knightAnimationsComponent;
    public PolygonCollider2D weapon;

    public Transform[] body = new Transform [15];

    // Use this for initialization
    void Start () {
        knightAnimationsComponent = GetComponent<KnightAnimations>();
        mainCharacter = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        weaponCollider = weapon;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public override void Attack()
    {
        if(!isHidden)
        {
            animator.SetBool("Attack", true);
            SwitchBooleanAfterTime("Attack", false, knightAnimationsComponent.attack1.length);
            adjustOrientationToPlayer();

            mainCharacter.GetComponent<Stats>().AddSubstractCurrentHealth(-damage);
        }
    }

    public override void Die()
    {
        Destroy(this.gameObject);
    }

    public override void LureAttack(Vector2 playerPosition)
    {
        if (playerPosition.x < transform.position.x)
        {
            GetComponent<Transform>().position = new Vector2(playerPosition.x + 1.2f, GetComponent<Transform>().position.y + 1.5f);
        }
        else
        {
            GetComponent<Transform>().position = new Vector2(playerPosition.x - 1.2f, GetComponent<Transform>().position.y + 1.5f);
        }
        weaponCollider.enabled = true;
        animator.SetBool("Attack", true);
        SwitchBooleanAfterTime("Attack", false, knightAnimationsComponent.attack1.length);
        adjustOrientationToPlayer();
    }

    public override void SetSortingLayer(string layerName)
    {
        foreach(Transform part in body)
        {
            part.GetComponent<SpriteRenderer>().sortingLayerName = layerName;
        }
    }
}
