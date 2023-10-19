﻿using System;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    private PlayerController _controller;
    private new Rigidbody2D rigidbody;

    public Vector2 yVelocity;
    [SerializeField] private float jumpForce;
    [SerializeField] private float maxJumpHeight;
    public bool isJumping;
    public bool canJumping;
    private CapsuleCollider2D capsuleCollider;
    private CircleCollider2D circleCollider;
    private bool isGrounded;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    private bool isWallSliding;
    private float wallSlidingSpeed = 1.5f;
    [SerializeField] private float timeSinceLastJump = 0f;
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        circleCollider = GetComponent<CircleCollider2D>();
        _controller = GetComponent<PlayerController>();
    }
    private void Update()
    {
        Jump(); 
        IsWalled();
        Debug.Log(Physics2D.OverlapCircle(circleCollider.bounds.center, circleCollider.bounds.size.x, wallLayer));
    }

    private void FixedUpdate()
    {
        GroundMovement();
        WallSlide();
    }
    void Jump()
    {
        var velocity = rigidbody.velocity;
        velocity.y = Mathf.Clamp(velocity.y, -maxJumpHeight, maxJumpHeight);
        rigidbody.velocity = velocity;
    }
    void GroundMovement()
    {
        var jumpVelocity = new Vector2(0f, jumpForce);
        isGrounded = Physics2D.OverlapCapsule(capsuleCollider.bounds.center, capsuleCollider.bounds.size, CapsuleDirection2D.Horizontal, 0f, groundLayer);
        canJumping = isGrounded;
        timeSinceLastJump += Time.fixedDeltaTime;
        if (yVelocity.y > 0 && isGrounded && timeSinceLastJump>2f)
        {
            rigidbody.velocity += jumpVelocity;
            timeSinceLastJump = 0f;
        }
    }
    private void OnEnable()
    {
        _inputReader.JumpEvent += OnJumpEvent;
    }
    private void OnDisable()
    {
        _inputReader.JumpEvent -= OnJumpEvent;
    }
    void OnJumpEvent(Vector2 jump)
    {
        yVelocity = jump;
    }
    private bool IsWalled()
    {
        return Physics2D.OverlapCircle(circleCollider.bounds.center, circleCollider.bounds.size.x, wallLayer);
    }

    private void WallSlide()
    {
        if (IsWalled() && !isGrounded && _controller.movementInput.x != 0f)
        {
            isWallSliding = true;
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, Mathf.Clamp(rigidbody.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        else
        {
            isWallSliding = false;
        }
    }
}