using UnityEngine;
using System.Collections;

public class ManageDeath : MonoBehaviour {

	public Bar healthBar;
    public Canvas DeathCanvas;
    private HandleAnimations characterAnimations;
    private Transform transformComponent;

    public float LowestYPoint;
    
    // Use this for initialization
	void Start () {
        characterAnimations = GetComponent<HandleAnimations>();
        transformComponent = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
	    if(healthBar.Status <= 0)
        {
            Die();
        }
        if(transformComponent.position.y < LowestYPoint)
        {
            Die();
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
        }
	}

    void Die()
    {
        characterAnimations.DeadAnimation();
        DeathCanvas.gameObject.SetActive(true);
    }
}
