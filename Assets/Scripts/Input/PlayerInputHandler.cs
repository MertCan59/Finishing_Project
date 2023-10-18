using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    public void OnMove(InputValue inputValue)
    {
        _inputReader.OnMove(inputValue.Get<Vector2>());
    }
    public void OnJump(InputValue jumpValue)
    {
        _inputReader.OnJump(jumpValue.Get<Vector2>());
    }
    public void OnDash(InputValue dashValue)
    {
        _inputReader.OnDash(dashValue.Get<Vector2>());
    }
}
