using AfterDestroy.Interactable;
using AfterDestroy.UI;
using UnityEngine;

namespace AfterDestroy.Player
{
    public class CheckInteractable : MonoBehaviour
    {
        [SerializeField] private Camera playerCamera;
        [SerializeField] private PointImage pointImage;
        [SerializeField] private Transform nearCameraPosition;
        [SerializeField] private PlayerController playerController;

        [Header("Inventory settings")] [SerializeField]
        private Inventory.Inventory inventory;

        private bool _isCanCheck;
        private string _interactableTag = "Interactable";

        //Player settings
        private Ray _ray;
        private bool _isPointImageOn;

        private void OnValidate()
        {
            if (pointImage == null)
            {
                pointImage = FindObjectOfType<PointImage>();
            }
        }

        private void Update()
        {
            CheckInteract();
        }

        private void CheckInteract()
        {
            Transform selection = null;
            Transform previousParent = null;
            
            if (_isCanCheck)
            {
                CheckUserInput(selection, previousParent);
            }

            RaycastHit raycastHit;
            if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out raycastHit, 100f))
            {
                selection = raycastHit.transform;
                if (selection.CompareTag(_interactableTag))
                {
                    pointImage.SetOn();
                    Interact(selection, out previousParent);
                }
                else
                {
                    pointImage.SetOff();
                }
            }
        }

        private void CheckUserInput(Transform selection, Transform previousParent)
        {
            if (Input.GetMouseButtonDown(0))
            {
                // selectionGameObject.gameObject.SetActive(false);
                // playerController.SetPlayerMove(true);
                // _isCanCheck = false;
            }
            else if (Input.GetMouseButtonDown(1))
            {
                // selection.SetParent(previousParent);
                playerController.SetPlayerMove(true);
                _isCanCheck = false;
            }
        }

        private void Interact(Transform selection, out Transform previousParent)
        {
            Transform parent = null;
            if (Input.GetMouseButtonDown(0))
            {
                if (selection.TryGetComponent(out IInteractable interactable))
                {
                    playerController.SetPlayerMove(false);
                    parent = interactable.GetTransform().parent;
                    interactable.Interact();
                    interactable.SetParent(nearCameraPosition);
                    interactable.SetPosition(nearCameraPosition.transform);
                    _isCanCheck = true;
                }
            }

            previousParent = parent;
        }
    }
}