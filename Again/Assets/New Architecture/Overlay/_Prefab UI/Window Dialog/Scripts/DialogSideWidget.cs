using DialogSystem;
using UnityEngine;

namespace Overlay.UIManagment.Windows.Dialog
{
    public class DialogSideWidget : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        private void Awake()
        {
            SetSide(DialogManager.Side);
            DialogManager.ListenSide(SetSide, true);
        }
        protected void OnDestroy()
        {
            DialogManager.ListenSide(SetSide, false);
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
    }
}

