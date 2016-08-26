using UnityEngine;
using System.Collections;

public class Finished : MonoBehaviour {
    
	public Transform FinishCanvas;
	public Transform ButtonCanvas;

	private Transform mainCharacter;
    private Run mainCharacterRunComponent;

    private bool levelFinished = false;

	private string levelFinishedText = "Level Finished!";

	Timer timer;

    public bool LevelFinished
    {
        get
        {
            return levelFinished;
        }
    }

    // Use this for initialization
    void Start () {
		mainCharacter = GameObject.FindWithTag ("Player").GetComponent<Transform>();
		timer = GameObject.FindWithTag ("Timer").GetComponent<Timer>();
        mainCharacterRunComponent = mainCharacter.GetComponent<Run>();
	}
	
	// Update is called once per frame
	void Update () {
	}

    void OnTriggerEnter2D (Collider2D other)
    {
        LevelFinishedAction();
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
