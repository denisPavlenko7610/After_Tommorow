using System.Collections;
using UnityEngine;

namespace AfterDestroy.Player
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Control settings")] [SerializeField]
        private Camera playerCamera;

        [SerializeField] private float mouseSensitivity = 3.5f;
        [SerializeField] private bool lookCursor = true;
        [SerializeField] private float walkSpeed = 6f;
        [SerializeField] [Range(0.0f, 0.5f)] private float moveSmoothTime = 0.3f;
        [SerializeField] [Range(0.0f, 0.5f)] private float mouseSmoothTime = 0.03f;
        [SerializeField] private float gravity = -13f;
        [SerializeField] private AnimationCurve jumpFallOff;
        [SerializeField] private float jumpMultiplier;
        [SerializeField] private float runSpeed;
        
        //Control settings
        private float _cameraPitch;
        private CharacterController _controller;
        private Vector2 _currentDir = Vector2.zero;
        private Vector2 _currentDirVelocity = Vector2.zero;
        private Vector2 _currentMouseDelta = Vector2.zero;
        private Vector2 _currentDeltaVelocity = Vector2.zero;
        private float _velocityY;
        private bool _isJumping;
        private float _currentSpeed;
        private bool _canMove = true;
        
        public void Awake()
        {
            _controller = GetComponent<CharacterController>();

            if (lookCursor)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }

        private void Update()
        {
            UpdateMouseLook();
            UpdateMovement();
        }

        public void SetPlayerMove(bool isCanMove)
        {
            _canMove = isCanMove;
        }

        private void UpdateMouseLook()
        {
            Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            _currentMouseDelta = Vector2.SmoothDamp(_currentMouseDelta, targetMouseDelta, ref _currentDeltaVelocity,
                mouseSmoothTime);
            _cameraPitch -= _currentMouseDelta.y * mouseSensitivity;
            _cameraPitch = Mathf.Clamp(_cameraPitch, -90.0f, 90.0f);
            playerCamera.transform.localEulerAngles = Vector3.right * _cameraPitch;
            transform.Rotate(Vector3.up * (_currentMouseDelta.x * mouseSensitivity));
        }

        private void UpdateMovement()
        {
            if (!_canMove)
                return;

            Vector2 targetDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            targetDirection.Normalize();

            _currentDir = Vector2.SmoothDamp(_currentDir, targetDirection, ref _currentDirVelocity, moveSmoothTime);

            if (_controller.isGrounded)
            {
                _velocityY = 0f;
            }

            _velocityY += gravity * Time.deltaTime;

            Vector3 velocity = (transform.forward * _currentDir.y + transform.right * _currentDir.x) * _currentSpeed +
                               Vector3.up * _velocityY;
            _controller.Move(velocity * Time.deltaTime);

            JumpInput();
            ShiftInput();
        }

        private void JumpInput()
        {
            if (Input.GetButtonDown("Jump") && _isJumping == false)
            {
                _isJumping = true;
                StartCoroutine(JumpEvent());
            }
        }

        private void ShiftInput()
        {
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                _currentSpeed = runSpeed;
            else
                _currentSpeed = walkSpeed;
        }

        private IEnumerator JumpEvent()
        {
            float timeInAir = 0f;
            do
            {
                float jumpForce = jumpFallOff.Evaluate(timeInAir);
                _controller.Move(Vector3.up * (jumpForce * jumpMultiplier * Time.deltaTime));
                timeInAir += Time.deltaTime;
                yield return null;
            } while (!_controller.isGrounded && _controller.collisionFlags != CollisionFlags.Above);

            _isJumping = false;
        }
    }
}