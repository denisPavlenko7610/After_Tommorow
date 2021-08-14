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
            RaycastHit raycastHit;
            if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out raycastHit, 100f))
            {
                var selection = raycastHit.transform;
                if (selection.CompareTag(_interactableTag))
                {
                    pointImage.SetOn();
                    Interact(selection);
                }
                else
                {
                    pointImage.SetOff();
                }
            }
        }

        private void Interact(Transform selection)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (selection.TryGetComponent(out Water water))
                {
                    GameObject waterGameObject;
                    (waterGameObject = water.gameObject).transform.SetParent(nearCameraPosition);
                    waterGameObject.GetComponent<Water>().Interact();
                    waterGameObject.transform.position = nearCameraPosition.position;
                }
            }
        }
    }
}