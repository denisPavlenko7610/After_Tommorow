﻿using UnityEngine;

namespace AfterDestroy.Interactable
{
    public interface IInteractable
    {
        void Interact();

        void Destroy();
        void SetParent(Transform transform);
        void SetPosition(Transform transform);

        void DisableCanvas();

        void ThrowObject();

        string GetObjectName();
    }
}