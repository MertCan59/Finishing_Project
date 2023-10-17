using UnityEngine;
using System;
using UnityEngine.Events;

[CreateAssetMenu(fileName ="InputReader",menuName ="Input/Input Reader")]
public class InputReader:ScriptableObject
{
    public event UnityAction<Vector2> MoveEvent = delegate { };
    public event UnityAction<Vector2> JumpEvent=delegate { };
    public void OnMove(Vector2 value)
    {
        MoveEvent.Invoke(value);
    }
    public void OnJump(Vector2 jumpValue)
    {
        JumpEvent.Invoke(jumpValue);
    }
}

