using UnityEngine;
using System.Collections;

public class Anchored : MonoBehaviour {

	protected bool isHit = false;
	protected Vector2 position;
	protected float alpha;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (isHit) {
			//transform.position = position;
			//transform.rotation = Quaternion.Euler (new Vector3 (0.0f, 0.0f, alpha));
		}
			
	}

	public bool IsHit {
		get {
			return isHit;
		}
		set {
			isHit = value;
		}
	}

	public Vector2 Position {
		get {
			return position;
		}
		set {
			position = value;
		}
	}
}
