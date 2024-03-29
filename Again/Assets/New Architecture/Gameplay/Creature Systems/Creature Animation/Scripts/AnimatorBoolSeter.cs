using UnityEngine;

namespace CreatureAnimation
{
    public class AnimatorBoolSeter : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        public void BoolOff(string key)
        {
            _animator.SetBool(key, false);
        }
        public void BoolOn(string key)
        {
            _animator.SetBool(key, true);
        }
    }
}
