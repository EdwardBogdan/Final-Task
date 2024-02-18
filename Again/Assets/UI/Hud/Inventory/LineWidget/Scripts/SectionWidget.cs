using Property.ObservableProperty;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Hud.Inventory
{
    public class SectionWidget : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private Color _colorActive;
        [SerializeField] private Color _colorEnactive;

        [SerializeField] private BoolObsProperty _active;

        public bool Status {set => _active.Value = value; }

        public void SetStatus(bool value, bool oldValue)
        {
            _image.color = value ? _colorActive : _colorEnactive;
        }

        private void Awake()
        {
            SetStatus(_active.Value, true);
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            _active.Validate();
        }
#endif
    }
}
