using AfterDestroy.Interactable;
using AfterDestroy.UI;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Zenject;

namespace AfterDestroy
{
    public class CheckInteractable : MonoBehaviour
    {
        [SerializeField] Camera _playerCamera;
        [SerializeField, HideInInspector] PointImage _pointImage;
        [SerializeField] Transform _nearCameraPosition;
        [SerializeField] AfterTomorrow.PlayerInput _playerInput;
        [SerializeField, HideInInspector] TextMeshProUGUI _objectName;
        [SerializeField] LayerMask _layerMask;
        [SerializeField] private float _transitionTimeInSec = 0.7f;

        [Header("Inventory settings")] Transform _selectionObject;
        Inventory.InventorySystem inventorySystem;
        Ray _ray;
        IInteractable _inetractableObject;
        InventoryObject currentInventoryObject;
        int _countOfLeftMouseClick;
        bool _isPointImageOn;
        bool _objectInteract;
        int _distanceToInteractObject = 7;
        bool _isClicked;
        int _clicksCountToGetObject = 2;
        InventoryObject currentItemObject;

        [Inject]
        void Construct(TextMeshProUGUI objectName, PointImage pointImage)
        {
            _objectName = objectName;
            _pointImage = pointImage;
        }

        void Update()
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
            if (_countOfLeftMouseClick != _clicksCountToGetObject)
                return;

            if (currentItemObject)
                currentItemObject.OnPickupItem();

            _pointImage.SetOff();
            _objectName.DOFade(0, _transitionTimeInSec);
            _playerInput.SetPlayerMove(true);
            _inetractableObject.DisableCanvas();
            _inetractableObject.Destroy();
            _objectInteract = false;
            _countOfLeftMouseClick = 0;
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

        void CheckInteract()
        {
            if (_objectInteract)
                return;

            var ScreenDivider = 2f;
            var ray = _playerCamera.ScreenPointToRay(new Vector2(Screen.width / ScreenDivider, Screen.height / ScreenDivider));
            //Debug.DrawRay(ray.origin, ray.direction * _distanceToInteractObject, Color.blue);

            if (Physics.Raycast(ray, out var raycastHit, _distanceToInteractObject, _layerMask))
            {
                if (raycastHit.collider == null)
                    return;

                _selectionObject = raycastHit.transform;
                if (!_selectionObject.TryGetComponent(out InventoryObject item))
                    return;

                currentItemObject = item;
                _pointImage.SetOn();
                Interact(_selectionObject);
            }
            else
            {
                _objectName.DOFade(0, _transitionTimeInSec);
                _pointImage.SetOff();
            }
        }

        void Interact(Transform selection)
        {
            if (!selection.TryGetComponent(out IInteractable interactable))
                return;

            _objectName.text = interactable.GetObjectName();
            _objectName.DOFade(1, _transitionTimeInSec);

            if (!_isClicked)
                return;

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