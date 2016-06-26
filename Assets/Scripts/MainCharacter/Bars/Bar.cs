using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Bar : MonoBehaviour {


	private float status;
	public float statusMaxValue; // ile ma maksymalnie wartosc przechowywania w pasku
	public float statusOnStart;
	public RectTransform statusRect;
	public Image statusImage;

	private float speed = 0.1f; //speed okresla szybkosc spadania/narastania paska statusu
	private float floatMargin = 0.001f;// margines wykorzystanay do porownania flotow. Oznacza ze sa porownane z taka wlasnie dokladnoscia
	private float statusAnimationLeft = 0.0f; //wartosc ktora jeszcze musi sie zmienic powoli

	private float fixedYPos; //fixedYPos to stała wartość lewego gornego rogu przesuwanego paska statusu, jest ona nieznienna

	public float maxColorValue;
	public float minColorValue;
	private float currentColor;


    void Start()
	{
		fixedYPos = statusRect.position.y;
		status = statusOnStart;
	}

	void Update()
	{
		if (statusImage.fillAmount - status / statusMaxValue > floatMargin || statusImage.fillAmount - status / statusMaxValue < -floatMargin) {
			Animate ();
		}
	}

	private void MatchColor()
	{
		currentColor = (minColorValue + (maxColorValue - minColorValue) * statusImage.fillAmount) / 255.0f;
		statusImage.color = new Color (currentColor, statusImage.color.g, statusImage.color.b);
	}

	private void Animate ()
	{
		if (statusImage.fillAmount > status / statusMaxValue) {
			statusImage.fillAmount -= speed * Time.deltaTime;				
		} 
		else {
			statusImage.fillAmount += speed * Time.deltaTime;	
		}
		MatchColor ();

	}

	public void DecrementStatus (float value)
	{
		status -= value;

		if (status < 0.0f) { 
			status = 0.0f;
		} 
		else if (status > statusMaxValue) {
			status = statusMaxValue;
		}
	}

	public void IncrementStatus (float value)
	{
		status += value;

		if (status < 0.0f) {
			status = 0.0f;
		} 
		else if (status > statusMaxValue) {
			status = statusMaxValue;
		}
	}

	public void SetStatus (float value)
	{
		status = value;

		if (status < 0.0f) {
			status = 0.0f;
		} 
		else if (status > statusMaxValue) {
			status = statusMaxValue;
		}
	}

    public float Status
    {
        get
        {
            return status;
        }

        set
        {
            status = value;
        }
    }

}

