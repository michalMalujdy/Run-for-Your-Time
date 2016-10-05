using UnityEngine;
using System.Collections;

public class ResizeBackground : MonoBehaviour {

    private float imageWidthPixels;
    private int imageHeightPixels;

    public Camera mainCamera;
    
    // Use this for initialization
	void Start () {
        imageWidthPixels = GetComponent<SpriteRenderer>().sprite.texture.width;
        imageHeightPixels = GetComponent<SpriteRenderer>().sprite.texture.height;

        

        if(Screen.width >= Screen.height)
        {
            float ratio = (float) Screen.width  / imageWidthPixels;


            transform.localScale = new Vector2(ratio, ratio);
        }
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
