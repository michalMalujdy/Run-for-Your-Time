using UnityEngine;
using System.Collections;

public class TrapTrigger : MonoBehaviour {

    private TrapExecutor executor;

    public TrapExecutor Executor
    {
        set
        {
            executor = value;
        }
        get
        {
            return executor;
        }
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            executor.PlayerStompOnTrigger();
        }
    }
}
