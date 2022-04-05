using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AfterDestroy.Interactable
{
    public class Water : MonoBehaviour, IInteractable
    {
        [SerializeField] private GameObject waterPanel;

        private bool _canInteract;
        private Rigidbody _rigidbody;
        private string objectName = "Вода";

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

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

        public void Destroy()
        {
            gameObject.SetActive(false);
            Destroy(gameObject, Random.Range(0,5f));
        }

        public void SetParent(Transform transform)
        {
            gameObject.transform.parent = transform;
        }

        public void SetPosition(Transform transform)
        {
            gameObject.transform.position = transform.position;
        }

        public void AddToInventory()
        {
            // Inve
        }

        public void DisableCanvas()
        {
            waterPanel.SetActive(false);
        }

        public void ThrowObject()
        {
            ThrowObjectCoroutine();
        }

        public string GetObjectName()
        {
            return objectName;
        }

        async void ThrowObjectCoroutine()
        {
            _rigidbody.isKinematic = false;
            await UniTask.Delay(TimeSpan.FromSeconds(2f));
            _rigidbody.isKinematic = true;
        }
    }
}
