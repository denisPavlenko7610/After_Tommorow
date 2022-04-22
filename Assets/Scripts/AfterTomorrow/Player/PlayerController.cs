using UnityEngine;
using Zenject;

namespace AfterDestroy.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] bool _lookCursor = true;
        [SerializeField] PlayerMove _playerMove;
        [SerializeField] PlayerLook _playerLook;
        [SerializeField] PlayerJump _playerJump;
        [SerializeField] private CheckInteractable _checkInteractable;
        float _cameraPitch;

        float _velocityY;
        float _currentSpeed;
        bool _canMove = true;
        PlayerInput _playerInput;
        Inventory.Inventory _inventory;

        [Inject]
        private void Construct(Inventory.Inventory inventory)
        {
            _inventory = inventory;
        }

        private void OnEnable()
        {
            _playerInput?.Enable();
        }

        private void OnDisable()
        {
            _playerInput?.Disable();
        }

        public void Awake()
        {
            if (_lookCursor)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }

            _playerInput = new PlayerInput();
        }

        private void Update()
        {
            _playerLook.MouseLook(_playerInput.Player.Look.ReadValue<Vector2>());
            UpdateMovement();

            if (_playerInput.Player.Inventory.triggered)
            {
                _inventory.SwitchInventoryStatus();
            }

            if (_playerInput.Player.Fire.triggered)
            {
                _checkInteractable.CheckLeftClick();
            }

            if (_playerInput.Player.Aim.triggered)
            {
                _checkInteractable.CheckRightClick();
            }
        }

        public void SetPlayerMove(bool isCanMove)
        {
            _canMove = isCanMove;
        }

        private void UpdateMovement()
        {
            if (!_canMove)
                return;

            _playerMove.UpdateMovement(_playerInput.Player.Move.ReadValue<Vector2>());

            if (_playerInput.Player.Jump.triggered)
            {
                _playerJump.JumpInput();
            }

            if (_playerInput.Player.Shift.IsPressed())
            {
                _playerMove.Shift();
            }
            else
            {
                _playerMove.UnShift();
            }
        }
    }
}