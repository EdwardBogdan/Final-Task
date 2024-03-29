using DialogSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Overlay.UIManagment.Windows.Dialog
{
    public class DialogFaceWidget : MonoBehaviour
    {
        [SerializeField] private SideType _side;
        [SerializeField] private Image _image;

        private void OnChange(Sprite value)
        {
            if (DialogManager.Side != _side) return;

            _image.sprite = value;
        }

        private void Awake()
        {
            DialogManager.ListenFace(OnChange);

            OnChange(DialogManager.Face);
        }

        private void OnDestroy()
        {
            DialogManager.ListenFace(OnChange, false);
        }
    }
}
