using UnityEngine;
using System.Collections;

public class Combat : MonoBehaviour {

	private Rigidbody2D rbComponent;
    private Attack attackComponent;
    private float ableToAttackRadious = 1.5f;
    private float getHurtRadious = 0.8f;
    private HandleAnimations animationComponent;

    Collider2D[] enemiesAround;
    Collider2D[] enemiesAttacking;

    private float retreatDistance = 2.0f;
    private int retreatDirection; //1 do prawej, -1 do lewej
    private float retreatSpeed = 0.1f;
    private float retreatedDistance = 0.0f;
    private float retreatTimeDelay = 0.4f;
    private bool retreating = false;
    private bool underAttackProcedureStarted = false;

    // Use this for initialization
    void Start () {
        rbComponent = GetComponent<Rigidbody2D>();
        animationComponent = GetComponent<HandleAnimations>();
	}
	
	// Update is called once per frame
	void Update () {
        CheckForEnemiesAround();
        
        if(retreating)
        {
            Retreating();
        }	
	}

    public void MeleeAttack()
    {
        animationComponent.triggerAttackClicked();
    }

    private void CheckForEnemiesAround()
    {

        enemiesAround = EnemiesInRange(ableToAttackRadious);

        if (enemiesAround.Length > 0)
        {
            animationComponent.setNoEnemyAround(false);
        }
        else
        {
            animationComponent.setNoEnemyAround(true);
        }

        enemiesAttacking = EnemiesInRange(getHurtRadious);
        if (enemiesAttacking.Length > 0)
        {
            animationComponent.setIsCharacterAttacked(true);
        }
        else
        {
            animationComponent.setIsCharacterAttacked(false);
        }

        if (animationComponent.getIsCharacterAttacked() && !underAttackProcedureStarted)
        {
            foreach (Collider2D coll in enemiesAttacking)
            {
                coll.GetComponent<Enemy_Abstract>().Attack();
                if (coll.GetComponent<Transform>().position.x > rbComponent.position.x)
                {
                    if(retreatSpeed > 0)
                    {
                        retreatSpeed *= -1;
                    }                    
                }
                else
                {
                    if(retreatSpeed < 0)
                    {
                        retreatSpeed *= -1;
                    }   
                }

                RetreatAfterTime(retreatTimeDelay);
            }
            underAttackProcedureStarted = true;
        }
    }

    private Collider2D[] EnemiesInRange(float radious)
    {
        Collider2D[] allCollidersInCircle = Physics2D.OverlapCircleAll(rbComponent.position, radious);

        int enemiesInArrayCounter = 0;
        for (int i = 0; i < allCollidersInCircle.Length; i++)
        {
            if (allCollidersInCircle[i].tag == "Enemy" && allCollidersInCircle[i] != null)
            {
                enemiesInArrayCounter++;
            }
        }

        Collider2D[] enemiesTab = new Collider2D [enemiesInArrayCounter];
        int enemiesTabIndexer = 0;

        for (int i = 0; i < allCollidersInCircle.Length; i++)
        {
            if(allCollidersInCircle[i].tag == "Enemy" && allCollidersInCircle[i] != null)
            {
                enemiesTab[enemiesTabIndexer] = allCollidersInCircle[i];
                enemiesTabIndexer++;
            }
        }
        return enemiesTab;
    }

    private void RetreatAfterTime(float time)
    {
        StartCoroutine(Retreat(time));
    }

    IEnumerator Retreat (float time)
    {
        yield return new WaitForSeconds(time);
        retreating = true;

    }
    private void Retreating()
    {
        if(retreatedDistance < retreatDistance)
        {
            Vector2 newPosition = new Vector2(rbComponent.position.x + retreatSpeed, rbComponent.position.y);
            rbComponent.position = newPosition;
            retreatedDistance += Mathf.Abs(retreatSpeed);
        }
        else
        {
            retreatedDistance = 0.0f;
            retreating = false;
            underAttackProcedureStarted = false;
        }
    }
}
