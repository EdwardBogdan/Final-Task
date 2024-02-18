using UnityEngine;
using UnityEngine.Events;

namespace UI.Windows
{
    [RequireComponent(typeof(Animator))]
    public class WindowAnimated<T> : MonoBehaviour where T : WindowAnimated<T>
    {
        private Animator _animator;

        protected static readonly int Show = Animator.StringToHash("Show");
        protected static readonly int Hide = Animator.StringToHash("Hide");

        public void TriggerHide()
        {
            _animator.SetTrigger(Hide);
        }
        public void TriggerShow()
        {
            _animator.SetTrigger(Show);
        }

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        #region Animation Events
        public UnityEvent EventShowStart;
        public UnityEvent EventShowFinish;
        public UnityEvent EventHideStart;
        public UnityEvent EventHideFinish;

        public void OnShowStart()
        {
            EventShowStart?.Invoke();
        }
        public void OnShowFinish()
        {
            EventShowFinish?.Invoke();
        }
        public void OnHideStart()
        {
            EventHideStart?.Invoke();
        }
        public void OnHideFinish()
        {
            EventHideFinish?.Invoke();

        }
        #endregion

        public void DestroySelf()
        {
            Destroy(gameObject);
        }
    }
}
