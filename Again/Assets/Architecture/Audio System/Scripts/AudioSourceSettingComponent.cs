using GameSystem.Audio.Property;
using UnityEngine;

namespace GameSystem.Audio.Components
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioSourceSettingComponent : MonoBehaviour
    {
        [SerializeField] private AudioSettingMode _mode;
        [SerializeField] private AudioSource _source;

        private void Awake()
        {
            AudioFloatProperty _model = AudioManager.GetAudioProperty(_mode);

            _model.OnChanged.AddListener(OnSoundSettingChanged);

            OnSoundSettingChanged(_model.Value, 0);
        }
        private void OnDestroy()
        {
            AudioFloatProperty _model = AudioManager.GetAudioProperty(_mode);

            _model.OnChanged.RemoveListener(OnSoundSettingChanged);
        }

        private void OnSoundSettingChanged(float newValue, float oldValue)
        {
            _source.volume = newValue;
        }
    }
}
