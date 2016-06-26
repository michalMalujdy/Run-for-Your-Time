using UnityEngine;
using System.Collections;

public class MaxOneSwingArrow : MonoBehaviour {

	private ShootingMode character;
	private CountingArrows cameraHolder;

	void Awake()
	{

		character = GameObject.FindWithTag ("Player").GetComponent<ShootingMode>();
		cameraHolder = GameObject.Find ("CameraHolder").GetComponent<CountingArrows> ();
		if (cameraHolder.isArrowShoot) {
			Destroy (cameraHolder.ArrowShoot.gameObject);
			Destroy (cameraHolder.ChainAttached);
			character.GetComponent<MoveAlongRope>().resetMovedDistance();

			character.GetComponent<ChainConnection>().IsCharacterAttachedToChain = false;
		} 
		cameraHolder.isArrowShoot = true;
		cameraHolder.ArrowShoot = this.gameObject;

	}

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		if (character.SwingMode  && GetComponent<Hit>().IsHit) {
			cameraHolder.ChainAttached = GameObject.FindWithTag ("WholeChain");
		}
	}
}
