using UnityEngine;

namespace CreatureAnimation
{
    public class CreatureAnimatorParameterSeter : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private Rigidbody2D _rigidbody;        

        public void OnSetGround(bool value)
        {
            animator.SetBool(AnimatorKeys.Key_IsGrounded, value);
        }
        public void OnSetWall(bool value)
        {
            animator.SetBool(AnimatorKeys.Key_IsWalled, value);
        }

        private void FixedUpdate()
        {
            animator.SetBool(AnimatorKeys.Key_IsRunning, _rigidbody.velocity.x != 0);
            animator.SetFloat(AnimatorKeys.Key_VerticalVelocity, _rigidbody.velocity.y);
        }
    }
}
