using UnityEngine;
using System.Collections;

public class OutOfScreen : MonoBehaviour {

	public float maxX;
	public float maxY;

	private Transform MainCharacter;

	void Awake(){
		MainCharacter = GameObject.FindWithTag("Player").GetComponent<Transform>();
	}
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.x > MainCharacter.position.x + maxX || transform.position.x < MainCharacter.position.x - maxX ||
			transform.position.y > MainCharacter.position.y + maxY || transform.position.y < MainCharacter.position.y - maxY) {
			Destroy(gameObject);	
		}
	}
}
