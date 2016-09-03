using UnityEngine;
using System.Collections;

public class WeaponCollisionWithPlayer : MonoBehaviour {

    private LurkingEnemy lurkingEnemyComponent;

    public void SetLurkingEnemyComponent(LurkingEnemy LEComponent)
    {
        lurkingEnemyComponent = LEComponent;
    }

    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnTriggerEnter2D (Collider2D other)
    {
        if(other.tag == "Player")
        {
            lurkingEnemyComponent.PlayerGotHit();
        }
    }
}
