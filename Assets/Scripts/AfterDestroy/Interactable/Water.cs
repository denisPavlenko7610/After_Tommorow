using System;
using UnityEngine;

namespace AfterDestroy.Interactable
{
    public class Water : MonoBehaviour, IInteractable
    {
        [SerializeField] private GameObject waterPanel;
        private bool _canInteract;
        
        void Update()
        {
            if (_canInteract)
            {
                transform.rotation = Quaternion.Euler(0,0,0);
            }
        }

        public void Interact()
        {
            _canInteract = true;
            waterPanel.gameObject.SetActive(true);
        }

        public Transform GetTransform()
        {
            return gameObject.transform;
        }

        public void SetParent(Transform transform)
        {
            gameObject.transform.parent = transform;
        }

        public void SetPosition(Transform transform)
        {
            gameObject.transform.position = transform.position;
        }
    }
}
