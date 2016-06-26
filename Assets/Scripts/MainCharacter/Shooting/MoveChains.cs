using UnityEngine;
using System.Collections;

public class MoveChains : MonoBehaviour {

	public GameObject chainPrefab;
	Transform arrow;

	void Awake(){
		arrow = transform.parent;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
