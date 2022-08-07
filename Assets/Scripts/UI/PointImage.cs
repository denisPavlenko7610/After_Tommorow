using UnityEngine;

namespace AfterDestroy.UI
{
    public class PointImage : MonoBehaviour
    {
        [SerializeField] Animator _animator;
        private static readonly int IsVisible = Animator.StringToHash("isVisible");

        public void SetOn() => _animator.SetBool(IsVisible, true);
        public void SetOff() => _animator.SetBool(IsVisible, false);
    }
}
