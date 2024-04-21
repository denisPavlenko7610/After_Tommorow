using AfterDestroy;
using Inventory;
using RDDependency;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AfterTomorrow
{
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] bool _lookCursor = true;
        [SerializeField] PlayerMove _playerMove;
        [SerializeField] PlayerLook _playerLook;
        [SerializeField] PlayerJump _playerJump;
        [field:SerializeField] public CheckInteractable CheckInteractable { get; set; }
        
        bool _canMove = true;
        PlayerInputActions _playerInputActions;
        InventoryUI _inventoryUI;

        [Inject]
        void Construct(InventoryUI inventoryUI)
        {
            _inventoryUI = inventoryUI;
        }

        private void OnEnable()
        {
            _playerInputActions = new PlayerInputActions();
            _playerInputActions.Enable();
            _playerInputActions.Player.Jump.started += Jump; 
            _playerInputActions.Player.Shift.performed += Shift; 
            _playerInputActions.Player.Shift.canceled += ShiftUp;
            _playerInputActions.Player.LeftClick.started += LeftClickDown;
            _playerInputActions.Player.LeftClick.canceled += LeftClickUp;
            _playerInputActions.Player.RightClick.started += RightClickDown;
            _playerInputActions.Player.Inventory.started += DisplayInventory;
        }

        private void OnDisable()
        {
            _playerInputActions?.Disable();
            _playerInputActions.Player.Jump.started -= Jump;
            _playerInputActions.Player.Shift.performed -= Shift;
            _playerInputActions.Player.Shift.canceled -= ShiftUp;
            _playerInputActions.Player.LeftClick.started -= LeftClickDown;
            _playerInputActions.Player.LeftClick.canceled -= LeftClickUp;
            _playerInputActions.Player.RightClick.started -= RightClickDown;
            _playerInputActions.Player.Inventory.started -= DisplayInventory;
        }

        public void Awake()
        {
            if (_lookCursor)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }

        private void Update()
        {
            _playerLook.MouseLook(_playerInputActions.Player.Look.ReadValue<Vector2>());

            if (!_canMove)
                return;

            _playerMove.UpdateMovement(_playerInputActions.Player.Move.ReadValue<Vector2>());
        }

        public void SetPlayerMove(bool isCanMove)
        {
            _canMove = isCanMove;
        }
        private void LeftClickDown(InputAction.CallbackContext ctx) => CheckInteractable.InteractWithObject();
        private void LeftClickUp(InputAction.CallbackContext ctx) => CheckInteractable.LeftClickUp();
        private void RightClickDown(InputAction.CallbackContext ctx) => CheckInteractable.RightClickDown();
        private void DisplayInventory(InputAction.CallbackContext ctx) => _inventoryUI.ChangeInventoryVisibility();
        private void Shift(InputAction.CallbackContext ctx) => _playerMove.Shift();
        private void ShiftUp(InputAction.CallbackContext ctx) => _playerMove.UnShift();
        private void Jump(InputAction.CallbackContext ctx) => _playerJump.JumpInput();
    }
}