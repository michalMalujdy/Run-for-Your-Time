using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour {

	public Transform mainCharacter;
	public float offsetX = 0.0f;
	public float offsetY = 0.0f;
    private Zoom zoomComponent;
	// Use this for initialization
	void Awake () {
		transform.localPosition = new Vector2 (mainCharacter.localPosition.x + offsetX, mainCharacter.localPosition.y + offsetY);
	}

    void Start()
    {
        zoomComponent = GetComponentInChildren<Zoom>();
    }
	
	// Update is called once per frame
	void LateUpdate () {
        if(!zoomComponent.getIsZoomingIn())
        {
            transform.localPosition = new Vector2(mainCharacter.localPosition.x + offsetX, mainCharacter.localPosition.y + offsetY);
        }        
	}
}
