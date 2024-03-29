using GameSystem.Initialization;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Overlay.UIManagment.General;

namespace Overlay.UIManagment
{
    [CreateAssetMenu(menuName = "UI/UIController", fileName = "UIController")]
    public class UIController : SystemOrigin<UIController>
    {        
        [SerializeField] private List<UnitUIRef> _activeUI = new List<UnitUIRef>();
        private static List<UnitUIRef> ActiveUI => I._activeUI;

        #region Logic
        internal static string GetName(UnitUI unit)
        {
            Type unitType = unit.GetType();

            return unitType.Name;
        }

        internal static UnitUIRef GetUnitRef(string name)
        {
            return ActiveUI.Find(ui => ui.TypeName == name);
        }
        internal static UnitUIRef GetUnitRef(UnitUI unit)
        {
            string typeName = GetName(unit);

            return ActiveUI.Find(ui => ui.TypeName == typeName);
        }

        internal static T GetUnit<T>(string name) where T : UnitUI
        {
            UnitUIRef uiRef = GetUnitRef(name);

            return uiRef == null ? null : uiRef.Unit as T;
        }
        internal static T GetUnit<T>(UnitUI unit) where T : UnitUI
        {
            UnitUIRef uiRef = GetUnitRef(unit);

            return uiRef == null ? null : uiRef.Unit as T;
        }

        private static T GetOrOpenUnitUI<T>(T unit) where T : UnitUI
        {
            string name = GetName(unit);

            var unitRef = GetUnitRef(name);

            if (unitRef != null)
            {
                return unitRef.Unit as T;
            }

            T instance = Instantiate(unit, OverlayManager.OverlayCanvas.transform);
            ActiveUI.Add(new UnitUIRef(name, instance));
            return instance;
        }
        #endregion

        #region UnitUI
        public static void OpenUI(UnitUI unit)
        {
            GetOrOpenUnitUI(unit);
        }
        public static void CloseUI(UnitUI unit)
        {
            var unitRef = GetUnitRef(unit);

            if (unitRef == null)
            {
#if UNITY_EDITOR
                Debug.LogWarning($"Attempt to close not opened UI: {GetName(unit)}");
#endif
                return;
            }

            ActiveUI.Remove(unitRef);
            Destroy(unitRef.Unit.gameObject);

        }
        #endregion

        #region UnitAnimUI
        public static void ShowUI(UnitUIAnim unit)
        {
            UnitUIAnim ui = GetOrOpenUnitUI(unit);

            ui.TriggerShow();
        }
        public static void HideUI(UnitUIAnim unit)
        {
            UnitUIAnim ui = GetUnit<UnitUIAnim>(unit);

            if (ui == null)
            {
#if UNITY_EDITOR
                Debug.LogWarning($"Attempt to hide not opened UI: {GetName(unit)}");
#endif
                return;
            }

            ui.TriggerHide();
        }
        #endregion

        #region Mono
        private void OnEnable()
        {
#if UNITY_EDITOR
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
#endif
        }
        private void OnDisable()
        {
#if UNITY_EDITOR
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
#endif
        }
        #endregion

        #region Editor
#if UNITY_EDITOR
        private void OnPlayModeStateChanged(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.ExitingPlayMode)
            {
                _activeUI.Clear();
            }
        }
#endif
        #endregion

        #region Init
        private const string Group = "UI";
        private const string AddressableKey = "UIController";

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        private static void Init()
        {
            InitializeSystem(Group, AddressableKey);
        }
        #endregion
    }
}
