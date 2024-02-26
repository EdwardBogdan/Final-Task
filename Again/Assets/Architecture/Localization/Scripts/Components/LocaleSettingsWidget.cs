using UnityEngine;

namespace Localization.Components
{
    public class LocaleSettingsWidget //ButtonBorder<LocaleSettingsWidget>
    {
        [SerializeField] private Language language;

        public void OnClick()
        {
            //LocalizationManager.LanguageProperty.Value = language;

            //ChangeBorder();
        }

        #region Mono
        private void Awake()
        {
            //if (language == LocalizationManager.LanguageKey)
            //{
            //    ChangeBorder();
            //}
        }
        #endregion
    }
}
