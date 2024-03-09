using DialogSystem;
using DialogSystem.Side;
using GameSystem.Overlay;
using UnityEngine;

namespace UI.Dialog
{
    public class DialogUIController : UnitUI
    {
        [SerializeField] private Animator _animator;

        private void Awake()
        {
            SetSide(DialogManager.Side);
            DialogManager.ListenSide(SetSide, true);
            DialogManager.DialogFinished += OnDialogFinished;
            _animator.SetTrigger(Key_Open);
        }
        protected override void OnDestroy()
        {
            base.OnDestroy();
            DialogManager.ListenSide(SetSide, false);
            DialogManager.DialogFinished -= OnDialogFinished;

        }
        private void OnDialogFinished()
        {
            _animator.SetTrigger(Key_Close);
        }
        private void SetSide(SideType side)
        {
            bool value = false;

            switch (side)
            {
                case SideType.Left:
                    value = true;
                    break;
                case SideType.Right:
                    value = false;
                    break;
            }

            _animator.SetBool(Key_Side, value);
        }
        
        public static readonly int Key_Side = Animator.StringToHash("Side");
        public static readonly int Key_Open = Animator.StringToHash("Open");
        public static readonly int Key_Close = Animator.StringToHash("Close");

        #region Init
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void Initialize()
        {
            string AddressableKey = "Dialog UI";

            DialogManager.DialogStarted += () => UILoader.LoadUI<DialogUIController>(AddressableKey, OnLoaded);

            static void OnLoaded(DialogUIController value)
            {
                DialogManager.NextPhrase();
            }
        }
        
        #endregion
    }
}