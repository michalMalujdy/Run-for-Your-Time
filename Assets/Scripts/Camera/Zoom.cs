using UnityEngine;
using System.Collections;

public class Zoom : MonoBehaviour {

    private float finalSize = 2.5f;
    private float finalY = -1.8f;

    private float startSize = 6.5f;
    private float startY = 0.0f;

    private float sizeSpeed;
    private float ySpeed = 0.1f;

    private bool isZoomingIn = false;
    private bool isZoomingOut = false;

    private Camera cameraComponent;
    private Transform transformComponent;


    public bool getIsZoomingIn()
    {
        return isZoomingIn;
    }

    // Use this for initialization
    void Start () {
        float stepsNumber = Mathf.Abs(finalY / ySpeed);
        sizeSpeed = Mathf.Abs(startSize - finalSize) / stepsNumber;

        cameraComponent = GetComponent<Camera>();
        transformComponent = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
	    if (isZoomingIn)
        {
            if(cameraComponent.orthographicSize > finalSize)
            {
                cameraComponent.orthographicSize -= sizeSpeed;
            }
            if (transformComponent.localPosition.y > finalY)
            {
                transformComponent.localPosition = new Vector3 (transformComponent.localPosition.x, transformComponent.localPosition.y - ySpeed, transformComponent.localPosition.z);
            }
            else
            {
                isZoomingIn = false;
            }
        }

        else if(isZoomingOut)
        {
            if(cameraComponent.orthographicSize < startSize)
            {
                cameraComponent.orthographicSize += sizeSpeed;
            }
            if (transformComponent.localPosition.y < startY)
            {
                transformComponent.localPosition = new Vector3(transformComponent.localPosition.x, transformComponent.localPosition.y + ySpeed, transformComponent.localPosition.z);
            }
            else
            {
                isZoomingOut = false;
            }
            if (transformComponent.localPosition.y < startY && transformComponent.localPosition.y > -ySpeed)
            {
                transformComponent.localPosition = new Vector3(transformComponent.localPosition.x, startY, transformComponent.localPosition.z);
            }
        }

	}

    public void ZoomIn()
    {
        isZoomingIn = true;
    }

    public void ZoomOut()
    {
        isZoomingOut = true;
    }
}
