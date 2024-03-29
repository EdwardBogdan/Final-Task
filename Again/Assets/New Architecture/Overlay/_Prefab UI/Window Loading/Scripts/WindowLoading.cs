using SceneManagment;
using Localization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Overlay.UIManagment.Windows
{
    public class WindowLoading : UnitUI
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private Image _panel;
        [SerializeField] private TextMeshProUGUI _text;

        private void Awake()
        {
            SceneConfig config = SceneLoader.Config;

            _panel.color = config.PanelColor;

            _text.color = config.TextColor;

            _text.text = LocalizationManager.GetText(LocaGroup.Location, config.TextNameKey);

            SceneLoader.ListenFinish(OnFinish);
        }

        private void OnFinish(SceneConfig config, LoadingType type)
        {
            SceneLoader.ListenFinish(OnFinish, false);

            _animator.SetTrigger("Hide");
        }
    }
}
