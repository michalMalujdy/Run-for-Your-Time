using UnityEngine;
using System.Collections;

public class Finished : MonoBehaviour {


	public float xMin;
	public float xMax;
	public float yMin;
	public float yMax;

	public Transform FinishCanvas;
	public Transform ButtonCanvas;

	private Transform mainCharacter;
    private Run mainCharacterRunComponent;

    private bool levelFinished = false;

	private string levelFinishedText = "Level Finished!";

	Timer timer;

	// Use this for initialization
	void Start () {
		mainCharacter = GameObject.FindWithTag ("Player").GetComponent<Transform>();
		timer = GameObject.FindWithTag ("Timer").GetComponent<Timer>();
        mainCharacterRunComponent = mainCharacter.GetComponent<Run>();
	}
	
	// Update is called once per frame
	void Update () {
		if (mainCharacter.position.x >= xMin && mainCharacter.position.x <= xMax &&
		    mainCharacter.position.y >= yMin && mainCharacter.position.y <= yMax)
		{
			LevelFinishedAction ();
		}
	}

	public bool LevelFinished {
		get {
			return levelFinished;
		}
	}		

	void OnGUI()
	{
		//Ta funkcja wyswietlala napis level finished po prostu
		/*if (levelFinished) {
			int w = Screen.width;
			int h = Screen.height;
			Rect rect = new Rect (0, 0, w, h);

			GUIStyle style = new GUIStyle ();
			style.alignment = TextAnchor.MiddleCenter;
			style.fontSize = h / 10;
			style.normal.textColor = new Color (0.8f, 0.2f, 0.2f, 1.0f);
			GUI.Label (rect, levelFinishedText, style);
		}*/
	}

	private void LevelFinishedAction()
	{
		levelFinished = true;
		timer.StopTimer ();
		ButtonCanvas.gameObject.SetActive (false);
		FinishCanvas.gameObject.SetActive (true);
        mainCharacterRunComponent.StopRunning();
	}
}
