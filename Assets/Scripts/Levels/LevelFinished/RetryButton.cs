using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class RetryButton : MonoBehaviour {
    
    void Start()
    {

    }

    void Update()
    {

    }
     
    public void Ungabunga()
    {
        //Application.LoadLevel(Application.loadedLevel);
        Debug.Log("Reload scene");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
