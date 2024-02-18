using GameSystem.Main;
using GameSystem.General;
using System;
using UnityEngine;
using UnityEngine.UI;
using Property.ObservableProperty;

namespace GameSystem.Screen
{
    public class ScreenManager : SingletonSaveSystem<ScreenManager>
    {
        [SerializeField] private Canvas _overlayCanvas;
        [SerializeField] private CanvasScaler _scaler;

        [SerializeField] private Vector2ObsProperty _resolution;

        public static Canvas OverlayCanvas => I._overlayCanvas;
        public static Vector2ObsProperty Resolution => I._resolution;
        internal static void ChangeResolution(Vector2 size)
        {
            I._resolution.Value = size;
        }

        #region Settings
        private const string SettingsSaveFileName = "ScreenSettings";

        [Serializable]
        private class Settings
        {
            [SerializeField] internal Vector2 resolution = I._resolution.Value;
        }
        protected override void LoadSettings()
        {
            Settings data = SystemManager.GetSettingsSave<Settings>(SettingsSaveFileName);

            _resolution.Value = data.resolution;
        }
        protected override void SaveSettings()
        {
            Settings data = new()
            {
                resolution = _resolution.Value,
            };

            SystemManager.SetSettingsSave(data, SettingsSaveFileName);
        }
        protected override void DropSettings()
        {
            Settings data = new();

            SystemManager.SetSettingsSave(data, SettingsSaveFileName);
        }
        #endregion

        #region Mono
        protected override void Awake()
        {
            base.Awake();

            SystemManager.Load += LoadSettings;
            SystemManager.Save += SaveSettings;
            SystemManager.Drop += DropSettings;

            _resolution.OnChanged.AddListener(OnChangeResolution);
        }
        private void OnDestroy()
        {
            SystemManager.Load -= LoadSettings;
            SystemManager.Save -= SaveSettings;
            SystemManager.Drop -= DropSettings;

            _resolution.OnChanged.RemoveListener(OnChangeResolution);
        }
        private void OnChangeResolution(Vector2 value, Vector2 oldValue)
        {
            _scaler.referenceResolution = value;
        }
#if UNITY_EDITOR 
        private void OnValidate()
        {
            _resolution.Validate();
        }
#endif
        #endregion
    }
}
