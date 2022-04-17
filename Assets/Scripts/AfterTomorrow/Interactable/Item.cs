using UnityEngine;
using Random = UnityEngine.Random;

namespace AfterDestroy.Interactable
{
    public class Item : MonoBehaviour, IInteractable
    {
        public InventoryItem item;
        [HideInInspector] public int Id;
        [SerializeField] private ItemInfoPanel itemInfoPanel;
        private bool _canInteract;
        private Rigidbody _rigidbody;
        private const string GroundTag = "Ground";

        private void OnValidate()
        {
            if (itemInfoPanel == null)
            {
                itemInfoPanel = FindObjectOfType<ItemInfoPanel>();
            }
        }

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        void Update()
        {
            if (_canInteract)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.collider.CompareTag(GroundTag))
            {
                _rigidbody.isKinematic = true;
            }
        }

        public void Interact()
        {
            _canInteract = true;
            itemInfoPanel.ShowPanel(item.ItemName, item.descriptionItem, item.Weight);
        }

        public void Destroy()
        {
            gameObject.SetActive(false);
            Destroy(gameObject, Random.Range(0, 5f));
        }

        public void SetParent(Transform transform) => gameObject.transform.parent = transform;

        public void SetPosition(Transform transform) => gameObject.transform.position = transform.position;

        public void DisableCanvas() => itemInfoPanel.HidePanel();

        public void ThrowObject() => _rigidbody.isKinematic = false;

        public string GetObjectName() => item.ItemName;
    }
}