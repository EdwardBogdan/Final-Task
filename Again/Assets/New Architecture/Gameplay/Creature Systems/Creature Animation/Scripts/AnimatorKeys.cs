using UnityEngine;

namespace CreatureAnimation
{
    internal static class AnimatorKeys
    {
        public static readonly int Key_IsGrounded = Animator.StringToHash("IsGrounded");
        public static readonly int Key_IsRunning = Animator.StringToHash("IsRunning");
        public static readonly int Key_IsWalled = Animator.StringToHash("IsWalled");
        public static readonly int Key_VerticalVelocity = Animator.StringToHash("Vertical Velocity");
    }
}
