using UnityEngine;

namespace CreatureAnimation.Player
{
    public class PlayerAnimatorChanger : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        private void Awake()
        {
            PlayerAnimationManager.ListenChangeController(OnChangeAnimator, true);

            _animator.runtimeAnimatorController = PlayerAnimationManager.AnimatorController;
    }

        private void OnChangeAnimator(RuntimeAnimatorController controller)
        {
            bool isGround = _animator.GetBool(AnimatorKeys.Key_IsGrounded);
            bool isWalled = _animator.GetBool(AnimatorKeys.Key_IsWalled);

            _animator.runtimeAnimatorController = PlayerAnimationManager.AnimatorController;

            _animator.SetBool(AnimatorKeys.Key_IsGrounded, isGround);
            _animator.SetBool(AnimatorKeys.Key_IsWalled, isWalled);
        }

        private void OnDestroy()
        {
            PlayerAnimationManager.ListenChangeController(OnChangeAnimator, false);
        }
    }
}
