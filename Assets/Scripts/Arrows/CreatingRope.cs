using UnityEngine;
using System.Collections;

public class CreatingRope : MonoBehaviour {

	public Transform chainCellPrefab;
	//potrzebne żeby podłączyć nowy element łańcucha do poprzedniego. Inaczej poprzednia stworzona komórka łańcucha.
	Transform oldCell;
	//Zakotwiczenie przy strzale (tylko 1 komórka)
	float arrowAnchor = 0.0f;
	//Zakotwiczenie przy pozostałych komórkach.
	float anchorBetweenCells = -0.8f;
	//kąt pod jakim mają się tworzyć komórki, pobierany bezpośrednio z kąta strzły
	float alpha;
	//Obecnie przeleciany przez strzałę dystans od utworzenia ostatniej komórki
	Vector2 fledDistance = new Vector2 (0.0f,0.0f);
	//Wykorzystane do sprawdzenia czy wlaczany jest tryb swing
	ShootingMode mainCharacter;
	private float chainCellHeight;
	float magnitude;
	private bool isHit = false;

	void Start()
	{
		chainCellHeight = chainCellPrefab.GetComponent<Renderer> ().bounds.size.y;
		alpha = GameObject.Find ("ScreenInputZone").GetComponent<OnDrag> ().alpha;
		mainCharacter = GameObject.FindGameObjectWithTag ("Player").GetComponent<ShootingMode> ();
		if (mainCharacter.SwingMode)
		{
			//Jeśli jest włączony tryb swing, to tworze nowa strzale. Miejsce utworzenia jest bez znaczenia, bo po odpowiednim podpieciu anchorBetweenCells utworzy się łączenie między komórkami i gra przeniesie w odpowiednie miejsce obecną komórkę.
			Transform newCell = (Transform)Instantiate (chainCellPrefab, new Vector3 (0.0f, 0.0f, 0.0f), Quaternion.identity);
			newCell.GetComponent<HingeJoint2D> ().connectedBody = this.GetComponent<Rigidbody2D> ();
			newCell.GetComponent<HingeJoint2D> ().connectedAnchor = new Vector2 (0.0f, arrowAnchor);
			oldCell = newCell;
		}
	}

	void Update () {
		//Zapisuje odleglosc ktora przeleciala strzala do zmiennej magnitude, bo potrzebuje maduł wektora który przeleciała
		//magnitude = fledDistance.magnitude;

		magnitude += GetComponent<Shoot> ().DerivatePositionVector.magnitude;



		//jeśli moduł większy, niż długość boku komórki i jest włączany tryb Swing, to tworzymy kolejne komórki
		if (magnitude >= chainCellHeight && mainCharacter.SwingMode && !isHit) {
			//Sprawdzam ile razy przeleciany dystans jest wiekszy od dl boku komórki. Zaokrąglam w dół rzutowaniem na int
			int timesLarger = (int) (magnitude / chainCellHeight);
			//tworzę tyle komórek ile wyliczyłem wyżej
			for (int i = 0; i < timesLarger; i++) {
				createChainCell ();
			}
			//Teraz resztę z dzielenia odległości przelecianej i długości boku komórki ustalam jako wartosc ktora zostala przeleciana, ale nie wliczono jej do stworzenia nowej komórki
			magnitude = timesLarger * chainCellHeight;
		}
		//Debug.Log ("Magnitude: " + magnitude);
	}

	void createChainCell()
	{
		Transform newCell = (Transform) Instantiate (chainCellPrefab, new Vector3 (0.0f, 0.0f, 0.0f), Quaternion.identity);
		newCell.GetComponent<HingeJoint2D> ().connectedBody = oldCell.GetComponent<Rigidbody2D> ();
		newCell.GetComponent<HingeJoint2D> ().connectedAnchor = new Vector2 (0.0f, anchorBetweenCells);
		oldCell = newCell;
	}

	public void attachCharacter()
	{
		mainCharacter.GetComponent<HingeJoint2D> ().enabled = true;
		mainCharacter.GetComponent<HingeJoint2D> ().connectedBody = oldCell.GetComponent<Rigidbody2D> ();
		mainCharacter.GetComponent<HingeJoint2D> ().connectedAnchor = new Vector2 (0.0f, anchorBetweenCells);
	}

	public bool IsHit {
		get {
			return isHit;
		}
		set {
			isHit = value;
		}
	}
}
