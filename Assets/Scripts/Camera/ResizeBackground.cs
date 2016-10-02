using UnityEngine;
using System.Collections;

public class ResizeBackground : MonoBehaviour {

    private int imageWidthPixels;
    private int imageHeightPixels;

    
    // Use this for initialization
	void Start () {
        imageWidthPixels = GetComponent<SpriteRenderer>().sprite.texture.width;
        imageHeightPixels = GetComponent<SpriteRenderer>().sprite.texture.height;

        Debug.Log("Width: " + imageWidthPixels);
        Debug.Log("Height: " + imageHeightPixels);

        if(Screen.width >= Screen.height)
        {
            float ratio = (float) imageWidthPixels / Screen.width;

            transform.localScale = new Vector2(ratio, ratio);
        }
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
