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
    private Vector2 anchorFromCharacter;
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
    private GameObject arrowWithChain;


    void Start()
	{
		anchorBetweenCells = GeneralVariables.anchorBetweenCells;
		chainCellHeightWithAnchor = GeneralVariables.chainCellHeight_without_anchor + anchorBetweenCells;
		mainCharacter = GameObject.FindGameObjectWithTag ("Player").GetComponent<ShootingMode> ();
        anchorFromCharacter = new Vector2(0.73f, -1.02f);
    }
	
	void Update () {		
		//jeśli moduł większy, niż długość boku komórki i jest włączany tryb Swing, to tworzymy kolejne komórki
		if (mainCharacter.SwingMode && startMakingChain) {
			//obl odl miedzy postacia z strzala i utworzyc lancuch o tej dlugosci
			characterPosition = mainCharacter.transform.position;
			arrowPosition = transform.position;
			Vector2 distance = characterPosition - arrowPosition;
			int chainCellCount = Mathf.CeilToInt(Mathf.Abs(distance.magnitude) / chainCellHeightWithAnchor);

			for(int i = 1; i < chainCellCount; i++)
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

            arrowWithChain = new GameObject();
            arrowWithChain.name = "ArrowWithChain";
            GetComponent<Transform>().parent = arrowWithChain.GetComponent<Transform>();
            newWholeChain.parent = arrowWithChain.GetComponent<Transform>();

            newCell.GetComponent<HingeJoint2D> ().connectedAnchor = new Vector2 (0.0f, 0.0f);
		    newCell.GetComponent<HingeJoint2D> ().anchor = new Vector2 (0.0f, 0.4f);
		} else {
			newCell.GetComponent<HingeJoint2D> ().connectedBody = oldCell.GetComponent<Rigidbody2D> ();
            newCell.GetComponent<HingeJoint2D>().connectedAnchor = new Vector2(0.0f, anchorBetweenCells);
            newCell.GetComponent<HingeJoint2D>().anchor = new Vector2(0.0f, anchorPlacemenent);
        }
		newCell.transform.parent = newWholeChain.transform;
		
        newCell.GetComponent<SpriteRenderer>().sortingOrder = 11;
		listOfChainCells.Add (newCell);
		oldCell = newCell;
		firstChainCellCreated = true;
	}
	
	public void attachCharacter()
	{
		mainCharacter.GetComponent<HingeJoint2D> ().enabled = true;
		mainCharacter.GetComponent<HingeJoint2D> ().connectedBody = oldCell.GetComponent<Rigidbody2D> ();
		mainCharacter.GetComponent<HingeJoint2D> ().connectedAnchor = new Vector2 (0.0f, anchorBetweenCells);
        mainCharacter.GetComponent<HingeJoint2D>().anchor = anchorFromCharacter;

        mainCharacter.GetComponent<ChainConnection>().IsCharacterAttachedToChain = true;
        mainCharacter.GetComponent<Run>().StopRunning();

		for (int i = 0; i < listOfChainCells.Count; i++) {
			Physics2D.IgnoreCollision (mainCharacter.GetComponent<Collider2D> (), listOfChainCells [i].GetComponent<Collider2D> ());
		}
		mainCharacter.GetComponent<MoveAlongRope> ().ListOfChainCells = listOfChainCells;
        mainCharacter.GetComponent<MoveAlongRope>().resetMovedDistance();
        GameObject.Find("CameraHolder").GetComponent<CountingArrows>().AddNewArrow(arrowWithChain, "Swing");
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
