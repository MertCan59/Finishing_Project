using System;
using System.Collections;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    private PlayerController _controller;
    private Vector2 dashInput;
    private bool canDash = true;
    [SerializeField] private float dashingPower = 2f;
    [SerializeField] private float dashingTime = 0.9f;
    private float dashingCooldown = 1f;
    private TrailRenderer trailRenderer;
    private new Rigidbody2D rigidbody;

    public bool isDashing;
    private void Awake()
    {
        trailRenderer = GetComponent<TrailRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
        _controller = GetComponent<PlayerController>();
    }
    private void Update()
    {
        StartDash();
    }
    private void OnEnable()
    {
        _inputReader.DashEvent += OnDashEvent;
    }
    private void OnDisable()
    {
        _inputReader.DashEvent -= OnDashEvent;
    }
    void OnDashEvent(Vector2 dash)
    {
        dashInput = dash;
    }
    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rigidbody.gravityScale;
        rigidbody.gravityScale = 0f;
        rigidbody.AddForce(new Vector2(_controller.movementInput.x * dashingPower, 0f));
        trailRenderer.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        trailRenderer.emitting = false;
        rigidbody.velocity = Vector2.zero;
        rigidbody.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
    private void StartDash()
    {
        if (dashInput.x > 0 && canDash)
        {
            canDash = false;
            StartCoroutine(Dash());
        }
    }
}
