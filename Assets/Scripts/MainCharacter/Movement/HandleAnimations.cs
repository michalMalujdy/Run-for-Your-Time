using UnityEngine;
using System.Collections;

public class HandleAnimations : MonoBehaviour {

	private Animator characterAnimator;
	private Run characterRun;
	private SpriteRenderer rendererComponent;
    private Run runComponent;

    private bool blockResettingState = false;
	private bool previusRunningState = false;

	public Buttons runButton;

	private string stateNr = "StateNumber";
	private string shortattack = "ShortAttack";
    private string isDead = "isCharacterDead";

	private int attack = 3;

	public AnimationClip ShortKillAnimation;

	// Use this for initialization
	void Awake() {
		characterAnimator = GetComponent <Animator> ();
		characterRun = GetComponent <Run> ();
		rendererComponent = GetComponent <SpriteRenderer> ();
        runComponent = GetComponent<Run>();
	}
	
	// Update is called once per frame
	void Update () {

		CheckForDirection ();

		if ((characterRun.IsRunningForeward || characterRun.IsRunningBack) && characterAnimator.GetInteger(stateNr) != 3 && characterAnimator.GetInteger (stateNr) != 1) {
			characterAnimator.SetInteger (stateNr, 1);	
		}
		else  if (!characterRun.IsRunningForeward && !characterRun.IsRunningBack && characterAnimator.GetInteger(stateNr) != 3 && characterAnimator.GetInteger(stateNr) != 0){					
			characterAnimator.SetInteger (stateNr, 0);
		}
	}

	private void CheckForDirection()
	{
		if (characterRun.PreviousDirection == -1) {
			characterRun.GetComponent <SpriteRenderer> ().flipX = true;
		} 

		else if(characterRun.PreviousDirection == 1){
			characterRun.GetComponent <SpriteRenderer> ().flipX = false;
		}
	}

	public void KillingAnimation()
	{
		characterAnimator.SetInteger (stateNr, attack);
		SwitchAnimationAfterTime (0, ShortKillAnimation.length - 0.5f);
	}

    public void DeadAnimation()
    {
        characterAnimator.SetBool(isDead, true);
        runComponent.StopRunning();
    }


	private void SwitchAnimationAfterTime (int nextAnimationNr, float time)
	{
		StartCoroutine (switchAnimation (nextAnimationNr,time));
	}
	private IEnumerator switchAnimation(int nextAnimationNr, float time)
	{
		yield return new WaitForSeconds (time);
		characterAnimator.SetInteger (stateNr, nextAnimationNr);
	}
}
