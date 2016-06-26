using UnityEngine;
using System.Collections;

public class ShootingMode : MonoBehaviour {

	bool shootMode = true;
	bool swingMode = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public bool ShootMode {
		get {
			return shootMode;
		}
		set {
			shootMode = value;
			//Debug.Log("ShootMode="+value);
		}
	}

	public bool SwingMode {
		get {
			return swingMode;
		}
		set {
			swingMode = value;
			//Debug.Log("SwingMode="+value);
		}
	}
}
