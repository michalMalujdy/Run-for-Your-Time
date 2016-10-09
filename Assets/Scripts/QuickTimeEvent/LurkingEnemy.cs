using UnityEngine;
using System.Collections;

public class LurkingEnemy : MonoBehaviour {

    public float probability; //0 to brak mozliwosci wystapienia zdarzenia, a 1 to pewne wystapienie
    private int maxValue = 1000;
    private int maxChosenValue;

    private bool isLurking;
    private string hiddenLayerName = "HiddenEnemy";

    public GameObject enemyKnightPrefab;
    private GameObject chosenEnemy;
    private GameObject chosenPrefab;

    public QuickTimeEvent quickTimeEventHandler;

    private Transform player;

    public bool getIsLurking()
    {
        return isLurking;
    }
    // Use this for initialization
	void Start () {
        randomizeOccurance();
        chooseEnemy();

        if(isLurking)
        {
            chosenEnemy = (GameObject) Instantiate(chosenPrefab, GetComponent<Transform>().position, Quaternion.Euler(0.0f, 0.0f, 0.0f));
            chosenEnemy.GetComponent<Knight>().body[8].GetComponent<WeaponCollisionWithPlayer>().SetLurkingEnemyComponent (this);
            SetEnemyHidden();

            quickTimeEventHandler = GameObject.FindWithTag("QuickTimeEvent").GetComponent<QuickTimeEvent>();
                      
        }
        else
        {
            this.enabled = false;
        }
	}
	
	// Update is called once per frame
	void Update () {
	}

    private void randomizeOccurance()
    {
        if(probability > 1.0f)
        {
            probability = 1.0f;
        }

        maxChosenValue = (int) ((float)maxValue * probability);

        int outcome = Random.Range(0, maxValue);

        if(outcome < maxChosenValue)
        {
            isLurking = true;
        }
        else
        {
            GetComponent<BoxCollider2D>().enabled = false;
            enabled = false;
        }
    }

    private void chooseEnemy()
    {
        chosenPrefab = enemyKnightPrefab;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if(other.GetComponentInChildren<GroundCollider>().IsGrounded)
            {
                SetEnemyQTEVisible();
                player = GameObject.FindWithTag("Player").GetComponent<Transform>();      
                chosenEnemy.GetComponent<Enemy_Abstract>().LureAttack(player.position);
                quickTimeEventHandler.StartQTE(QuickTimeEvent.lurkingEnemy, this, true, "AttackButton");
            }
        }
    }

    public void SetEnemyHidden()
    {
        chosenEnemy.GetComponent<Enemy_Abstract>().SetSortingLayer(hiddenLayerName);
        chosenEnemy.GetComponent<Enemy_Abstract>().isHidden = true;
        chosenEnemy.gameObject.layer = 18;
        chosenEnemy.tag = "HiddenEnemy";
    }

    public void SetEnemyQTEVisible()
    {
        chosenEnemy.GetComponent<Enemy_Abstract>().SetSortingLayer("Enemy");
        chosenEnemy.GetComponent<Enemy_Abstract>().isHidden = false;
    }

    public void SetEnemyFullVisible()
    {
        chosenEnemy.GetComponent<Enemy_Abstract>().SetSortingLayer("Enemy");
        chosenEnemy.GetComponent<Enemy_Abstract>().isHidden = false;
        chosenEnemy.gameObject.layer = 13;
        chosenEnemy.tag = "Enemy";
    }

    public void PlayerGotHit()
    {
        player.GetComponent<Stats>().AddSubstractCurrentHealth(-chosenEnemy.GetComponent<Enemy_Abstract>().damage * 2.0f);
        player.GetComponent<Combat>().MakeRetreat(1);
        chosenEnemy.GetComponent<Enemy_Abstract>().getWeaponCollider().enabled = false;

        chosenEnemy.GetComponent<Enemy_Abstract>().isHidden = false;
        SetEnemyFullVisible();

        EndLurking();
    }

    public void EnemyGotHit()
    {
        chosenEnemy.GetComponent<Enemy_Abstract>().Die();

        EndLurking();
    }

    private void EndLurking()
    {
        quickTimeEventHandler.EndQTE();
        GetComponent<BoxCollider2D>().enabled = false;
        enabled = false;
    }
}
