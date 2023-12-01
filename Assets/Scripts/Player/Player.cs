using UnityEngine;
using System;
public class Player : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private bool isFacingRight=true;
    private PlayerController playerController;
    private new Rigidbody2D rigidbody;
    private GameObject player;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
        player = GetComponent<GameObject>();
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Fall"))
        {
            rigidbody.interpolation = RigidbodyInterpolation2D.Interpolate;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        rigidbody.interpolation = RigidbodyInterpolation2D.None;
    }
}
    