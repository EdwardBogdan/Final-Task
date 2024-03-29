using UnityEngine;
using UnityEngine.Events;

namespace Overlay.UIManagment
{
    [RequireComponent(typeof(Animator))]
    public abstract class UnitUIAnim : UnitUI
    {
        #region Controll
        public void ShowUI(UnitUIAnim unit)
        {
            UIController.OpenUI(unit);
        }
        public void HideUI(UnitUIAnim unit)
        {
            UIController.HideUI(unit);
        }
        #endregion

        #region Anim
        [SerializeField, EditorAttributes.EditorReadOnly] private Animator _animator;

        public static readonly int Key_Show = Animator.StringToHash("Show");
        public static readonly int Key_Hide = Animator.StringToHash("Hide");

        protected Animator Animator => _animator;

        public void TriggerShow()
        {
            _animator.SetTrigger(Key_Show);
        }
        public void TriggerHide()
        {
            _animator.SetTrigger(Key_Hide);
        }
        #endregion

        #region Event
        [SerializeField] private UnityEvent _onStartAnim;
        [SerializeField] private UnityEvent _onFinishAnim;

        public void OnStartAnim()
        {
            _onStartAnim?.Invoke();
        }
        public void OnFinishAnim()
        {
            _onFinishAnim?.Invoke();
        }
        #endregion

        #region Mono
        protected virtual void OnValidate()
        {
            if (_animator == null) _animator = GetComponent<Animator>();
        }

        #endregion
    }
}
