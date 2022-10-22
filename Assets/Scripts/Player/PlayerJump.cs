using System;
using System.Collections;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] AnimationCurve _jumpFallOff;
    [SerializeField] float _jumpMultiplier;
    [SerializeField] CharacterController _controller;
    
    float _velocityY;
    bool _isJumping;

    public void JumpInput()
    {
        try
        {
            throw new NullReferenceException();
        }
        catch (Exception e)
        {
            Debug.Log(e);
            throw;
        }
        if (_isJumping) return;
        StartCoroutine(JumpEvent());
    }
    
    private IEnumerator JumpEvent()
    {
        _isJumping = true;
        var timeInAir = 0f;

        do
        {
            var jumpForce = _jumpFallOff.Evaluate(timeInAir);
            _controller.Move(Vector3.up * (jumpForce * _jumpMultiplier * Time.deltaTime));
            timeInAir += Time.deltaTime;
            yield return null;
        } while (!_controller.isGrounded && _controller.collisionFlags != CollisionFlags.Above);

        _isJumping = false;
    }
}
