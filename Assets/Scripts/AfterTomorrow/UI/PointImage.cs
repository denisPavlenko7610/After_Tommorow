using System;
using UnityEngine;
using UnityEngine.UI;

namespace AfterDestroy.UI
{
    public class PointImage : MonoBehaviour
    {
        [SerializeField] Image _point;
        [SerializeField] Animator _animator;
        private static readonly int IsVisible = Animator.StringToHash("isVisible");

        public void SetOn()
        {
            _animator.SetBool(IsVisible, true);
        }

        public void SetOff()
        {
            _animator.SetBool(IsVisible, false);
        }
    }
}
