using UnityEngine;

namespace DeepSystem.Audio.Components
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioSourceInstaller : MonoBehaviour
    {
        [SerializeField] private AudioSettingMode _mode;

        private void Awake()
        {
            AudioSource source = GetComponent<AudioSource>();

            AudioManager.InstallAudioSource(_mode, source);

            Destroy(this);
        }
    }
}