using UnityEngine;
using System.Collections;

public class ChainConnection : MonoBehaviour {

	private bool isCharacterAttachedToChain = false;

	public Gestures ScreenInputZone;
	private Tug tugComponent;


	// Use this for initialization
	void Start () {
		tugComponent = GetComponent<Tug> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (isCharacterAttachedToChain) {
			if (ScreenInputZone.CheckForDoubleTap () || Input.GetKeyDown(KeyCode.LeftControl)) {
				Dismount ();
			}
		}
	}

	public bool IsCharacterAttachedToChain {
		get {
			return isCharacterAttachedToChain;
		}
		set {
			isCharacterAttachedToChain = value;
		}
	}

	public void Dismount ()
	{
		GetComponent<HingeJoint2D> ().enabled = false;
		isCharacterAttachedToChain = false;
		GetComponent<HingeJoint2D> ().connectedBody = null;
		if (tugComponent.IsTuggingEnable) {
			tugComponent.StopTugging ();
		}
	}
}
