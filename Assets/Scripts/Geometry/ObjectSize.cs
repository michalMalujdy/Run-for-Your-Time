using UnityEngine;
using System.Collections;

public class ObjectSize : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Debug.Log("Size of x" + gameObject.name + " = " + GetComponent<Renderer> ().bounds.size.x);
		Debug.Log("Size of y" + gameObject.name + " = " + GetComponent<Renderer> ().bounds.size.y);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
