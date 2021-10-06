using UnityEngine;

namespace AfterDestroy.Interactable
{
    public interface IInteractable
    {
        void Interact();
        Transform GetTransform();
        void SetParent(Transform transform);
        void SetPosition(Transform transform);
    }
}