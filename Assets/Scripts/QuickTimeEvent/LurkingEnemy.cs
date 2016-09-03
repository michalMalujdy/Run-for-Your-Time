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
    private Attack attackButton;

    public Zoom cameraZoom;
    private float timeToZoomOut = 0.3f;
    private float zoomOutTimer = 0.0f;
    private bool startZoomOutTimer = false;

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

            attackButton = GameObject.Find("AttackButton").GetComponent<Attack>();
            attackButton.SetLurkingEnemyComponent(this);

            SetEnemyHidden();
        }
        else
        {
            this.enabled = false;
        }
	}
	
	// Update is called once per frame
	void Update () {
	    if(startZoomOutTimer)
        {
            zoomOutTimer += Time.unscaledDeltaTime;
            if(zoomOutTimer >= timeToZoomOut)
            {  
                EndLurking();
            }
        }
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
                player.GetComponent<DisableEnableMovement>().disableClimbingAndTugging();
                player.GetComponent<DisableEnableMovement>().disableJumping();
                player.GetComponent<DisableEnableMovement>().disableRunning();



                attackButton.QuickTimeEventMode = true;

                chosenEnemy.GetComponent<Enemy_Abstract>().LureAttack(player.position);
                cameraZoom.ZoomIn();

                Time.timeScale = 0.1f;
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

        attackButton.QuickTimeEventMode = false;

        startZoomOutTimer = true;
    }

    public void EnemyGotHit()
    {
        chosenEnemy.GetComponent<Enemy_Abstract>().Die();
        attackButton.QuickTimeEventMode = false;
        startZoomOutTimer = true;
    }

    private void EndLurking()
    {        
        cameraZoom.ZoomOut();
        player.GetComponent<DisableEnableMovement>().enableMovement();
        Time.timeScale = 1.0f;
        GetComponent<BoxCollider2D>().enabled = false;
        enabled = false;
    }
}
