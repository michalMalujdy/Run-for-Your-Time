using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class RetryButton : MonoBehaviour {
    
    
    public void Clicked()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
}
