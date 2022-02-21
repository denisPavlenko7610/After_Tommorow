using AfterDestroy.Interactable;
using AfterDestroy.UI;
using TMPro;
using UnityEngine;
using Zenject;

namespace AfterDestroy.Player
{
    public class CheckInteractable : MonoBehaviour
    {
        [SerializeField] private Camera playerCamera;
        [SerializeField] private PointImage pointImage;
        [SerializeField] private Transform nearCameraPosition;
        [SerializeField] private PlayerController playerController;
        [SerializeField] private TextMeshProUGUI objectName;

        [Header("Inventory settings")]
        [SerializeField]
        private Inventory.Inventory inventory;

        private string _interactableTag = "Interactable";
        private bool _objectInteract;
        Transform selection;
        private Ray _ray;
        private bool _isPointImageOn;
        private IInteractable _inetractableObject;
        private int _countOfLeftMouseClick;

        [Inject]
        private void Construct(TextMeshProUGUI objectName, PointImage pointImage)
        {
            this.objectName = objectName;
            this.pointImage = pointImage;
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

            RaycastHit raycastHit;
            if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out raycastHit, 100f))
            {
                selection = raycastHit.transform;
                if (selection.CompareTag(_interactableTag))
                {
                    pointImage.SetOn();
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
            {
                return;
            }

            if (Input.GetMouseButtonDown(0))
            {
                _countOfLeftMouseClick++;
                if (_countOfLeftMouseClick == 3)
                {
                    _inetractableObject.Destroy();
                    playerController.SetPlayerMove(true);
                    _inetractableObject.DisableCanvas();
                    _objectInteract = false;
                    _countOfLeftMouseClick = 0;
                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                _inetractableObject.DisableCanvas();
                _inetractableObject.SetParent(null);
                playerController.SetPlayerMove(true);
                _inetractableObject.ThrowObject();
                _objectInteract = false;
                _countOfLeftMouseClick = 0;
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