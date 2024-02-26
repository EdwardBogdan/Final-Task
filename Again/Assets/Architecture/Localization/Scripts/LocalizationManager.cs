using UnityEngine;
using GameSystem.General;

namespace Localization
{
    [CreateAssetMenu(menuName = "Data/Localization/Manager", fileName = "LocalizationManager")]
    public class LocalizationManager : SystemOrigin<LocalizationManager>
    {
        [SerializeField] private LocalizationProperty _language;

        internal static Font Font => I._language.Def.GetFont;
        internal static string GetText(LocaGroup group, string key)
        {
            return I._language.Def.GetText(group, key);
        }

        #region Mono
#if UNITY_EDITOR
        private void OnValidate()
        {
            _language.Validate();
        }
#endif
        #endregion

        #region Init
        private const string AddressableKey = "LocaManager";

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        private static void Init()
        {
            InitializeSystem(AddressableKey);
        }
        #endregion
    }


}
