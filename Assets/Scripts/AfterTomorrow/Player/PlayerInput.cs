using AfterDestroy.Player;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace AfterTomorrow.Player
{
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] bool _lookCursor = true;
        [SerializeField] PlayerMove _playerMove;
        [SerializeField] PlayerLook _playerLook;
        [SerializeField] PlayerJump _playerJump;
        [SerializeField] private CheckInteractable _checkInteractable;
        
        bool _canMove = true;
        PlayerInputActions playerInputActions;
        AfterDestroy.Inventory.Inventory _inventory;

        [Inject]
        private void Construct(AfterDestroy.Inventory.Inventory inventory)
        {
            _inventory = inventory;
        }

        private void OnEnable()
        {
            playerInputActions?.Enable();
            playerInputActions.Player.Jump.started += Jump;
            playerInputActions.Player.Shift.performed += Shift;
            playerInputActions.Player.Shift.canceled += ShiftUp;
            playerInputActions.Player.LeftClick.started += LeftClickDown;
            playerInputActions.Player.LeftClick.canceled += LeftClickUp;
            playerInputActions.Player.RightClick.started += RightClickDown;
            playerInputActions.Player.Inventory.started += DisplayInventory;
        }

        private void OnDisable()
        {
            playerInputActions?.Disable();
            playerInputActions.Player.Jump.started -= Jump;
            playerInputActions.Player.Shift.performed -= Shift;
            playerInputActions.Player.Shift.canceled -= ShiftUp;
            playerInputActions.Player.LeftClick.started -= LeftClickDown;
            playerInputActions.Player.LeftClick.canceled -= LeftClickUp;
            playerInputActions.Player.RightClick.started -= RightClickDown;
            playerInputActions.Player.Inventory.started -= DisplayInventory;
        }

        public void Awake()
        {
            if (_lookCursor)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }

            playerInputActions = new PlayerInputActions();
        }

        private void Update()
        {
            _playerLook.MouseLook(playerInputActions.Player.Look.ReadValue<Vector2>());

            if (!_canMove)
                return;

            _playerMove.UpdateMovement(playerInputActions.Player.Move.ReadValue<Vector2>());
        }

        public void SetPlayerMove(bool isCanMove)
        {
            _canMove = isCanMove;
        }
        private void LeftClickDown(InputAction.CallbackContext ctx) => _checkInteractable.InteractWithObject();
        private void LeftClickUp(InputAction.CallbackContext ctx) => _checkInteractable.LeftClickUp();
        private void RightClickDown(InputAction.CallbackContext ctx) => _checkInteractable.RightClickDown();
        private void DisplayInventory(InputAction.CallbackContext ctx) => _inventory.SwitchInventoryStatus();
        private void Shift(InputAction.CallbackContext ctx) => _playerMove.Shift();
        private void ShiftUp(InputAction.CallbackContext ctx) => _playerMove.UnShift();
        private void Jump(InputAction.CallbackContext ctx) => _playerJump.JumpInput();
    }
}