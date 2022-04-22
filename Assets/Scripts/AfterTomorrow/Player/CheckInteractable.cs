using AfterDestroy.Interactable;
using AfterDestroy.UI;
using DG.Tweening;
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
        [SerializeField] AfterTomorrow.Player.PlayerInput _playerInput;
        [SerializeField] TextMeshProUGUI _objectName;
        [SerializeField] LayerMask _layerMask;
        [SerializeField] private float _transitionTimeInSec = 0.7f;

        [Header("Inventory settings")] Transform _selectionObject;
        Inventory.Inventory _inventory;
        Ray _ray;
        IInteractable _inetractableObject;
        Item _currentItem;
        int _countOfLeftMouseClick;
        bool _isPointImageOn;
        bool _objectInteract;
        int _distanceToInteractObject = 5;
        private bool _isClicked;
        private int _clicksCountToGetObject = 2;

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
        }

        public void LeftClickUp() => _isClicked = false;

        public void InteractWithObject()
        {
            _isClicked = true;
            if (_objectInteract == false)
                return;

            _countOfLeftMouseClick++;
            if (_countOfLeftMouseClick != _clicksCountToGetObject) return;

            _pointImage.SetOff();
            _objectName.DOFade(0, _transitionTimeInSec);
            _inetractableObject.Destroy();
            _playerInput.SetPlayerMove(true);
            _inetractableObject.DisableCanvas();
            _objectInteract = false;
            _countOfLeftMouseClick = 0;
            _inventory.DisplayItem(_currentItem).Forget();
        }
        
        public void RightClickDown()
        {
            if (_objectInteract == false)
                return;

            _inetractableObject.DisableCanvas();
            _inetractableObject.SetParent(null);
            _playerInput.SetPlayerMove(true);
            _inetractableObject.ThrowObject();
            _objectInteract = false;
            _countOfLeftMouseClick = 0;
        }

        private void CheckInteract()
        {
            if (_objectInteract)
                return;

            var ray = _playerCamera.ScreenPointToRay(new Vector2(Screen.width / 2f, Screen.height / 2f));
            Debug.DrawRay(ray.origin, ray.direction * _distanceToInteractObject, Color.blue);

            if (Physics.Raycast(ray, out var raycastHit, _distanceToInteractObject, _layerMask))
            {
                if (raycastHit.collider == null)
                    return;

                _selectionObject = raycastHit.transform;
                if (!_selectionObject.TryGetComponent(out Item item)) return;

                _pointImage.SetOn();
                _currentItem = item;
                Interact(_selectionObject);
            }
            else
            {
                _objectName.DOFade(0, _transitionTimeInSec);
                _pointImage.SetOff();
            }
        }

        private void Interact(Transform selection)
        {
            if (!selection.TryGetComponent(out IInteractable interactable))
                return;

            _objectName.text = interactable.GetObjectName();
            _objectName.DOFade(1, _transitionTimeInSec);

            if (!_isClicked) return;
            
            _objectName.DOFade(0, _transitionTimeInSec);
            _pointImage.SetOff();
            _countOfLeftMouseClick++;
            _objectInteract = true;
            _inetractableObject = interactable;
            _playerInput.SetPlayerMove(false);
            interactable.Interact();
            interactable.SetParent(_nearCameraPosition);
            interactable.SetPosition(_nearCameraPosition.transform);
        }
    }
}