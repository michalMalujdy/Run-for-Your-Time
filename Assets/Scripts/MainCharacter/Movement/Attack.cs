using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour {

    float buttonLeft = Screen.width * 0.88f;
    float buttonRight = Screen.width * 0.98f;
    float buttonUp = Screen.height * 0.33f;
    float buttonDown = Screen.height * 0.19f;

    private bool isAttacking = false;
    public Combat mainCharacterCombatComponent;
    private HandleAnimations mainCharacterAnimationComponent;

    public bool IsAttacking
    {
        get
        {
            return isAttacking;
        }

        set
        {
            isAttacking = value;
        }
    }

    // Use this for initialization
    void Start () {
        mainCharacterAnimationComponent = mainCharacterCombatComponent.GetComponent<HandleAnimations>();
	}
	
	// Update is called once per frame
	void Update () {
	    if(GetComponent<Buttons>().IsButtonOrKeyboardDown(buttonLeft, buttonRight, buttonDown, buttonUp, KeyCode.Z) && !mainCharacterAnimationComponent.getAttackClicked())
        {
            mainCharacterCombatComponent.MeleeAttack();
        }
	}
}
