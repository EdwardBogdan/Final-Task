using UnityEngine;
using UnityEngine.Events;
using GameSystem.Initialization;

namespace Overlay.UIManagment.HudManagment
{
    [CreateAssetMenu(fileName = "HudController", menuName = "UI/HudController")]
    public class HudController : SystemOrigin<HudController>
    {
        [SerializeField] private HudMode _hudMode;

        public static bool HudMode => I._hudMode.Value;

        #region Controll
        public static void ChangeGlobalMode(bool mode)
        {
            I._hudMode.Value = mode;
        }

        public static void ChangeLocalModeToShow(UnitUIHud unit)
        {
            UnitUIHud hud = UIController.GetUnit<UnitUIHud>(unit);

            if (hud == null)
            {
#if UNITY_EDITOR
                Debug.Log($"Attempt to show not opened HUD: {UIController.GetName(unit)}");
#endif
                return;
            }

            hud.ChangeMode(true);
        }
        public static void ChangeLocalModeToHide(UnitUIHud unit)
        {
            UnitUIHud hud = UIController.GetUnit<UnitUIHud>(unit);

            if (hud == null)
            {
#if UNITY_EDITOR
                Debug.Log($"Attempt to hide not opened HUD: {UIController.GetName(unit)}");
#endif
                return;
            }

            hud.ChangeMode(false);
        }

        #endregion

        #region Event
        public static void ListenHudMode(UnityAction<bool> func, bool mode)
        {
            if (mode) I._hudMode.OnChanged.AddListener(func);
            else I._hudMode.OnChanged.RemoveListener(func);
        }
        #endregion

        private void OnValidate()
        {
            _hudMode.Validate();
        }

        #region Init
        private const string Group = "UI";
        private const string AddressableKey = "HudController";

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        private static void Init()
        {
            InitializeSystem(Group, AddressableKey);
        }
        #endregion
    }
}
