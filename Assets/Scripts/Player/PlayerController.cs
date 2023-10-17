using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Vector2 movementInput;
    public Vector3 movementVector;
    [SerializeField] private float TargetSpeed;
    [SerializeField] private InputReader _inputReader;
    private void OnEnable()
    {
        _inputReader.MoveEvent += OnMoveEvent;
    }
    private void OnDisable()
    {
        _inputReader.MoveEvent -= OnMoveEvent;
    }
    private void Update()
    {
        ComputeMovement();
    }
    private void FixedUpdate()
    {
        Move();
    }
    private void Move()
    {
        transform.position += movementVector;
    }

    private void ComputeMovement()
    {
        float targetSpeed = TargetSpeed;

        var move = new Vector3(movementInput.x, movementInput.y,0f);
        movementVector = targetSpeed * Time.fixedDeltaTime * move;
    }
    private void OnMoveEvent(Vector2 move)
    {
        movementInput = move;
    }
}
