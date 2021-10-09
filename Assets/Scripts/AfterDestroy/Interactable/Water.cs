using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AfterDestroy.Interactable
{
    public class Water : MonoBehaviour, IInteractable
    {
        [SerializeField] private GameObject waterPanel;
        private bool _canInteract;
        private Rigidbody rigidbody;

        private void Start()
        {
            rigidbody = GetComponent<Rigidbody>();
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

        public void DisableCanvas()
        {
            waterPanel.SetActive(false);
        }

        public void ThrowObject()
        {
            StartCoroutine(ThrowObjectCoroutine());
        }

        IEnumerator ThrowObjectCoroutine()
        {
            rigidbody.isKinematic = false;
            yield return new WaitForSeconds(2f);
            rigidbody.isKinematic = true;
        }
    }
}
