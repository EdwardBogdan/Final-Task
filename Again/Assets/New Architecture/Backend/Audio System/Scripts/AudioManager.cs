using GameSystem.Initialization;
using UnityEngine;

namespace GameSystem.Audio
{
    [CreateAssetMenu(fileName = "AudioManager", menuName = "Audio/Manager")]
    public class AudioManager : SystemOrigin<AudioManager>
    {
        [Header("Music"), EditorAttributes.EditorReadOnly]

        [SerializeField] private AudioSource _musicSource;
        [SerializeField] private AudioFloatProperty _musicLevel;
        
        [Header("Sfx"), EditorAttributes.EditorReadOnly]

        [SerializeField] private AudioSource _sfxSource;
        [SerializeField] private AudioFloatProperty _sfxLevel;

        internal static AudioFloatProperty GetAudioProperty(AudioSettingMode mode)
        {
            return mode switch
            {
                AudioSettingMode.music => I._musicLevel,
                AudioSettingMode.sfx => I._sfxLevel,
                _ => null,
            };
        }
        internal static AudioSource GetAudioSource(AudioSettingMode mode)
        {
            return mode switch
            {
                AudioSettingMode.music => I._musicSource,
                AudioSettingMode.sfx => I._sfxSource,
                _ => null,
            };
        }
        internal static void InstallAudioSource(AudioSettingMode mode, AudioSource source)
        {
            switch (mode)
            {
                case AudioSettingMode.music:
                    I._musicSource = source;
                    break;
                case AudioSettingMode.sfx:
                    I._sfxSource = source;
                    break;
                default:
                    break;
            }
        }

        #region Init
        private const string Group = "Backend";
        private const string AddressableKey = "AudioManager";

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        private static void Init()
        {
            InitializeSystem(Group, AddressableKey);
        }
        #endregion

        #region Mono

#if UNITY_EDITOR
        private void OnValidate()
        {
            _musicLevel.Validate();
            _sfxLevel.Validate();
        }
#endif
        #endregion
    }
}
