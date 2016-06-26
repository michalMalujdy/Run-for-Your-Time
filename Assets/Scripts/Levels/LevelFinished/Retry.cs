using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Retry : MonoBehaviour {

	float buttonLeft = Screen.width * 0.3f;
	float buttonRight = Screen.width * 0.53f;
	float buttonUp = Screen.height * 0.4f;
	float buttonDown = Screen.height * 0.3f;

	void Update()
	{
		//SceneManager.LoadScene (0);
		if (GetComponent<Buttons>().isButtonClicked (buttonLeft, buttonRight, buttonDown, buttonUp)) {
			Application.LoadLevel (Application.loadedLevel);
		}
	}
}
