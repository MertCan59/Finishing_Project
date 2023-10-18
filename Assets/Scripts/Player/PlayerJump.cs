using System;
using UnityEngine;

 public class PlayerJump : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    private new Rigidbody2D rigidbody;

    public Vector2 yVelocity;
    [SerializeField] private float jumpForce;
    [SerializeField] private float maxJumpHeight;
    public bool isJumping;
    public bool canJumping;

    private bool isGrounded;
    private CircleCollider2D circleCollider;
    [SerializeField] private LayerMask groundLayer;
    private Transform player;
    
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
        player = GetComponent<Transform>();
    }
    private void Start()
    {
         
    }
    private void Update()
    {
        Jump();
    }

    private void FixedUpdate()
    {
        GroundMovement();
    }
    void Jump()
    {
        var velocity=rigidbody.velocity;
        velocity.y=Mathf.Clamp(velocity.y,-maxJumpHeight,maxJumpHeight);
        rigidbody.velocity=velocity;
    }
    void GroundMovement()
    {
        var jumpVelocity = new Vector2(0f, jumpForce);
        isGrounded = Physics2D.OverlapCircle(player.position, circleCollider.radius, groundLayer);
        canJumping = isGrounded;
        if (yVelocity.y>0 && isGrounded)
        {
            rigidbody.velocity += jumpVelocity;
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
       yVelocity  = jump;
    }
}
    