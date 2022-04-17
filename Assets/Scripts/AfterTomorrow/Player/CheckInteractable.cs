using AfterDestroy.Interactable;
using AfterDestroy.UI;
using TMPro;
using UnityEngine;
using Zenject;

namespace AfterDestroy.Player
{
    public class CheckInteractable : MonoBehaviour
    {
        [SerializeField] Camera _playerCamera;
        [SerializeField] PointImage _pointImage;
        [SerializeField] Transform _nearCameraPosition;
        [SerializeField] PlayerController _playerController;
        [SerializeField] TextMeshProUGUI _objectName;

        [Header("Inventory settings")]
        Transform _selectionObject;
        Inventory.Inventory _inventory;
        Ray _ray;
        IInteractable _inetractableObject;
        Item _currentItem;
        int _countOfLeftMouseClick;
        bool _isPointImageOn;
        bool _objectInteract;
        int _distanceToInteractObject = 5;

        [Inject]
        private void Construct(TextMeshProUGUI objectName, PointImage pointImage, Inventory.Inventory inventory)
        {
            _objectName = objectName;
            _pointImage = pointImage;
            _inventory = inventory;
        }

        private void Update()
        {
            CheckInteract();
            CheckUserInput();
        }

        private void CheckInteract()
        {
            if (_objectInteract)
                return;

            var ray = _playerCamera.ScreenPointToRay(new Vector2(Screen.width / 2f, Screen.height / 2f));
            if (Physics.Raycast(ray, out var raycastHit, _distanceToInteractObject))
            {
                if (raycastHit.collider == null)
                    return;

                _selectionObject = raycastHit.transform;
                if (_selectionObject.TryGetComponent(out Item item))
                {
                    _pointImage.SetOn();
                    _currentItem = item;
                    Interact(_selectionObject);
                }
                else
                {
                    _objectName.text = "";
                    _pointImage.SetOff();
                }
            }
        }

        private void CheckUserInput()
        {
            if (_objectInteract == false)
                return;

            if (Input.GetMouseButtonDown(0))
            {
                _pointImage.SetOff();
                _objectName.text = "";
                _countOfLeftMouseClick++;
                
                if (_countOfLeftMouseClick == 3)
                {
                    _pointImage.SetOff();
                    _objectName.text = "";
                    _inetractableObject.Destroy();
                    _playerController.SetPlayerMove(true);
                    _inetractableObject.DisableCanvas();
                    _objectInteract = false;
                    _countOfLeftMouseClick = 0;
                    _inventory.DisplayItem(_currentItem).Forget();
                }
            }
            else if (Input.GetMouseButtonDown(1))
            {
                _inetractableObject.DisableCanvas();
                _inetractableObject.SetParent(null);
                _playerController.SetPlayerMove(true);
                _inetractableObject.ThrowObject();
                _objectInteract = false;
                _countOfLeftMouseClick = 0;
            }
        }

        private void Interact(Transform selection)
        {
            if (selection.TryGetComponent(out IInteractable interactable))
            {
                _objectName.text = interactable.GetObjectName();

                if (Input.GetMouseButtonDown(0))
                {
                    _countOfLeftMouseClick++;
                    _objectInteract = true;
                    _inetractableObject = interactable;
                    _playerController.SetPlayerMove(false);
                    interactable.Interact();
                    interactable.SetParent(_nearCameraPosition);
                    interactable.SetPosition(_nearCameraPosition.transform);
                }
            }
        }
    }
}