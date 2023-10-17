using UnityEngine;
using System;
public class Player : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private bool isFacingRight=true;
    private PlayerController playerController;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        Flip();
    }
    void Flip()
    {
        if(playerController.movementInput.x<0 && isFacingRight)
        {
            spriteRenderer.flipX = true;
        }
        else if(playerController.movementInput.x>0 && !isFacingRight)
        {
            spriteRenderer.flipX=false;
        }
        isFacingRight = !isFacingRight;
    }
}
    