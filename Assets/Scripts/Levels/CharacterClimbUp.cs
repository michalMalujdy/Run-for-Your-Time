using UnityEngine;
using System.Collections;

public class CharacterClimbUp : MonoBehaviour {

    private Collider2D colliderComponent;
    private bool arrowStuckIn = false;
    public Transform mainCharacter;
    private Transform transformComponent;

    public float yOffset = 2.0f;
    public float xOffset = 2.0f;

    private bool isUnder;
    private bool isOnLeft;
    private bool isOnRight;

    private bool isClimbingUp = false;

    public void setArrowStuckIn(bool value)
    {
        arrowStuckIn = value;
    }

    public bool getArrowStuckIn()
    {
        return arrowStuckIn;
    }

    // Use this for initialization
	void Start () {
        colliderComponent = GetComponent<Collider2D>();
        mainCharacter = GameObject.FindWithTag("Player").GetComponent <Transform>();
        transformComponent = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
	    if(arrowStuckIn)
        {
            if(!isClimbingUp)
            {
                isUnder = mainCharacter.position.y >= colliderComponent.bounds.center.y + colliderComponent.bounds.extents.y - yOffset && mainCharacter.position.y <= colliderComponent.bounds.center.y + colliderComponent.bounds.extents.y;
                isOnLeft = (mainCharacter.position.x >= colliderComponent.bounds.center.x - colliderComponent.bounds.extents.x - xOffset) && (mainCharacter.position.x <= colliderComponent.bounds.center.x - colliderComponent.bounds.extents.x);
                isOnRight = (mainCharacter.position.x <= colliderComponent.bounds.center.x + colliderComponent.bounds.extents.x + xOffset) && (mainCharacter.position.x >= colliderComponent.bounds.center.x + colliderComponent.bounds.extents.x);
                if (isUnder && (isOnLeft || isOnRight))
                {
                    ClimbUp();
                }
            }
        }
	}

    void ClimbUp()
    {
        isClimbingUp = true;
        Debug.Log("Climb Up");

        if(isOnLeft)
        {
            mainCharacter.position = new Vector2(colliderComponent.bounds.center.x - colliderComponent.bounds.extents.x + xOffset / 2.5f, colliderComponent.bounds.center.y + colliderComponent.bounds.extents.y + yOffset / 2.5f);
        }

        else if(isOnRight)
        {
            mainCharacter.position = new Vector2(colliderComponent.bounds.center.x + colliderComponent.bounds.extents.x - xOffset / 2.5f, colliderComponent.bounds.center.y + colliderComponent.bounds.extents.y + yOffset / 2.5f);
        }

        mainCharacter.GetComponent<ChainConnection>().Dismount();
        isClimbingUp = false;
        arrowStuckIn = false;
    }
}
