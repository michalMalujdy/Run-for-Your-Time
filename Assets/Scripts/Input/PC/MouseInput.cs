using UnityEngine;
using System.Collections;

public class MouseInput : MonoBehaviour {


    public Transform mainCharacter;

    private Vector2 distance;
    private OnDrag onDragComponent;

    // Use this for initialization
    void Start () {
        onDragComponent = GetComponent<OnDrag>();
        Input.simulateMouseWithTouches = false;
	}
	
	// Update is called once per frame
	void Update () {
#if UNITY_EDITOR
        if (Input.GetMouseButtonUp(0))
        {
            distance = Camera.main.ScreenToWorldPoint(Input.mousePosition) - mainCharacter.position;
            onDragComponent.PrepareArrowToShoot(distance);
        }
#endif
    }

}
