using UnityEngine;
using System.Collections;

public class TrapExecutor : MonoBehaviour {

    public TrapTrigger trigger;
    public Transform netPrefab;
    private Transform createdNet;
    private float yCreationOffset = 1.0f;
    private float xCreationOffset = 1.0f;

    private bool quickTimeEventMode = false;
    public bool QuickTimeEventMode { get { return quickTimeEventMode; } set { quickTimeEventMode = value; } }

    private Attack attackButton;
    private Transform player;

    public QuickTimeEvent quickTimeEventHandler;

    int fallingNet = QuickTimeEvent.fallingNet;
    int ballWithSpikes = QuickTimeEvent.ballWithSpikes;

    public enum TrapType
    {
        fallingNet, ballWithSpikes
    }
    public TrapType chosenTrapType;

    // Use this for initialization
    void Start () {
        trigger.Executor = this;

        player = GameObject.FindWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void PlayerStompOnTrigger()
    {
        if(chosenTrapType == TrapType.fallingNet)
        {
            createdNet = (Transform) Instantiate(netPrefab, new Vector2(player.position.x + xCreationOffset, transform.position.y + yCreationOffset), Quaternion.Euler(new Vector2(0.0f, 0.0f)));
            player.GetComponent<DisableEnableMovement>().disableMovement();
            quickTimeEventHandler = GameObject.FindWithTag("QuickTimeEvent").GetComponent<QuickTimeEvent>();

            StartCoroutine(StartQTEAfterTime(2.0f));
        }
    }

    IEnumerator StartQTEAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        QTE();
    }

    private void QTE()
    {
        quickTimeEventHandler.StartQTE(QuickTimeEvent.fallingNet, this, false, "AttackButton");
    }
}
