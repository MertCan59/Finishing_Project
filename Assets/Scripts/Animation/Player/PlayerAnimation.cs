using UnityEngine;

public class PlayerAnimation : IAnimations
{
    public float WalkAnimation(float walkSpeed)
    {
        return Mathf.Abs(walkSpeed);
    }
    public bool JumpAnimation(bool canJumping, bool isJumping)
    {
        if(canJumping)
        {
            return isJumping = false;
        }
        return isJumping=true;
    }

}
