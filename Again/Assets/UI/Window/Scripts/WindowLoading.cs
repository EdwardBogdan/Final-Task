using GameSystem.SceneManagment;
using Localization;
using TMPro;
using GameSystem.Overlay;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows.SceneTransition
{
    public class WindowLoading : UnitUI
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private Image _panel;
        [SerializeField] private TextMeshProUGUI _text;

        private void Awake()
        {
            SceneConfig config = GameSceneManager.Config;

            _panel.color = config.PanelColor;

            _text.color = config.TextColor;

            _text.text = LocalizationManager.GetText(LocaGroup.Location, config.TextNameKey);

            GameSceneManager.ListenFinish(OnFinish);
        }

        private void OnFinish(SceneConfig config)
        {
            GameSceneManager.ListenFinish(OnFinish, false);

            _animator.SetTrigger("Hide");
        }
    }
}
