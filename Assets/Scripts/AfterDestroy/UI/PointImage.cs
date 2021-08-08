using UnityEngine;
using UnityEngine.UI;

namespace AfterDestroy.UI
{
    public class PointImage : MonoBehaviour
    {
        private Image _point;
        private Animator _animator;
        private static readonly int IsVisible = Animator.StringToHash("isVisible");

        private void OnValidate()
        {
            if (_point == null)
            {
                _point = GetComponent<Image>();
            }

            if (_animator == null)
            {
                _animator = GetComponent<Animator>();
            }
        }

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
