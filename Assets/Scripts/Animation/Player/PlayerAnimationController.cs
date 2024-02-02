using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private PlayerController playerController;
    private PlayerAnimation playerAnimation;
    private PlayerJump playerJump;
    private Animator animator;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        playerAnimation=new PlayerAnimation();
        animator = GetComponent<Animator>();
        playerJump = GetComponent<PlayerJump>();
    }
    private void Update()
    {
        AnimatorPlayer();
    }
    private void WalkAnimation(IAnimations animations)
    {
        animator.SetFloat("Speed",animations.WalkAnimation(Mathf.Abs(playerController.movementInput.x)));
    }
    private void JumpAnimation(IAnimations animations)
    {
      animator.SetBool("isJumping",animations.JumpAnimation(playerJump.canJumping,playerJump.isJumping,animator));
    }
    private void AnimatorPlayer()
    {
        WalkAnimation(playerAnimation);
        JumpAnimation(playerAnimation);
    }
}
