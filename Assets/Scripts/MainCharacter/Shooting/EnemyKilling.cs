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
		Destroy (coll.gameObject);
		anim.KillingAnimation ();
		DestroyAfterTime (coll.gameObject, 0.6f);
	}

	public void KillEnemyOnShortDistance (GameObject coll)
	{
		Destroy (coll.gameObject);
		anim.KillingAnimation ();
		DestroyAfterTime (coll, 0.6f);
	}

	IEnumerator DestroyAfterTime(GameObject obj, float time)
	{
		yield return new WaitForSeconds (time);
		Destroy (obj.gameObject);
	}


}
