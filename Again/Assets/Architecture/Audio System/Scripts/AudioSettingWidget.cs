using GameSystem.Audio.Property;
using UnityEngine;
using UnityEngine.UI;

namespace GameSystem.Audio.Components
{
    public class AudioSettingWidget : MonoBehaviour
    {
        [SerializeField] private AudioSettingMode _mode;

        [SerializeField] private Slider _slider;
        [SerializeField] private Text _text;

        private AudioFloatProperty _model;

        public void OnSliderValueChanged(float value)
        {
            _model.Value = value;
        }
        private void OnValueChanged(float newValue, float oldValue)
        {
            var value = Mathf.Round(newValue * 100);

            _text.text = value.ToString();

            _slider.normalizedValue = newValue; 
        }

        #region Mono
        private void Awake()
        {
            _model = AudioManager.GetAudioProperty(_mode);

            _model.OnChanged.AddListener(OnValueChanged);
            OnValueChanged(_model.Value, 0);
        }
        private void OnDestroy()
        {
            _model.OnChanged.RemoveListener(OnValueChanged);
        }
        #endregion
    }
}
