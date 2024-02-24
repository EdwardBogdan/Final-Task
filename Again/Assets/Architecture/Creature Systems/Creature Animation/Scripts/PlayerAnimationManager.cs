using GameSystem.General;
using UnityEngine;
using UnityEngine.Events;

namespace CreatureAnimation.Player
{
    [CreateAssetMenu(fileName = "PlayerAnimationManager", menuName = "Player/Animation/Manager")]
    public class PlayerAnimationManager : SystemOrigin<PlayerAnimationManager>
    {
        [SerializeField, EditorAttributes.EditorReadOnly] private Animator _playerAnimator;
        [SerializeField, EditorAttributes.EditorReadOnly] private RuntimeAnimatorController _animatorController;

        internal static RuntimeAnimatorController AnimatorController => I._animatorController;

        public void SetTrigger(string name)
        {
            _playerAnimator?.SetTrigger(name);
        }
        public void SetAnimatoController(RuntimeAnimatorController controller)
        {
            _animatorController = controller;

            OnAnimatorChanged?.Invoke(_animatorController);
#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }
        internal static void SetAnimator(Animator animator)
        {
            I._playerAnimator = animator;
        }

        #region Event
        [SerializeField] private UnityEvent<RuntimeAnimatorController> OnAnimatorChanged;

        public static void ListenChangeController(UnityAction<RuntimeAnimatorController> action, bool subscribe)
        {
            if (subscribe) I.OnAnimatorChanged.AddListener(action);
            else I.OnAnimatorChanged.RemoveListener(action);
        }
        #endregion

        #region Init
        private const string AddressableKey = "AnimatorManager";

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        private static void Init()
        {
            InitializeSystem(AddressableKey);
        }
        #endregion
    }
}
