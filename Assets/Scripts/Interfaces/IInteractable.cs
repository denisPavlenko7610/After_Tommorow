using UnityEngine;

namespace AfterDestroy.Interactable
{
    public interface IInteractable : IDestroy
    {
        void Interact();
        
        void SetParent(Transform transform);
        void SetPosition(Transform transform);

        void DisableCanvas();

        void ThrowObject();

        string GetObjectName();
    }
}