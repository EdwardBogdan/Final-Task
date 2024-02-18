using UnityEngine;

namespace ScriptAnimation
{
    public class AnimatorSwitchComponent : MonoBehaviour
    {
        [SerializeField] Animator _animator;
        [SerializeField] bool _state;

        [SerializeField] string _animationKey;

        [ContextMenu("Switch")]
        public void Switch()
        {
            _state = !_state;
            _animator.SetBool(_animationKey, _state);
        }

        private void Awake()
        {
            _animator.SetBool(_animationKey, _state);
        }
    }
}

