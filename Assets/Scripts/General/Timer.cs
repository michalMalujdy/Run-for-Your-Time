using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {

	private float startTime;
	private float passedTime = 0.0f;
	private bool countingTime = true;

	private int miliseconds = 0;
	private int seconds = 0;
	private int minutes = 0;
	private int hours = 0;

	private string text;


	// Use this for initialization
	void Start () {	
	}
	
	// Update is called once per frame
	void Update () {
		if (countingTime) {
			passedTime += Time.deltaTime;
			FormatTime ();
		}	
	}

	void OnGUI()
	{
		int w = Screen.width, h = Screen.height;

		GUIStyle style = new GUIStyle();

		Rect rect = new Rect(0, 0, w, h * 2 / 100);
		style.alignment = TextAnchor.UpperRight;
		style.fontSize = h * 2 / 100 * 2;
		style.normal.textColor = new Color (0.0f, 1.0f, 0.0f, 1.0f);
		if (hours != 0) {
			text = string.Format ("{0:0}h {1:0}m {2:0}s {3:0}ms", hours, minutes, seconds, miliseconds);
		} 
		else if (minutes != 0) {
			text = string.Format ("{0:0}m {1:0}s {2:0}ms", minutes, seconds, miliseconds);
		}
		else {
			text = string.Format ("{0:0}s {1:0}ms", seconds, miliseconds);
		}

		GUI.Label(rect, text, style);
	}

	private void FormatTime()
	{
		if (passedTime >= 0.1) {
			miliseconds += 1;
			passedTime = 0;
		}

		if (miliseconds == 10) {
			seconds += 1;
			miliseconds = 0;
		}

		if (seconds == 60) {
			minutes += 1;
			seconds = 0;
		}

		if (minutes == 60)
		{
			hours += 1;
			minutes = 0;
		}
	}

	public float PassedTime {
		get {
			return passedTime;
		}
	}

	public void ResetTimer()
	{
		passedTime = 0.0f;
	}

	public void StopTimer()
	{
		countingTime = false;
	}

	public void StartTimer()
	{
		countingTime = true;
	}
}
