using UnityEngine;
using System.Collections;

public class RunTimeRopeCreator : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameObject newRope = new GameObject();
        newRope.AddComponent<Rope>();

        Rope ropeComponent = newRope.GetComponent<Rope>();

        ropeComponent.useBendLimit = false;
        ropeComponent.FirstSegmentConnectionAnchor = new Vector2(-3.0f, 0.0f);
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
