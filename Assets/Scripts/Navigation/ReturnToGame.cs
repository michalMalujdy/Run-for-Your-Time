using UnityEngine;
using System.Collections;

public class ReturnToGame : MonoBehaviour {

	float buttonLeft = Screen.width * 0.48f;
	float buttonRight = Screen.width * 0.65f;
	float buttonUp = Screen.height * 0.4f;
	float buttonDown = Screen.height * 0.3f;
    bool deactivateCanvas = false;

    public HardwareButtonsInGame HWButtonsComponent;
	public Canvas pauseCanvas;
    
	void Update()
	{
        //musze wylaczac canvas w nastepnej klatce, bo inaczej wali sie funkcja isButtonClicked
        if (deactivateCanvas)
        {
            HWButtonsComponent.PauseCanvasOn = false;
            deactivateCanvas = false;
            pauseCanvas.gameObject.SetActive(false);                       
        }
        
		if (GetComponent<Buttons> ().isButtonClicked (buttonLeft, buttonRight, buttonDown, buttonUp)) {
            deactivateCanvas = true;		
		}
	}
}
