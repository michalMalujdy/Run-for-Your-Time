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
    private string isDead = "isCharacterDead";
    private string isAttackClicked = "AttackClicked";
    private string isCharacterAttacked = "IsCharacterAttacked";
    private string isNoEnemyAround = "NoEnemyAround";
    private string isJumping = "IsJumping";
    private string isFalling = "IsFalling";

    private int attack = 3;
    private int voidAttack = 4;

    public AnimationClip voidAttackAnimationClip;

    public void setAttackClicked (bool value)
    {
        characterAnimator.SetBool(isAttackClicked, value);
    }

    public void triggerAttackClicked ()
    {
        characterAnimator.SetBool(isAttackClicked, true);
        SwitchBooleanAfterTime(isAttackClicked, false, voidAttackAnimationClip.length / 2.5f);
    }

    public bool getAttackClicked ()
    {
        return characterAnimator.GetBool(isAttackClicked);
    } 

    public void setIsCharacterAttacked(bool value)
    {
        characterAnimator.SetBool(isCharacterAttacked, value);
    }

    public bool getIsCharacterAttacked()
    {
        return characterAnimator.GetBool(isCharacterAttacked);
    }

    public void setNoEnemyAround(bool value)
    {
        characterAnimator.SetBool(isNoEnemyAround, value);
    }

    public bool getNoEnemyAround()
    {
        return characterAnimator.GetBool(isNoEnemyAround);
    }

    public void setIsJumping(bool value)
    {
        characterAnimator.SetBool(isJumping, value);
    }

    public bool getIsJumping()
    {
        return characterAnimator.GetBool(isJumping);
    }

    public void setIsFalling(bool value)
    {
        characterAnimator.SetBool(isFalling, value);
    }

    public bool getIsFalling()
    {
        return characterAnimator.GetBool(isFalling);
    }


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

    //WLASCIWE ANIMACJE POSTACI

	public void KillingAnimation()
	{
		characterAnimator.SetInteger (stateNr, attack);
		SwitchAnimationAfterTime (0, voidAttackAnimationClip.length - 0.5f);
	}

    public void VoidAttackAnimation()
    {
        characterAnimator.SetInteger(stateNr, attack);
        SwitchAnimationAfterTime(0, voidAttackAnimationClip.length - 0.5f);
    }

    public void AttackWithNoEnemy()
    {

    }

    public void DeadAnimation()
    {
        characterAnimator.SetBool(isDead, true);
        runComponent.StopRunning();
    }

    private void SwitchBooleanAfterTime(string name, bool state, float time)
    {
        StartCoroutine(SwitchBoolean(name, state, time));
    }

    private IEnumerator SwitchBoolean (string name, bool state, float time)
    {
        yield return new WaitForSeconds(time);
        characterAnimator.SetBool(name, state);
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
