using DialogSystem.Data;
using DialogSystem.Property;
using GameSystem.Initialization;
using Localization;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace DialogSystem
{
    [CreateAssetMenu(fileName = "DialogManager", menuName = "Dialog/Manager")]
    public class DialogManager : SystemOrigin<DialogManager>
    {
        #region Face
        [SerializeField] private FaceProperty _face;

        public static Sprite Face => I._face.Value;
        public static void ListenFace(UnityAction<Sprite> func, bool mode = true)
        {
            if (mode) I._face._onChanged.AddListener(func);
            else I._face._onChanged.RemoveListener(func);
        }
        #endregion

        #region Side
        [SerializeField] private SideProperty _side;

        public static SideType Side => I._side.Value;
        public static void ListenSide(UnityAction<SideType> func, bool mode = true)
        {
            if (mode) I._side._onChanged.AddListener(func);
            else I._side._onChanged.RemoveListener(func);
        }
        #endregion

        #region Text
        [SerializeField] private TextProperty _text;
        public static void ListenText(UnityAction<string> func, bool mode = true)
        {
            if (mode) I._text._onChanged.AddListener(func);
            else I._text._onChanged.RemoveListener(func);
        }
        #endregion

        #region Events
        [SerializeField] private UnityEvent _dialogStarted;
        [SerializeField] private UnityEvent _dialogFinished;

        public static void ListenDialogStart(UnityAction func, bool mode = true)
        {
            if (mode) I._dialogStarted.AddListener(func);
            else I._dialogStarted.RemoveListener(func);
        }

        public static void ListenDialogFinish(UnityAction func, bool mode = true)
        {
            if (mode) I._dialogFinished.AddListener(func);
            else I._dialogFinished.RemoveListener(func);
        }
        #endregion

        #region Controllers

        private static int index = 0;
        private static DialogData dialog;

        

        internal static void StartDialog(DialogData data)
        {
            index = 0;
            dialog = data;

            I._dialogStarted?.Invoke();
        }

        internal static void NextPhrase()
        {
            if (dialog == null) return;

            if (index >= dialog.Phrases.Length)
            {
                I._dialogFinished?.Invoke();
                return;
            }

            PhraseSet set = dialog.Phrases[index];

            I.SetPhrase(set);

            index++;
        }

        private void SetPhrase(PhraseSet data)
        {
            _side.Value = data.Phrase.Side;

            _face.Value = data.Phrase.Face;

            _text.Value = LocalizationManager.GetText(LocaGroup.Dialog, data.Phrase.PhraseKey);

            data.Event?.Invoke();
        }
        #endregion

        private void OnValidate()
        {
            _text.Validate();
            _face.Validate();
            _side.Validate();
        }

        #region Init
        private const string Group = "Gameplay";
        private const string AddressableKey = "DialogManager";

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        private static void Init()
        {
            InitializeSystem(Group, AddressableKey);
        }
        #endregion
    }
}
