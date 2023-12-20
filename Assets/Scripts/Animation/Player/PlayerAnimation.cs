using UnityEngine;

public class PlayerAnimation : IAnimations
{
    public float WalkAnimation(float walkSpeed)
    {
        return Mathf.Abs(walkSpeed);
    }
    public bool JumpAnimation(bool canJumping, bool isJumping,Animator animator)
    {
        if(canJumping)
        {
            animator.SetTrigger("JumpTrigger");
            return false;
        }
        return true;
    }

    public bool AttackAnimation(bool isAttacking, float distance)
    {
        if(isAttacking && distance<=1)
        {
            return isAttacking=true;
        }
        else
        {
            return isAttacking=false;
        }
    }
}
