using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MoveAlongRope : MonoBehaviour {

	private HingeJoint2D mainCharacterJoint;
	private bool isRunningUp = false;
	private bool isRunningDown = false;
	private List <Transform> listOfChainCells = new List <Transform>();
	private float movedDistnace = 0.0f;
	private float speed = 0.05f;
	private float distanceBetweenCells;
	private float chainCellHeight;
	private int chainCellIterator = 0;

	// Use this for initialization
	void Start () {
		mainCharacterJoint = GetComponent<HingeJoint2D> ();
		distanceBetweenCells = GeneralVariables.anchorBetweenCells;
		chainCellHeight = GeneralVariables.chainCellHeight_without_anchor + distanceBetweenCells;
	}
	
	// Update is called once per frame
	void Update () {
		if (mainCharacterJoint.GetComponent<ChainConnection>().IsCharacterAttachedToChain) {
			if (isRunningUp) {
				goUp ();
			} else if (isRunningDown) {
				goDown ();
			}
		}
	}

	void goUp()
	{
		if (listOfChainCells.Count > chainCellIterator) {
			movedDistnace += speed;
			GetComponent<HingeJoint2D> ().connectedAnchor = new Vector2 (mainCharacterJoint.connectedAnchor.x, mainCharacterJoint.connectedAnchor.y + speed);
			if (movedDistnace >= chainCellHeight) {
				chainCellIterator++;
				switchChainCell ();
				movedDistnace = movedDistnace - chainCellHeight;
			}
		}
	}
	void goDown()
	{
		if (chainCellIterator > 1) {
			movedDistnace -= speed;
			GetComponent<HingeJoint2D> ().connectedAnchor = new Vector2 (mainCharacterJoint.connectedAnchor.x, mainCharacterJoint.connectedAnchor.y - speed);
			if (movedDistnace <= chainCellHeight) {
				chainCellIterator--;
				switchChainCell ();
				movedDistnace = movedDistnace + chainCellHeight;
			}
		}
	}

	void switchChainCell()
	{
		mainCharacterJoint.connectedBody = listOfChainCells [listOfChainCells.Count - chainCellIterator].GetComponent<Rigidbody2D> ();
		GetComponent<HingeJoint2D>().connectedAnchor = new Vector2 (mainCharacterJoint.connectedAnchor.x, -chainCellHeight);
	}

	public void resetMovedDistance ()
	{
		movedDistnace = 0;
		chainCellIterator = 0;
	}

	public bool IsRunningUp {
		get {
			return isRunningUp;
		}
		set {
			isRunningUp = value;
		}
	}

	public bool IsRunningDown {
		get {
			return isRunningDown;
		}
		set {
			isRunningDown = value;
		}
	}

	public List<Transform> ListOfChainCells {
		get {
			return listOfChainCells;
		}
		set {
			listOfChainCells = value;
		}
	}
}
