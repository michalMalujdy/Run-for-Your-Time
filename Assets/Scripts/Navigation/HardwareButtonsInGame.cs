using UnityEngine;
using System.Collections;

public class HardwareButtonsInGame : MonoBehaviour {

	private bool buttonPressed = false;
	public Canvas pauseCanvas;
	private bool pauseCanvasOn = false;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void LateUpdate () {
		#if UNITY_EDITOR

		if (Input.GetKeyDown(KeyCode.Space) && !buttonPressed) {

			if(!pauseCanvasOn){
				pauseCanvas.gameObject.SetActive(true);
				pauseCanvasOn = true;
			}
			else
			{
				pauseCanvas.gameObject.SetActive(false);
				pauseCanvasOn = false;
            }

			buttonPressed = true;
		}

		if (!Input.GetMouseButtonDown(1) && buttonPressed) {
			buttonPressed = !buttonPressed;
		}
		#endif

		if (Input.GetKey (KeyCode.Escape) && !buttonPressed) {

			if(!pauseCanvasOn){
				pauseCanvas.gameObject.SetActive(true);
				pauseCanvasOn = true;
			}
			else
			{
				pauseCanvas.gameObject.SetActive(false);
				pauseCanvasOn = false;
			}

			buttonPressed = true;
		}

		if (!Input.GetKey (KeyCode.Escape) && buttonPressed) {
			buttonPressed = !buttonPressed;
		}
	}

    public bool PauseCanvasOn
    {
        get
        {
            return pauseCanvasOn;
        }

        set
        {
            pauseCanvasOn = value;
        }
    }
}
