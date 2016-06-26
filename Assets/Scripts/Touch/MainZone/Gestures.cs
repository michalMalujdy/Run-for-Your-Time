using UnityEngine;
using System.Collections;

public class Gestures : MonoBehaviour {

    //Gestures Zone
    float ZoneLeft = Screen.width * 0.27f;
    float ZoneDown = Screen.height * 0.12f;

    private float tapDistanceX = 60.0f;
	private float tapDistanceY = 100.0f;
	private int tapCounter = 0;
	private float timeBetweenTaps = 0.0f;
	private float maxTimeBetweenTaps = 0.5f;
	private bool isTimercounting = false;
	private Vector2 previousTapCoordinates;
	private bool tapedTwice = false;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (isTimercounting) {
			CountTimeBetweenTaps ();
		}
	}

	public bool CheckForDoubleTap()
	{
		foreach (Touch touch in Input.touches) {
            if (IsGestureInZone(touch))
            {
                if (touch.phase == TouchPhase.Began)
                {
                    if (tapCounter == 0)
                    {
                        previousTapCoordinates = touch.position;
                        tapCounter++;
                        isTimercounting = true;
                        return false;
                    }
                    else if (tapCounter == 1)
                    {
                        if (touch.position.x >= previousTapCoordinates.x - tapDistanceX && touch.position.x <= previousTapCoordinates.x + tapDistanceX &&
                            touch.position.y >= previousTapCoordinates.y - tapDistanceY && touch.position.y <= previousTapCoordinates.y + tapDistanceY)
                        {
                            tapCounter++;
                        }
                    }
                }
            }
		}

		if (tapCounter == 2) {
			isTimercounting = false;
			timeBetweenTaps = 0.0f;
			tapCounter = 0;
			return true;
		} else {
			return false;
		}
	}

	private void CountTimeBetweenTaps()
	{
		timeBetweenTaps += Time.deltaTime;

		if (timeBetweenTaps > maxTimeBetweenTaps) {
			isTimercounting = false;
			timeBetweenTaps = 0.0f;
			tapCounter = 0;
		}
	}

    private bool IsGestureInZone(Touch touch)
    {
        if (touch.position.x >= ZoneLeft && touch.position.y >= ZoneDown)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
