using GameSystem.General;
using UnityEngine;
using UnityEngine.Events;

namespace CreatureAnimation.Player
{
    [CreateAssetMenu(fileName = "PlayerAnimationManager", menuName = "Player/Animatation/Manager")]
    public class PlayerAnimationManager : SystemOrigin<PlayerAnimationManager>
    {
        [SerializeField, EditorAttributes.EditorReadOnly] private RuntimeAnimatorController _currentAnimator;

        [SerializeField] private UnityEvent<RuntimeAnimatorController> OnAnimatorChanged;

        internal static RuntimeAnimatorController CurrentAnimator => I._currentAnimator;

        public static void ListenChangeController(UnityAction<RuntimeAnimatorController> action, bool subscribe)
        {
            if (subscribe) I.OnAnimatorChanged.AddListener(action);
            else I.OnAnimatorChanged.RemoveListener(action);
        }
        public void SetAnimatoController(RuntimeAnimatorController controller)
        {
            _currentAnimator = controller;

            OnAnimatorChanged?.Invoke(_currentAnimator);
#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }

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
