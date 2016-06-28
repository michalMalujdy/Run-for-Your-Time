using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CountingArrows : MonoBehaviour {
    
    public List<GameObject> arrowsList = new List<GameObject>();
    private int maxArrowNumber = 10;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void AddNewArrow(GameObject item)
    {
        arrowsList.Add(item);
        if(arrowsList.Count > maxArrowNumber)
        {
            Destroy(arrowsList[0]);
            arrowsList.RemoveAt(0);
        }
        Debug.Log("Arrows Number=" + arrowsList.Count);
    }
}
