﻿using UnityEngine; 
public interface IAnimations
{
    float WalkAnimation(float walkSpeed);
    bool JumpAnimation(bool canJumping,bool isJumping,Animator animator);
}
