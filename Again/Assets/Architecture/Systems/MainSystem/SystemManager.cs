using System;
using DeepSystem.General;
using DeepSystem.SaveSystem;

namespace DeepSystem.Main
{
    public class SystemManager : SingletonSaveSystem<SystemManager>
    {
        #region Settings
        private const string SaveFolderName = "SystemData";
        internal static T GetSettingsSave<T>(string fileName) where T : new()
        {
            return SaveManager.GetSave<T>(SaveFolderName, fileName);
        }
        internal static void SetSettingsSave(object settings, string fileName)
        {
            SaveManager.SetSave(settings, SaveFolderName, fileName);
        }
        #endregion

        #region Mono
        protected void Start()
        {
            LoadSettings();
        }
        #endregion

        #region Events
        internal static event Action Save;
        internal static event Action Load;
        internal static event Action Drop;

        protected override void SaveSettings()
        {
            Save?.Invoke();
        }
        protected override void LoadSettings()
        {
            Load?.Invoke();
        }
        protected override void DropSettings()
        {
            Drop?.Invoke();
        }
        #endregion
    }
}
