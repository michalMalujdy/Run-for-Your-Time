using UnityEngine;
using System.Collections;

public class OnDrag : MonoBehaviour {

	private int ID;
	private Vector2 startPosition;
	private Vector2 currentPosition;
	private Vector2 endPosition;
	private Vector2 distance;
	private bool hasBeenTracked = false;
	private float dragMin = Screen.width * 0.05f;
	private float leftBorder = Screen.width * 0.27f;
	private float downBorder = Screen.height * 0.17f;
    private float rightBorder = Screen.width * 0.87f;

    public GameObject arrowPrefab;
	public GameObject mainCharacter;
	private float mainCharacterCircle = 1.2f;
	private Vector2 arrowVelocity;
	public float alpha;
	private Touch findedEndedTouch;
    public ChainConnection chainConnectionComponent;

	// Use this for initialization
	void Start () {
	    

	}
	
	// Update is called once per frame
	void Update () {
        //DragModeShoot();
        if(!chainConnectionComponent.IsCharacterAttachedToChain)
        {
            TapModeShoot();
        }
	}

    private void DragModeShoot()
    {
        //Jest sprawdzany tylko jeden dotyk dla strzelania, wiec warunek czy juz jakis jest sledzony
        if (!hasBeenTracked)
        {
            //przejscie po petli wszystkich dotykow i sprawdzenie czy ktorys sie zaczal i jest w porzadanym obszarze
            foreach (Touch touch in Input.touches)
            {
                if ((touch.position.x >= leftBorder && touch.position.y >= downBorder && touch.position.x <= rightBorder) || (touch.position.y > Screen.height * 0.33f && touch.position.x >= leftBorder))
                {
                    if (touch.phase == TouchPhase.Began)
                    {
                        //Jesli tak, to zbieram jego id zeby mozna bylo go znalezc, pozycje poczatkowa zeby ustalic wektor przesuniecia palcem i zaznaczam ze dotyk jest sledzony
                        ID = touch.fingerId;
                        startPosition = touch.position;
                        hasBeenTracked = true;
                    }
                }
            }
        }
        else
        {
            //Wykonuje sie gdy dotyk jest sledzony
            FindByFingerID(ID);
            Touch touch = findedEndedTouch;
            currentPosition = touch.position;
            distance = currentPosition - startPosition;
            //Jesli palec zostal puszczony i pokonana droga jest wieksza od minimum
            if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                if (distance.magnitude >= dragMin)
                {
                    PrepareArrowToShoot(distance);
                }
                hasBeenTracked = false;
                endPosition = new Vector2(0.0f, 0.0f);
            }
        }
    }

    private void TapModeShoot()
    {
        foreach (Touch touch in Input.touches)
        {
            if ((touch.position.x >= leftBorder && touch.position.y >= downBorder && touch.position.x <= rightBorder) || (touch.position.y > Screen.height * 0.33f && touch.position.x >= leftBorder))
            {
                if (touch.phase == TouchPhase.Began)
                {
                    distance = Camera.main.ScreenToWorldPoint(touch.position) - mainCharacter.transform.position;
                    PrepareArrowToShoot(distance);
                    return;
                }
            }
        }
    }

    public void TapShoot(Vector2 touchPosition)
    {
        distance = Camera.main.ScreenToWorldPoint(touchPosition) - mainCharacter.transform.position;
        PrepareArrowToShoot(distance);
    }

    public void PrepareArrowToShoot(Vector2 distance)
    {
        //Zapisuje WEKTOR predkosci i go skracam
        arrowVelocity = Vectors.ShortenVector(distance, dragMin);
        //ustawiam poczatek wystrzalu. mainCharacterCircle to odleglosc od bohatera z jakiej ma byc wypuszczona strzala
        Vector2 arrowStart = Vectors.ShortenVector(distance, MainCharacterCircle);
        //Obliczam pod jakim katem alpha ma byc wypuszczona do strzalu
        alpha = Mathf.Asin(distance.y / distance.magnitude) * 180 / Mathf.PI;
        //Kat alpha sie jebie gdy strzela sie w strone II i III ćwiartki bo skladowa x jest ujemna wiec naprawiam to 
        if (distance.x < 0)
        {
            alpha = 180.0f - alpha;
        }
        //Tworzymy nową strzałę w pozycji bohatera o mainCharacterCircle odleglą od niego i pod kątem alpha
        Instantiate(arrowPrefab, new Vector2(mainCharacter.transform.localPosition.x + arrowStart.x, mainCharacter.transform.localPosition.y + arrowStart.y), Quaternion.Euler(0.0f, 0.0f, alpha));

    }

    void FindByFingerID(int ID){
		foreach (Touch touch in Input.touches) {
			if(touch.fingerId == ID){
				findedEndedTouch = touch;
			}
		}
	}

	public Vector2 Distance {
		get {
			return distance;
		}
	}

	public Vector2 EndPosition {
		get {
			return endPosition;
		}
	}

	public Vector2 ArrowVelocity {
		get {
			return arrowVelocity;
		}
	}

    public float MainCharacterCircle
    {
        get
        {
            return mainCharacterCircle;
        }

        set
        {
            mainCharacterCircle = value;
        }
    }
}
