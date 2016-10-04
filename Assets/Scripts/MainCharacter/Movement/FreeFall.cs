using UnityEngine;
using System.Collections;

public class FreeFall : MonoBehaviour
{

    private HandleAnimations characterAnimations;
    private ChainConnection chainConnectionComponent;
    public Jump jumpComponent;
    public GroundCollider groundColliderComponent;
    private bool isFalling = false;

    // Use this for initialization
    void Start()
    {
        characterAnimations = GetComponent<HandleAnimations>();
        chainConnectionComponent = GetComponent<ChainConnection>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!jumpComponent.IsJumping && !groundColliderComponent.IsGrounded && !chainConnectionComponent.IsCharacterAttachedToChain)
        {
            IsFalling = true;
        }
        else
        {
            IsFalling = false;
        }
    }
    public bool IsFalling
    {
        get
        {
            return isFalling;
        }
        set
        {
            isFalling = value;
            characterAnimations.setIsFalling(value);
        }
    }
}
