using UnityEngine;
using System.Collections;

public class DeathArea : MonoBehaviour
{

    private BoxCollider2D boxColliderComponent;

    // Use this for initialization
    void Start()
    {
        boxColliderComponent = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        Debug.Log("kolizja");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
             other.GetComponent<Stats>().AddSubstractCurrentHealth(- other.GetComponent<Stats>().getMaxHealth());
        }
        else if(other.tag == "Enemy")
        {
            other.GetComponent<Enemy_Abstract>().Die();
        }
    }
}