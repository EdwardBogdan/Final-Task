using UnityEngine;

namespace Overlay.UIManagment.HudManagment
{
    public abstract class UnitUIHud : UnitUIAnim
    {
        [SerializeField] private HudMode _localeHudMode = new();

        public void ChangeMode(bool value)
        {
            _localeHudMode.Value = value;
        }

        protected virtual void Awake()
        {
            HudController.ListenHudMode(OnChangeGlobalHudMode, true);
            _localeHudMode.OnChanged.AddListener(OnChange);
            _localeHudMode.Value = HudController.HudMode;
        }
        protected virtual void OnDestroy()
        {
            HudController.ListenHudMode(OnChangeGlobalHudMode, false);
            _localeHudMode.OnChanged.RemoveListener(OnChange);
        }

        private void OnChange(bool value)
        {
            if (value)
            {
                TriggerShow();
            }
            else
            {
                TriggerHide();
            }
        }

        private void OnChangeGlobalHudMode(bool value)
        {
            _localeHudMode.Value = value;
        }

        protected override void OnValidate()
        {
            base.OnValidate();
            _localeHudMode.Validate();
        }
    }
}
