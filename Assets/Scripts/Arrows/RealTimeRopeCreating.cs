using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RealTimeRopeCreating : MonoBehaviour {
	
	public Transform chainCellPrefab;
	//potrzebne żeby podłączyć nowy element łańcucha do poprzedniego. Inaczej poprzednia stworzona komórka łańcucha.
	private Transform oldCell;
	//Zakotwiczenie przy strzale (tylko pierwsza komórka)
	private float arrowAnchor = 0.4f;
	//Zakotwiczenie przy pozostałych komórkach.
	private float anchorBetweenCells;
	private float anchorPlacemenent = 0.15f;
	//Wykorzystane do sprawdzenia czy wlaczany jest tryb swing
	private ShootingMode mainCharacter;
	private float chainCellHeightWithAnchor;
	private float magnitude;
	//Oznacza, że strzała typu swing się wbiła i można zacząć tworzyć łańcuch
	private bool startMakingChain = false;
	//pozycja postaci w momencie wbicia sie strzaly typu swing
	private Vector2 characterPosition;
	//pozycja strzaly w momencie wbicia sie
	private Vector2 arrowPosition;
	private bool firstChainCellCreated = false;
	public Transform wholeChainPrefab;
	private Transform newWholeChain;
	private List <Transform> listOfChainCells = new List <Transform>();
	
	void Start()
	{
		anchorBetweenCells = GeneralVariables.anchorBetweenCells;
		chainCellHeightWithAnchor = GeneralVariables.chainCellHeight_without_anchor + anchorBetweenCells;
		mainCharacter = GameObject.FindGameObjectWithTag ("Player").GetComponent<ShootingMode> ();
	}
	
	void Update () {		
		//jeśli moduł większy, niż długość boku komórki i jest włączany tryb Swing, to tworzymy kolejne komórki
		if (mainCharacter.SwingMode && startMakingChain) {
			//obl odl miedzy postacia z strzala i utworzyc lancuch o tej dlugosci
			characterPosition = mainCharacter.transform.position;
			arrowPosition = transform.position;
			Vector2 distance = characterPosition - arrowPosition;
			int chainCellCount = Mathf.CeilToInt(Mathf.Abs(distance.magnitude) / chainCellHeightWithAnchor);

			for(int i = 2; i < chainCellCount; i++)
			{
				createChainCell();
			}
			attachCharacter();
			Destroy(this);
		}
	}
	
	void createChainCell()
	{
		Transform newCell = (Transform)Instantiate (chainCellPrefab, mainCharacter.transform.position, Quaternion.identity);
		if (!firstChainCellCreated) {
			newCell.GetComponent<HingeJoint2D> ().connectedBody = GetComponent<Rigidbody2D> ();	
			newWholeChain = Instantiate (wholeChainPrefab);
		} else {
			newCell.GetComponent<HingeJoint2D> ().connectedBody = oldCell.GetComponent<Rigidbody2D> ();
		}
		newCell.transform.parent = newWholeChain.transform;
		newCell.GetComponent<HingeJoint2D> ().connectedAnchor = new Vector2 (0.0f, anchorBetweenCells);
		newCell.GetComponent<HingeJoint2D> ().anchor = new Vector2 (0.0f, anchorPlacemenent);
		listOfChainCells.Add (newCell);
		oldCell = newCell;
		firstChainCellCreated = true;
	}
	
	public void attachCharacter()
	{
		mainCharacter.GetComponent<HingeJoint2D> ().enabled = true;
		mainCharacter.GetComponent<HingeJoint2D> ().connectedBody = oldCell.GetComponent<Rigidbody2D> ();
		mainCharacter.GetComponent<HingeJoint2D> ().connectedAnchor = new Vector2 (0.0f, anchorBetweenCells * 2f);
		mainCharacter.GetComponent<ChainConnection>().IsCharacterAttachedToChain = true;

		for (int i = 0; i < listOfChainCells.Count; i++) {
			Physics2D.IgnoreCollision (mainCharacter.GetComponent<Collider2D> (), listOfChainCells [i].GetComponent<Collider2D> ());
		}
		mainCharacter.GetComponent<MoveAlongRope> ().ListOfChainCells = listOfChainCells;
	}

	public bool StartMakingChain {
		get {
			return startMakingChain;
		}
		set {
			startMakingChain = value;
		}
	}
}
