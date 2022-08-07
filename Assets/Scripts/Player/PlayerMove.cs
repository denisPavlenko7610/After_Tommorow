using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] [Range(0.0f, 0.5f)] float _moveSmoothTime = 0.3f;
    [SerializeField] float _gravity = -13f;
    [SerializeField] CharacterController _controller;
    [SerializeField] float _walkSpeed = 6f;
    [SerializeField] float _runSpeed = 12f;
    
    Vector2 _currentDir = Vector2.zero;
    Vector2 _currentDirVelocity = Vector2.zero;
    float _currentSpeed;
    float _velocityY;

    private void Start()
    {
        _currentSpeed = _walkSpeed;
    }

    public void UpdateMovement(Vector2 input)
    {
        var targetDirection = new Vector2(input.x, input.y);
        targetDirection.Normalize();
        _currentDir = Vector2.SmoothDamp(_currentDir, targetDirection, ref _currentDirVelocity, _moveSmoothTime);

        if (_controller.isGrounded)
            _velocityY = 0f;

        _velocityY += _gravity * Time.deltaTime;
        var velocity = (transform.forward * _currentDir.y + transform.right * _currentDir.x) * _currentSpeed +
                           Vector3.up * _velocityY;
        _controller.Move(velocity * Time.deltaTime);
    }

    public void Shift() => _currentSpeed = _runSpeed;

    public void UnShift() => _currentSpeed = _walkSpeed;
}