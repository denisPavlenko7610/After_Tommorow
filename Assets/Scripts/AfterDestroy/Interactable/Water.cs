using System;
using UnityEngine;

namespace AfterDestroy.Interactable
{
    public class Water : MonoBehaviour
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
    }
}
