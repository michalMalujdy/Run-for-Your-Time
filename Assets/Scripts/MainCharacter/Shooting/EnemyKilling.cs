using UnityEngine;
using System.Collections;

public class EnemyKilling : MonoBehaviour {

	private HandleAnimations anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<HandleAnimations> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void KillEnemyOnLongDistance (Collision2D coll)
	{
		Destroy (coll.gameObject);
	}

	public void KillEnemyOnShortDistance (Collision2D coll)
	{
		anim.KillingAnimation ();
		StartCoroutine(DestroyAfterTime (coll.gameObject, 0.1f));
	}

	public void KillEnemyOnShortDistance (GameObject coll)
	{
		anim.KillingAnimation ();
		StartCoroutine(DestroyAfterTime (coll, 0.1f));
    }

	private IEnumerator DestroyAfterTime(GameObject obj, float time)
	{
        
        yield return new WaitForSeconds (time);
		Destroy (obj.gameObject);
    }
}
