using AfterDestroy.Interactable;
using AfterDestroy.Inventory;
using AfterDestroy.UI;
using TMPro;
using UnityEngine;
using Zenject;

namespace AfterDestroy.Player
{
    public class CheckInteractable : MonoBehaviour
    {
        [SerializeField] Camera playerCamera;
        [SerializeField] PointImage pointImage;
        [SerializeField] Transform nearCameraPosition;
        [SerializeField] PlayerController playerController;
        [SerializeField] TextMeshProUGUI objectName;

        [Header("Inventory settings")] string _interactableTag = "Interactable";
        Transform selection;
        Inventory.Inventory inventory;
        Ray _ray;
        IInteractable _inetractableObject;
        private Item currentItem;
        int _countOfLeftMouseClick;
        bool _isPointImageOn;
        bool _objectInteract;
        int _distanceToInteractObject = 5;

        [Inject]
        private void Construct(TextMeshProUGUI objectName, PointImage pointImage, Inventory.Inventory inventory)
        {
            this.objectName = objectName;
            this.pointImage = pointImage;
            this.inventory = inventory;
        }

        private void Update()
        {
            CheckInteract();
            CheckUserInput();
        }

        private void CheckInteract()
        {
            if (_objectInteract)
            {
                return;
            }

            var ray = playerCamera.ScreenPointToRay(new Vector2(Screen.width / 2f, Screen.height / 2f));
            if (Physics.Raycast(ray, out var raycastHit, _distanceToInteractObject))
            {
                if (raycastHit.collider == null)
                    return;

                selection = raycastHit.transform;
                if (selection.TryGetComponent(out Item item))
                {
                    pointImage.SetOn();
                    currentItem = item;
                    Interact(selection);
                }
                else
                {
                    objectName.text = "";
                    pointImage.SetOff();
                }
            }
        }

        private void CheckUserInput()
        {
            if (_objectInteract == false)
                return;

            if (Input.GetMouseButtonDown(0))
            {
                _countOfLeftMouseClick++;
                if (_countOfLeftMouseClick == 3)
                {
                    AddToInventory();
                    _inetractableObject.Destroy();
                    playerController.SetPlayerMove(true);
                    _inetractableObject.DisableCanvas();
                    _objectInteract = false;
                    _countOfLeftMouseClick = 0;
                }
            }
            else if (Input.GetMouseButtonDown(1))
            {
                _inetractableObject.DisableCanvas();
                _inetractableObject.SetParent(null);
                playerController.SetPlayerMove(true);
                _inetractableObject.ThrowObject();
                _objectInteract = false;
                _countOfLeftMouseClick = 0;
            }
        }

        private void AddToInventory()
        {
            for (int i = 0; i < inventory.Items.Count; i++)
            {
                if (inventory.Items[i].Id == 0)
                {
                    inventory.Items[i] = currentItem;
                }
            }
        }

        private void Interact(Transform selection)
        {
            if (selection.TryGetComponent(out IInteractable interactable))
            {
                objectName.text = interactable.GetObjectName();

                if (Input.GetMouseButtonDown(0))
                {
                    _countOfLeftMouseClick++;
                    _objectInteract = true;
                    _inetractableObject = interactable;
                    playerController.SetPlayerMove(false);
                    interactable.Interact();
                    interactable.SetParent(nearCameraPosition);
                    interactable.SetPosition(nearCameraPosition.transform);
                }
            }
        }
    }
}